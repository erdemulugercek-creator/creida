namespace Creida.Services;

public class SiteSettings
{
    // Hero
    public string HeroLine1 { get; set; } = "Veri konuşur,";
    public string HeroLine2 { get; set; } = "kreatif susar.";
    public string HeroSubtitle { get; set; } = "Performans pazarlamasını, marka stratejisini ve kreatif tasarımı tek bir masada birleştiren bağımsız bir ajansız.";
    public string HeroCtaPrimaryLabel { get; set; } = "Bir kahve içelim";
    public string HeroCtaPrimaryHref { get; set; } = "/#contact";
    public string HeroCtaSecondaryLabel { get; set; } = "Çalışmalarımız";
    public string HeroCtaSecondaryHref { get; set; } = "/#work";

    // İletişim
    public string Email { get; set; } = "hello@creida.co";
    public string Phone { get; set; } = "+90 531 405 30 70";
    public string Address { get; set; } = "Kadıköy, İstanbul";

    // Sosyal
    public string Instagram { get; set; } = "https://instagram.com/creidaco";
    public string LinkedIn { get; set; } = "https://linkedin.com/company/creidaco";
}

public class SiteSettingsService
{
    private const string Key = "site-settings";
    private readonly ContentStore _store;

    public SiteSettingsService(ContentStore store)
    {
        _store = store;
        if (!_store.Exists(Key))
            _store.Write(Key, new SiteSettings());
    }

    public SiteSettings Get() => _store.ReadOr(Key, () => new SiteSettings());

    public void Save(SiteSettings s) => _store.Write(Key, s);
}
