using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Localization;

namespace Creida.Localization;

/// <summary>
/// Çok hafif JSON tabanlı IStringLocalizer.
/// Her kültür (örn. tr, en) için Localization/Resources/{culture}.json dosyasını okur.
/// Anahtar bulunmazsa, son çare olarak anahtarın kendisini döner (LocalizedString.ResourceNotFound = true).
/// </summary>
public class JsonStringLocalizer : IStringLocalizer
{
    // Dosya mtime'ına göre invalidate olan cache.
    // Geliştirme sırasında JSON'u kaydedince anında yenisi okunsun.
    private static readonly ConcurrentDictionary<string, (DateTime mtime, Dictionary<string, string> dict)> _cache = new();
    private readonly string _resourcesPath;
    private readonly string _defaultCulture;

    public JsonStringLocalizer(string resourcesPath, string defaultCulture = "tr")
    {
        _resourcesPath = resourcesPath;
        _defaultCulture = defaultCulture;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetValue(name, out var found);
            return new LocalizedString(name, value, resourceNotFound: !found);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = GetValue(name, out var found);
            var formatted = string.Format(value, arguments);
            return new LocalizedString(name, formatted, resourceNotFound: !found);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        var dict = LoadCulture(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
        foreach (var kv in dict)
        {
            yield return new LocalizedString(kv.Key, kv.Value, resourceNotFound: false);
        }
    }

    private string GetValue(string key, out bool found)
    {
        var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        if (TryGet(culture, key, out var value))
        {
            found = true;
            return value;
        }

        if (culture != _defaultCulture && TryGet(_defaultCulture, key, out value))
        {
            found = true;
            return value;
        }

        found = false;
        return key;
    }

    private bool TryGet(string culture, string key, out string value)
    {
        var dict = LoadCulture(culture);
        return dict.TryGetValue(key, out value!);
    }

    private Dictionary<string, string> LoadCulture(string culture)
    {
        var file = Path.Combine(_resourcesPath, $"{culture}.json");
        if (!File.Exists(file))
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        var mtime = File.GetLastWriteTimeUtc(file);

        if (_cache.TryGetValue(culture, out var cached) && cached.mtime == mtime)
        {
            return cached.dict;
        }

        var json = File.ReadAllText(file);
        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        _cache[culture] = (mtime, dict);
        return dict;
    }
}

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly string _resourcesPath;
    private readonly string _defaultCulture;

    public JsonStringLocalizerFactory(string resourcesPath, string defaultCulture = "tr")
    {
        _resourcesPath = resourcesPath;
        _defaultCulture = defaultCulture;
    }

    public IStringLocalizer Create(Type resourceSource) =>
        new JsonStringLocalizer(_resourcesPath, _defaultCulture);

    public IStringLocalizer Create(string baseName, string location) =>
        new JsonStringLocalizer(_resourcesPath, _defaultCulture);
}

/// <summary>
/// Generic IStringLocalizer&lt;T&gt; wrapper — DI'da open generic olarak kayıtlı,
/// her T tipi için aynı paylaşılan kaynak setini döner.
/// </summary>
public class JsonStringLocalizerOfT<T> : IStringLocalizer<T>
{
    private readonly IStringLocalizer _inner;

    public JsonStringLocalizerOfT(IStringLocalizerFactory factory)
    {
        _inner = factory.Create(typeof(T));
    }

    public LocalizedString this[string name] => _inner[name];
    public LocalizedString this[string name, params object[] arguments] => _inner[name, arguments];
    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _inner.GetAllStrings(includeParentCultures);
}
