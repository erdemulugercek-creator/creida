using System.Globalization;
using Markdig;

namespace Creida.Data;

public record InsightFrontmatter(
    string Slug,
    string Title,
    string Subtitle,
    string Author,
    DateOnly Date,
    string ReadingTime,
    string Category);

public record InsightPost(
    InsightFrontmatter Meta,
    string HtmlBody);

/// <summary>
/// Content/Insights/*.md dosyalarını okuyup hafif bir YAML benzeri header parser ile
/// metadata'yı ayırır, gövdeyi Markdig ile HTML'e dönüştürür.
/// </summary>
public class InsightsService
{
    private static readonly MarkdownPipeline Pipeline =
        new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

    private readonly string _root;
    private List<InsightPost>? _cache;
    private DateTime _cacheStamp = DateTime.MinValue;

    public InsightsService(IWebHostEnvironment env)
    {
        _root = Path.Combine(env.ContentRootPath, "Content", "Insights");
    }

    public IReadOnlyList<InsightPost> All()
    {
        if (!Directory.Exists(_root)) return Array.Empty<InsightPost>();

        var latestFileTime = Directory.GetFiles(_root, "*.md")
            .Select(f => File.GetLastWriteTimeUtc(f))
            .DefaultIfEmpty(DateTime.MinValue)
            .Max();

        if (_cache != null && latestFileTime <= _cacheStamp)
            return _cache;

        var posts = new List<InsightPost>();
        foreach (var path in Directory.GetFiles(_root, "*.md"))
        {
            var raw = File.ReadAllText(path);
            if (TryParse(path, raw, out var post)) posts.Add(post);
        }

        _cache = posts.OrderByDescending(p => p.Meta.Date).ToList();
        _cacheStamp = latestFileTime;
        return _cache;
    }

    public InsightPost? FindBySlug(string slug) =>
        All().FirstOrDefault(p => string.Equals(p.Meta.Slug, slug, StringComparison.OrdinalIgnoreCase));

    private static bool TryParse(string path, string raw, out InsightPost post)
    {
        post = default!;
        // Frontmatter biçimi:
        // ---
        // key: value
        // ---
        // markdown body...
        if (!raw.StartsWith("---")) return false;
        var endIdx = raw.IndexOf("\n---", 3, StringComparison.Ordinal);
        if (endIdx < 0) return false;

        var header = raw.Substring(3, endIdx - 3);
        var body = raw.Substring(endIdx + 4).TrimStart('\n', '\r');

        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in header.Split('\n'))
        {
            var l = line.Trim();
            if (string.IsNullOrEmpty(l)) continue;
            var ci = l.IndexOf(':');
            if (ci <= 0) continue;
            var key = l.Substring(0, ci).Trim();
            var val = l.Substring(ci + 1).Trim().Trim('"');
            dict[key] = val;
        }

        var slug = dict.GetValueOrDefault("slug") ?? Path.GetFileNameWithoutExtension(path);
        var title = dict.GetValueOrDefault("title") ?? slug;
        var subtitle = dict.GetValueOrDefault("subtitle") ?? "";
        var author = dict.GetValueOrDefault("author") ?? "Creida";
        var readingTime = dict.GetValueOrDefault("readingTime") ?? "5 dk okuma";
        var category = dict.GetValueOrDefault("category") ?? "Notlar";

        DateOnly date = DateOnly.FromDateTime(DateTime.Today);
        if (dict.TryGetValue("date", out var ds) &&
            DateOnly.TryParseExact(ds, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var d))
        {
            date = d;
        }

        var html = Markdown.ToHtml(body, Pipeline);
        post = new InsightPost(
            new InsightFrontmatter(slug, title, subtitle, author, date, readingTime, category),
            html);
        return true;
    }
}
