namespace Creida.Data;

public record SectorPlaybook(string Title, string Body);
public record SectorPage(
    string Slug,
    string Name,
    string Headline1,
    string Headline2,
    string Lede,
    string PriceFrom,
    IReadOnlyList<string> CaseSlugs,
    IReadOnlyList<SectorPlaybook> Playbook,
    IReadOnlyList<(string Q, string A)> Faq);

public static class Sectors
{
    public static IReadOnlyList<SectorPage> All { get; } = new[]
    {
        new SectorPage(
            Slug: "fintech",
            Name: "Fintech",
            Headline1: "Para konuşur,",
            Headline2: "siz korkutmayın.",
            Lede: "Türkiye'de 22-32 yaş yatırımcı kitlesi için fintech markası kurmak, kategori klişesini kırmaktan geçiyor. Yeşil-kırmızı oklar, ciddi yüzlü bankacılar değil; sade illüstrasyon, gündelik dil, hızlı onboarding.",
            PriceFrom: "₺220.000",
            CaseSlugs: new[] { "veris-finans" },
            Playbook: new[]
            {
                new SectorPlaybook("Onboarding'i 3 adıma sıkıştırın", "Çoğu uygulama 7-9 adımlı KYC'ye sıkışıyor; bizim akışlarımızda 3 adım + paralel doğrulama. Aktivasyon oranı %19'dan %34'e kadar çıkıyor."),
                new SectorPlaybook("Korkutmayan dil sistemi", "20+ finansal kavram için (kâr payı, vade, kısa pozisyon...) gündelik dile çevrilmiş, sıcak palette illüstrasyonlu bir sistem inşa ediyoruz."),
                new SectorPlaybook("Performance + brand health birlikte", "Sadece CPI kovalamak yeterli değil. Brand lift ile birlikte ölçüyoruz; sevmediği ürünü kullanan kullanıcı kazanmıyorsunuz.")
            },
            Faq: new[]
            {
                ("BDDK / SPK uyumlu metinleri yazabilir misiniz?", "Hayır, hukuki uyum metnini avukat yazmalı; biz iletişimsel tonu uyumlu hâle getiriyoruz."),
                ("Mevcut uygulamamızı yeniden tasarlar mısınız?", "İçeriden bir tasarım ekibiniz yoksa evet; varsa tasarım sisteminize uyum sağlayan kreatif üretiyoruz.")
            }),

        new SectorPage(
            Slug: "dtc",
            Name: "DTC / E-ticaret",
            Headline1: "Ürün ilk değil,",
            Headline2: "marka.",
            Lede: "Cilt bakım, kahve, yeme-içme, mobilya — Türkiye'de DTC pazarı, ürün tarafı doygun; kazanan, marka tarafı zayıf rakipler arasında doğru ses tonunu bulan. Ambalajdan reklam kreatifine, abonelikten lifetime value'ya kadar bütünsel düşünüyoruz.",
            PriceFrom: "₺250.000",
            CaseSlugs: new[] { "ayla-botanik", "lume-coffee" },
            Playbook: new[]
            {
                new SectorPlaybook("Ambalaj iletişimin yarısıdır", "Müşteri ürünü ilk fiziksel olarak gördüğünde reklam bütçesinin geri kalanı boşa gider ya da katlanır. Ambalajı brief'e dahil etmediğimiz proje almıyoruz."),
                new SectorPlaybook("İkinci satın alma asıl satıştır", "İlk siparişten CAC çıkarmak çoğu DTC için imkânsız. Abonelik veya 2. sipariş döngüsünü tasarlamadan kreatif yapmak israftır."),
                new SectorPlaybook("UGC'yi performansa entegre edin", "Markanızın UGC oranı yıllık %50 büyümüyorsa, içerik motorunuz iflasta. Bunu kuruyoruz.")
            },
            Faq: new[]
            {
                ("Shopify / Ideasoft entegrasyonu yapar mısınız?", "Kreatif ve performans tarafıyız; e-ticaret altyapısı için kanıtlanmış partner'ler öneriyoruz."),
                ("Ürün fotoğrafçılığı dahil mi?", "Marka kimliği projesinde lansman çekimi dahil; sürekli üretim için aylık paket konuşulur.")
            }),

        new SectorPage(
            Slug: "b2b-saas",
            Name: "B2B SaaS",
            Headline1: "Sıkıcı kategoride,",
            Headline2: "fark edilen marka.",
            Lede: "Tedarik zinciri, sigorta yazılımı, ERP, sales tool — Türkiye'de B2B SaaS pazarının %90'ı aynı PowerPoint dilini konuşuyor. Biz ABM mantığıyla, hesap özelinde kreatif üretiyor, satış döngüsünü hızlandırıyoruz.",
            PriceFrom: "₺180.000",
            CaseSlugs: new[] { "northgate" },
            Playbook: new[]
            {
                new SectorPlaybook("Mesaj mimarisi olmadan kreatif yapmıyoruz", "Hedef hesabın 8 farklı kişisi var: COO başka şey okur, IT director başka. Üç katmanlı mesaj mimarisi olmadan kreatif boşa gider."),
                new SectorPlaybook("LinkedIn'de hesap, kişi değil", "ABM kampanyasında 40-100 hedef hesap, her hesap için 3-5 persona, 12-20 kreatif. Bu üretim ekonomisini kurmadan sonuç gelmiyor."),
                new SectorPlaybook("Sales enablement, marketing'in işidir", "Pipeline'ı pazarlama açıyor, ama dönüşümün %60'ı satış görüşmesinde belirleniyor. Satış materyallerini de biz tasarlıyoruz.")
            },
            Faq: new[]
            {
                ("HubSpot / Salesforce kurulumu yapar mısınız?", "Doğrudan değil; attribution kurulumu ve dashboard hazırlama dahil; CRM tarafı için RevOps partner öneriyoruz."),
                ("Yurt dışına açılan müşteriyle çalışır mısınız?", "Çoğu B2B müşterimiz Avrupa ve MENA'ya satıyor; İngilizce iletişim ve Avrupa ABM tarafı standart paketimizde.")
            })
    };

    public static SectorPage? FindBySlug(string slug) =>
        All.FirstOrDefault(s => string.Equals(s.Slug, slug, StringComparison.OrdinalIgnoreCase));
}
