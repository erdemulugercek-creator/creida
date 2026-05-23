namespace Creida.Services;

/// <summary>
/// Sitenin görsel kimliği: font, punto, renk, kenar yumuşaklığı.
/// _Layout bu değerleri okuyup head'e :root CSS değişken override'ı
/// olarak basar — site.css'i değiştirmeden tüm temayı değiştirir.
/// </summary>
public class AppearanceSettings
{
    // --- Fontlar ---
    // Neue Radial yerel TTF (wwwroot/fonts) — Google'dan istenmez.
    public string FontHeadingFamily { get; set; } = "Neue Radial";
    public string FontHeadingWeights { get; set; } = "400;500;600;700;800";
    public string FontBodyFamily { get; set; } = "Neue Radial";
    public string FontBodyWeights { get; set; } = "300;400;500;600";
    public string FontSerifFamily { get; set; } = "Neue Radial";
    public string FontSerifWeights { get; set; } = "400";

    // Yerel olarak yüklenen fontlar (Google Fonts'a istek atılmaz)
    private static readonly HashSet<string> LocalFonts = new(StringComparer.OrdinalIgnoreCase)
    {
        "Neue Radial", "Neue Radial Alt"
    };

    // --- Puntolar (px cinsinden) ---
    public int FontSizeBase { get; set; } = 16;           // gövde
    public int FontSizeHeroMin { get; set; } = 36;        // hero başlık alt sınır
    public int FontSizeHeroMax { get; set; } = 90;        // hero başlık üst sınır
    public int FontSizeH2 { get; set; } = 48;
    public int FontSizeH3 { get; set; } = 28;
    public int FontSizeSmall { get; set; } = 13;

    // --- Renkler (hex) ---
    public string ColorBgDark { get; set; } = "#0A0A0A";
    public string ColorTextDark { get; set; } = "#FFFFFF";
    public string ColorMutedDark { get; set; } = "#8A8A8A";
    public string ColorBgLight { get; set; } = "#FFFFFF";
    public string ColorTextLight { get; set; } = "#1D1D1B";
    public string ColorMutedLight { get; set; } = "#6B6B6B";
    public string ColorAccent { get; set; } = "#FFFFFF";  // hero italic vurgu

    // --- Köşe yumuşaklığı (px) ---
    public int RadiusSmall { get; set; } = 8;
    public int RadiusMedium { get; set; } = 14;
    public int RadiusLarge { get; set; } = 22;

    // --- Boşluk skalası (px) ---
    public int SectionSpacing { get; set; } = 140;

    public string GoogleFontsUrl()
    {
        var families = new List<string>();
        var added = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        void Add(string family, string weights)
        {
            if (string.IsNullOrWhiteSpace(family)) return;
            if (LocalFonts.Contains(family)) return; // yerel font — Google'dan istenmez
            if (!added.Add(family)) return;
            families.Add($"{family.Replace(" ", "+")}:wght@{weights}");
        }
        Add(FontHeadingFamily, FontHeadingWeights);
        Add(FontBodyFamily, FontBodyWeights);
        Add(FontSerifFamily, FontSerifWeights);
        if (families.Count == 0) return "";
        return "https://fonts.googleapis.com/css2?" + string.Join("&", families.Select(f => "family=" + f)) + "&display=swap";
    }

    public string ToCssVariables()
    {
        // CSS değişkenlerini :root altına basıyoruz. Mevcut site.css'teki
        // değişken isimleri ile birebir uyumlu — başka kod değişmesin.
        return $@"
:root[data-theme=""dark""], :root {{
  --bg: {ColorBgDark};
  --text: {ColorTextDark};
  --muted: {ColorMutedDark};
  --accent: {ColorAccent};
}}
:root[data-theme=""light""] {{
  --bg: {ColorBgLight};
  --text: {ColorTextLight};
  --muted: {ColorMutedLight};
}}
:root {{
  --radius-sm: {RadiusSmall}px;
  --radius-md: {RadiusMedium}px;
  --radius-lg: {RadiusLarge}px;
  --section-py: {SectionSpacing}px;
  --font-heading: ""{FontHeadingFamily}"", -apple-system, BlinkMacSystemFont, ""Segoe UI"", system-ui, sans-serif;
  --font-body: ""{FontBodyFamily}"", -apple-system, BlinkMacSystemFont, ""Segoe UI"", system-ui, sans-serif;
  --font-serif: ""{FontSerifFamily}"", Georgia, serif;
}}
html, body {{ font-family: var(--font-body); font-size: {FontSizeBase}px; }}
h1, h2, h3, h4, h5, .display, .hero-headline {{ font-family: var(--font-heading); }}
.italic, em, .accent {{ font-family: var(--font-serif); }}
.hero-headline {{ font-size: clamp({FontSizeHeroMin}px, 6vw, {FontSizeHeroMax}px); }}
.section-head h2, h2 {{ font-size: {FontSizeH2}px; }}
h3 {{ font-size: {FontSizeH3}px; }}
small, .small, .meta-side, .eyebrow {{ font-size: {FontSizeSmall}px; }}
";
    }
}

public class AppearanceService
{
    private const string Key = "appearance";
    private readonly ContentStore _store;

    public AppearanceService(ContentStore store)
    {
        _store = store;
        if (!_store.Exists(Key))
            _store.Write(Key, new AppearanceSettings());
    }

    public AppearanceSettings Get() => _store.ReadOr(Key, () => new AppearanceSettings());

    public void Save(AppearanceSettings s) => _store.Write(Key, s);
}
