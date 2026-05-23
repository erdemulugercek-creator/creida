using System.Text.Json;

namespace Creida.Services;

/// <summary>
/// Veritabanı yerine JSON dosyaları. Her "koleksiyon" (team, cases, insights,
/// settings) ContentRoot/Data/Content/{name}.json içinde tutulur. Atomic save
/// için önce .tmp'ye yazıp Move ile değiştiriyoruz. Bellekte cache yok —
/// yazma frekansı düşük (admin tek kullanıcı), okumalar zaten OS cache'inden
/// gelir. Yapı değişince yeniden başlatmaya gerek kalmasın diye her okuyuşta
/// disk'e bakıyoruz.
/// </summary>
public class ContentStore
{
    private readonly string _root;
    private static readonly JsonSerializerOptions Json = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        // Türkçe karakterleri escape etme — gözle düzenlenebilir kalsın
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public ContentStore(IWebHostEnvironment env)
    {
        _root = Path.Combine(env.ContentRootPath, "Data", "Content");
        Directory.CreateDirectory(_root);
    }

    private string FilePathFor(string name) => Path.Combine(_root, $"{name}.json");

    public bool Exists(string name) => File.Exists(FilePathFor(name));

    public T? Read<T>(string name) where T : class
    {
        var p = FilePathFor(name);
        if (!File.Exists(p)) return null;
        var json = File.ReadAllText(p);
        return JsonSerializer.Deserialize<T>(json, Json);
    }

    public T ReadOr<T>(string name, Func<T> fallback) where T : class
    {
        return Read<T>(name) ?? fallback();
    }

    public void Write<T>(string name, T value)
    {
        var p = FilePathFor(name);
        var tmp = p + ".tmp";
        var json = JsonSerializer.Serialize(value, Json);
        File.WriteAllText(tmp, json);
        if (File.Exists(p)) File.Delete(p);
        File.Move(tmp, p);
    }

    /// <summary>
    /// Yeni id üretici — slug'lar için. "ayla-botanik" benzersiz olmazsa
    /// "-2", "-3" ekler.
    /// </summary>
    public string UniqueSlug(IEnumerable<string> existing, string seed)
    {
        var set = new HashSet<string>(existing, StringComparer.OrdinalIgnoreCase);
        var s = Slugify(seed);
        if (!set.Contains(s)) return s;
        for (var i = 2; ; i++)
            if (!set.Contains($"{s}-{i}")) return $"{s}-{i}";
    }

    public static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "yeni";
        var map = new Dictionary<char, string>
        {
            { 'ı', "i" }, { 'İ', "i" }, { 'ş', "s" }, { 'Ş', "s" },
            { 'ğ', "g" }, { 'Ğ', "g" }, { 'ü', "u" }, { 'Ü', "u" },
            { 'ö', "o" }, { 'Ö', "o" }, { 'ç', "c" }, { 'Ç', "c" }
        };
        var sb = new System.Text.StringBuilder();
        foreach (var ch in input.Trim().ToLowerInvariant())
        {
            if (map.TryGetValue(ch, out var rep)) sb.Append(rep);
            else if (char.IsLetterOrDigit(ch)) sb.Append(ch);
            else if (ch == ' ' || ch == '-' || ch == '_') sb.Append('-');
        }
        var slug = System.Text.RegularExpressions.Regex.Replace(sb.ToString(), "-+", "-").Trim('-');
        return string.IsNullOrEmpty(slug) ? "yeni" : slug;
    }
}
