namespace Creida.Data;

public record CaseMetric(string Value, string Label);
public record CaseSection(string Heading, string Body);
public record CaseProcessStep(string Number, string Title, string Body);
public record CaseQuote(string Body, string Author, string Role);
public record CaseCredit(string Role, string Names);
public record CaseGalleryImage(string Url, string Caption);

public record CaseStudy(
    string Slug,
    string Brand,
    string Tagline,
    string Category,
    string Industry,
    string Year,
    string Duration,
    string Location,
    string CoverClass,
    string Lede,
    string Mark,
    string Badge,
    string HeroImage,
    IReadOnlyList<string> Services,
    IReadOnlyList<CaseSection> Sections,
    IReadOnlyList<CaseProcessStep> Process,
    IReadOnlyList<CaseGalleryImage> Gallery,
    CaseQuote Quote,
    IReadOnlyList<CaseCredit> Credits,
    IReadOnlyList<CaseMetric> Metrics);

public static class CaseStudies
{
    // Görseller Picsum üzerinden (https://picsum.photos) — ücretsiz,
    // her seed için aynı profesyonel fotoğrafı döner. Markaya özel
    // çekimle değiştirmek için sadece seed yerine kendi URL'ini koy.
    private const string ImgBase = "https://picsum.photos/seed/";

    public static IReadOnlyList<CaseStudy> All { get; } = new[]
    {
        // ========= AYLA BOTANİK =========
        new CaseStudy(
            Slug: "ayla-botanik",
            Brand: "Ayla Botanik",
            Tagline: "Doğa yapısöküme uğradı.",
            Category: "Marka kimliği · Dijital lansman",
            Industry: "Cilt Bakım · DTC",
            Year: "2026",
            Duration: "14 hafta",
            Location: "İstanbul",
            CoverClass: "cover-1",
            Mark: "ayla",
            Badge: "Lansman · 2026",
            Lede: "Yeni nesil bir cilt bakım markası için sıfırdan marka kimliği, ambalaj sistemi ve lansman kampanyası. Üç ayda 18.000 kayıtlı kullanıcıya ulaştık, ilk 30 günde 6.400 sipariş aldık.",
            HeroImage: ImgBase + "creida-ayla-hero/1800/1100",
            Services: new[] { "Marka stratejisi", "İsimlendirme", "Görsel kimlik", "Ambalaj tasarımı", "Dijital reklam", "İçerik prodüksiyonu" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Pazardaki cilt bakım markaları ya klinik laboratuvar dilini ya da fazla romantik bir doğa dilini seçiyordu. Ayla, ikisinin ortasında — bilimsel ama sıcak — bir alan istiyordu. Hedef kitle 25–38 yaş, eğitimli, sosyal medyaya hâkim ve etiket okuyan kadındı. Mevcut iletişim ne onlara hitap ediyordu ne de markayı premium konumlandırıyordu."),
                new CaseSection("Yaklaşım",
                    "\"Tanıdık bir botanik\" konseptiyle, bilinen Anadolu bitkilerini (kekik, lavanta, ada çayı, gül) modern bir formülasyon diliyle bir araya getirdik. Tipografi sade Geist + serif başlıklar, paleti toprak tonlu, ses tonu sokak diline yakın ama doktrini bilimsel. Ambalaj sistemini 3 kategoride 9 ürüne ölçeklenebilir kıldık."),
                new CaseSection("Sonuç",
                    "Lansman haftasında web sitesi 220.000 tekil ziyaretçi gördü, ilk 30 günde 6.400 sipariş alındı; üçüncü ayda CAC (kullanıcı edinim maliyeti) ilk hedefin %38 altına indi. Kategoride en yüksek save-rate'li Instagram lansmanlarından biri oldu (1.84%).")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Strateji ve isim",
                    "İki haftalık masterclass: pazar analizi, kullanıcı görüşmeleri (12 derinlemesine), konum haritası ve isim atölyesi. \"Ayla\" — Anadolu adı, hem ay halesi hem botanik hisleri taşıdığı için seçildi."),
                new CaseProcessStep("02", "Görsel kimlik sistemi",
                    "Logo, tipografi, palet, fotoğraf stili, illüstrasyon dili, ses tonu rehberi ve ambalaj sistemi. 64 sayfalık marka kitabı + dijital tokenlar."),
                new CaseProcessStep("03", "Ambalaj prototipleri",
                    "Cam şişe + minimal etiket sistemi. 9 ürün için tek bir kalıp, dokuz farklı renk kodu. Sürdürülebilir mürekkep ve geri dönüşümlü karton."),
                new CaseProcessStep("04", "Lansman ve performans",
                    "Influencer seeding (32 mikro-yaratıcı), Meta + TikTok reklamları, e-posta serisi ve içerik takvimi. Lansman gününden itibaren günlük dashboard takibi.")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-ayla-g1/1400/900", "Ambalaj sistemi, dokuz ürün için tek mimari."),
                new CaseGalleryImage(ImgBase + "creida-ayla-g2/1400/900", "Lansman görsellerinden bir kare."),
                new CaseGalleryImage(ImgBase + "creida-ayla-g3/900/1100", "Marka rehberinden tipografi sayfası.")
            },
            Quote: new CaseQuote(
                "Yıllardır pazarda kendimize benzer bir marka arıyorduk. Bulamayınca Creida ile yaptık. Lansman sonrası ikinci hafta CAC'imiz hedefin altına indi, hâlâ orada.",
                "Selin Yıldırım",
                "Kurucu Ortak, Ayla Botanik"),
            Credits: new[]
            {
                new CaseCredit("Strateji", "Mert Aksoy, Beril Demir"),
                new CaseCredit("Kreatif Direktör", "Eda Karaman"),
                new CaseCredit("Sanat Yönetmeni", "Tuna Bakırcıoğlu"),
                new CaseCredit("Kopirayter", "Yiğit Solmaz"),
                new CaseCredit("Ambalaj", "Studio Kavak (iş birliği)"),
                new CaseCredit("Performans", "Hazal Üstün, Ozan Polat"),
                new CaseCredit("Fotoğraf", "Cem Kütük")
            },
            Metrics: new[]
            {
                new CaseMetric("18K", "Lansman kayıt"),
                new CaseMetric("3.4×", "ROAS"),
                new CaseMetric("−38%", "CAC iyileşmesi"),
                new CaseMetric("220K", "Tekil ziyaretçi")
            }),

        // ========= NORTHGATE =========
        new CaseStudy(
            Slug: "northgate",
            Brand: "NorthGate",
            Tagline: "Yeni nesil B2B'nin sesi.",
            Category: "Konumlandırma · LinkedIn Ads",
            Industry: "B2B SaaS · Tedarik Zinciri",
            Year: "2025",
            Duration: "6 ay",
            Location: "İstanbul · Londra",
            CoverClass: "cover-2",
            Mark: "northgate",
            Badge: "B2B SaaS",
            Lede: "Sıkıcı bir kategoride farklılaşmak — tedarik zinciri yazılımı için yeni marka konumlandırması, mesaj mimarisi ve LinkedIn üzerinde altı aylık ABM kampanyası. MQL %170 arttı.",
            HeroImage: ImgBase + "creida-northgate-hero/1800/1100",
            Services: new[] { "Marka konumlandırma", "Mesaj mimarisi", "LinkedIn Ads", "ABM kampanyası", "İçerik üretimi" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Pazardaki tüm rakipler aynı PowerPoint dilini konuşuyordu: \"end-to-end visibility\", \"real-time analytics\", \"seamless integration\". NorthGate, COO'larla konuşurken bu jargondan sıkıldıklarını biliyordu ama satış ekibi bile mesajı aynı kelimelerle kuruyordu. Pipeline veriye dayalı değil, ilişkiye dayalıydı."),
                new CaseSection("Yaklaşım",
                    "\"Tedarik zinciri sessiz olmalı.\" Konumlandırmasıyla, bağırarak değil çalışarak işini yapan bir markaya dönüştürdük. ABM (Account-Based Marketing) kampanyalarında 40 hedef hesap için, alıcı kişisine özel — sektöre, role, hatta şirketin son yatırım haberine kadar — kreatifler ürettik. Mesaj mimarisi: jargonsuz, vakaya dayalı, sayısal."),
                new CaseSection("Sonuç",
                    "Pipeline'a giren MQL sayısı 5 ayda 2.7× arttı; ortalama deal velocity %22 hızlandı; LinkedIn CPM'leri %31 düştü. 6. ay sonunda 11 enterprise hesap kapanmıştı ve sales cycle ortalama 142 günden 110 güne indi.")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Alıcı haritası",
                    "8 COO, 6 supply chain lead, 4 IT director ile derinlemesine. \"Onlar gibi konuşmayı\" değil, onların neden seçtiğini anlamaya çalıştık."),
                new CaseProcessStep("02", "Mesaj mimarisi",
                    "Üç katmanlı: kategoriye karşı pozisyon (sessiz vs. gürültücü), şirket büyüklüğüne göre değer önerisi, role göre proof point. 12 sayfalık iç doküman."),
                new CaseProcessStep("03", "Kreatif sistem",
                    "40 hesap × 3 persona × 4 medya = 480 reklam varyantı şablonu. Tek bir görsel sistemde, otomatize edilebilir kreatif üretim."),
                new CaseProcessStep("04", "Optimizasyon",
                    "Haftalık standup, iki haftada bir kreatif rotasyonu. CRM (HubSpot) entegrasyonu, attribution kurulumu, dashboard.")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-northgate-g1/1400/900", "ABM kampanyası — hesap özelinde landing page."),
                new CaseGalleryImage(ImgBase + "creida-northgate-g2/900/1100", "LinkedIn carousel — sektör başına özelleştirilmiş kreatif."),
                new CaseGalleryImage(ImgBase + "creida-northgate-g3/1400/900", "Sales enablement materyali, satış ekibi için.")
            },
            Quote: new CaseQuote(
                "Önceki ajansla kampanya kreatifi 6 haftaydı, biz 3'üncü hafta canlıydık. Daha önemlisi: ilk defa müşteri onları aradıklarında \"sizinle yeni tanıştım\" demiyor.",
                "Daniel Kessler",
                "CMO, NorthGate"),
            Credits: new[]
            {
                new CaseCredit("Strateji", "Beril Demir"),
                new CaseCredit("Mesaj mimarisi", "Yiğit Solmaz, Beril Demir"),
                new CaseCredit("Sanat Yönetmeni", "Eda Karaman"),
                new CaseCredit("Performans", "Hazal Üstün, Erkin Tekin"),
                new CaseCredit("Veri & Attribution", "Onur Çetin")
            },
            Metrics: new[]
            {
                new CaseMetric("2.7×", "MQL artışı"),
                new CaseMetric("−31%", "CPM"),
                new CaseMetric("+22%", "Deal velocity"),
                new CaseMetric("11", "Kapanan enterprise hesap")
            }),

        // ========= LUME COFFEE =========
        new CaseStudy(
            Slug: "lume-coffee",
            Brand: "Lume Coffee",
            Tagline: "Tek çekirdek, çok hikâye.",
            Category: "Ambalaj · Mağaza deneyimi",
            Industry: "Specialty Coffee · F&B",
            Year: "2025",
            Duration: "10 hafta",
            Location: "İstanbul",
            CoverClass: "cover-3",
            Mark: "lume",
            Badge: "Specialty",
            Lede: "İstanbul'da üç şubeli specialty coffee markası için ambalaj sistemi, mağaza içi iletişim ve dijital içerik stratejisi. Tek bardak satışı %28 arttı, abonelik 1.200 kullanıcıya ulaştı.",
            HeroImage: ImgBase + "creida-lume-hero/1800/1100",
            Services: new[] { "Ambalaj sistemi", "Mağaza içi tasarım", "Sosyal medya kreatif", "İçerik prodüksiyonu", "Menü tasarımı" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Lume, kahve dünyasında derin uzmanlığa sahipti — yıllık 4 ton çekirdek ithal eden, baristaları yarışmalardan dönen bir ekip. Ama bu uzmanlık sokaktaki müşteriye geçmiyordu. Ambalaj kalabalık, mağazadaki bilgi katmanları okunaksızdı; \"specialty\" hissi sadece içerken vardı, satın alırken kayboluyordu."),
                new CaseSection("Yaklaşım",
                    "Her çekirdek için tek bir hikâye, tek bir görsel kod, tek bir dokunsal his. Ambalajı 9 farklı varyanttan 3 ana sisteme (Daily / Reserve / Limited) indirdik; her sistem kendi rengi, kâğıt dokusu, etiket diliyle. Mağazadaki menüyü %60 kısalttık, baristaların sözlü brifingleri için arka kart sistemi geliştirdik."),
                new CaseSection("Sonuç",
                    "Tek bardak satışı (yüksek marjlı kalem) %28 arttı, abonelik programına 6 ayda 1.200 kayıt geldi; Instagram'da takipçi 18K'dan 64K'ya çıktı. Daha önemlisi, ortalama sepet 87 TL'den 124 TL'ye yükseldi.")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Mağaza saha çalışması",
                    "Üç şubede üç tam gün gözlem. 47 müşteri kısa görüşmesi, 8 barista derinlemesine. \"Ne sorulduğunda donuyorlar\" sorusunu çözmek için."),
                new CaseProcessStep("02", "Sistem mimarisi",
                    "SKU sadeleştirme: 9 → 3 kategori. Her kategori için palet, tipografi, kâğıt, mürekkep ve etiket alanı sabitlendi."),
                new CaseProcessStep("03", "Mağaza içi yenileme",
                    "Menü panoları, masa kartları, paket alım kutusu, baristaların önündeki bilgi kartları, kahve detay etiketleri."),
                new CaseProcessStep("04", "Sosyal içerik",
                    "Her hafta bir çekirdek hikayesi: kaynaktan, sürecten, baristadan. Reels formatı için bir günde çekim takvimi.")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-lume-g1/1400/900", "Daily / Reserve / Limited — üç ambalaj sistemi."),
                new CaseGalleryImage(ImgBase + "creida-lume-g2/1400/900", "Kadıköy şubesinde yeni menü panosu."),
                new CaseGalleryImage(ImgBase + "creida-lume-g3/900/1100", "Çekirdek hikayesi içerik formatı.")
            },
            Quote: new CaseQuote(
                "Üç haftada ambalaj depomuz yarı yarıya küçüldü. Müşteri \"reserve nedir?\" diye sormayı bıraktı, doğrudan ona uzanmaya başladı.",
                "Hakan Tezcan",
                "Kurucu, Lume Coffee"),
            Credits: new[]
            {
                new CaseCredit("Kreatif Direktör", "Eda Karaman"),
                new CaseCredit("Ambalaj", "Tuna Bakırcıoğlu, Studio Kavak"),
                new CaseCredit("İçerik & Sosyal", "Yiğit Solmaz, Naz Sungur"),
                new CaseCredit("Fotoğraf", "Cem Kütük"),
                new CaseCredit("Mağaza içi", "Selim Aydın")
            },
            Metrics: new[]
            {
                new CaseMetric("+28%", "Bardak satışı"),
                new CaseMetric("1.2K", "Abone"),
                new CaseMetric("3.6×", "IG takipçi"),
                new CaseMetric("9 → 3", "SKU sadeleşme")
            }),

        // ========= VERIS FINANS =========
        new CaseStudy(
            Slug: "veris-finans",
            Brand: "Veris Finans",
            Tagline: "Para korkutmaz, açıklar.",
            Category: "Marka tazeleme · Kullanıcı edinimi",
            Industry: "Fintech · Mobil Yatırım",
            Year: "2025",
            Duration: "8 hafta + sürekli optimizasyon",
            Location: "İstanbul",
            CoverClass: "cover-4",
            Mark: "veris",
            Badge: "Fintech",
            Lede: "Yatırım uygulaması için marka tazeleme ve genç yatırımcıyı hedefleyen 8 haftalık performans kampanyası. CPI %46 düştü, aktivasyon oranı %19'dan %34'e çıktı.",
            HeroImage: ImgBase + "creida-veris-hero/1800/1100",
            Services: new[] { "Marka tazeleme", "Reklam kreatifi", "Performance media", "Landing page CRO", "İllüstrasyon sistemi" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Veris, jenerik fintech görseli kullanıyordu: yeşil-kırmızı oklar, ciddi yüzlü bankacılar, BIST grafikleri. Hedef kitle 22–32 yaş için bu görsel kod hem korkutucu hem yorucuydu — ürünü indirdiklerinde aktivasyon oranı %19'da takılı kalıyordu."),
                new CaseSection("Yaklaşım",
                    "\"Para korkutmaz, açıklar.\" Konseptiyle finansal kavramları sade illüstrasyonlar (özelleştirilmiş bir illüstrasyon sistemi kurduk) ve günlük dille anlattık. Reklam kreatiflerini her hafta yeniliyor, kreatif başına 4 varyant ile A/B test ediyoruz. Onboarding'i 7 adımdan 3'e indirdik."),
                new CaseSection("Sonuç",
                    "Uygulama indirme maliyeti 4 haftada %46 düştü, yeni kullanıcıların ilk yatırıma geçme oranı %19'dan %34'e çıktı. 12 binin üzerinde yeni yatırımcı, ortalama ilk işlem hacmi 1.450 TL.")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Kullanıcı görüşmeleri",
                    "İndirip aktive olmayan 18 kişi, aktive olup işlem yapmayan 14 kişi, düzenli işlem yapan 10 kişi. Onboarding ekranlarında nerede sıkıldıklarını saatlik olarak haritaladık."),
                new CaseProcessStep("02", "İllüstrasyon sistemi",
                    "20 kavram (faiz, kâr payı, kısa pozisyon, vade…) için tutarlı illüstrasyon dili. Sıcak palet, organik formlar, küçük karakterler."),
                new CaseProcessStep("03", "Onboarding yeniden",
                    "7 adım → 3 adım. Doğrulama akışı paralelleştirildi, ilk yatırım için 5.000 TL'lik karşılama bonusu eklendi."),
                new CaseProcessStep("04", "Sürekli kreatif",
                    "Haftalık 8 yeni reklam, iki günde bir performans incelemesi, durdurma / ölçekleme kararları aynı toplantıda.")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-veris-g1/900/1100", "Yeni onboarding ekranlarından bir kare."),
                new CaseGalleryImage(ImgBase + "creida-veris-g2/1400/900", "Reklam kreatifleri — sıcak palet, sade illüstrasyon."),
                new CaseGalleryImage(ImgBase + "creida-veris-g3/1400/900", "Mobil uygulamada yeni dashboard.")
            },
            Quote: new CaseQuote(
                "İndirme başına maliyet dört haftada yarıya indi. Asıl etkili olan onboarding'i sadeleştirmemiz oldu — reklam değil, ürünün kendisi konuşur hâle geldi.",
                "Ece Tunç",
                "Growth Lead, Veris"),
            Credits: new[]
            {
                new CaseCredit("Strateji", "Mert Aksoy"),
                new CaseCredit("İllüstrasyon", "Burç Karaca"),
                new CaseCredit("Sanat Yönetmeni", "Tuna Bakırcıoğlu"),
                new CaseCredit("Kopirayter", "Naz Sungur"),
                new CaseCredit("Performans", "Hazal Üstün, Erkin Tekin"),
                new CaseCredit("UX", "Selim Aydın")
            },
            Metrics: new[]
            {
                new CaseMetric("−46%", "CPI"),
                new CaseMetric("+78%", "Aktivasyon"),
                new CaseMetric("4.7×", "ROAS"),
                new CaseMetric("12K", "Yeni yatırımcı")
            }),

        // ========= MADEN SU PREMIUM =========
        new CaseStudy(
            Slug: "maden-su-premium",
            Brand: "Maden Su Premium",
            Tagline: "Sessiz lüks.",
            Category: "Konumlandırma · TV kampanyası",
            Industry: "FMCG · İçecek",
            Year: "2024",
            Duration: "16 hafta (strateji → yayın)",
            Location: "İstanbul · Bodrum çekim",
            CoverClass: "cover-5",
            Mark: "maden",
            Badge: "FMCG · TVC",
            Lede: "Premium maden suyu için yeni konumlandırma, TVC kampanyası ve restoran kanalına özel iletişim. Aided awareness %71'e ulaştı, HoReCa satış %19 arttı, Kristal Elma bronz.",
            HeroImage: ImgBase + "creida-maden-hero/1800/1100",
            Services: new[] { "Konumlandırma", "TVC (reklam filmi)", "OOH kampanyası", "HoReCa iletişim", "Sosyal medya destek" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Premium maden suyu pazarı son 5 yılda gösterişli, fizik metaforlu (fışkıran su, donma anı, bardakta yıldırım) reklamlarla doluydu. Markamız sessiz bir tarafa çekilmek istiyordu — fine dining masalarında değer önerisi netti, ama market rafında bu netlik kayboluyordu."),
                new CaseSection("Yaklaşım",
                    "\"Sessiz lüks.\" Yaklaşımıyla bir tek su damlasını çevreleyen sahneler tasarladık. Müzik yok, sadece doğal sesler — yağmur, taş, cam. OOH'larda tek bir kelime, tek bir renk: \"Sessiz.\" HoReCa için sommelier'lere yönelik özel materyal."),
                new CaseSection("Sonuç",
                    "Aided awareness 3 ay sonunda %52'den %71'e çıktı; premium kanalda (yiyecek-içecek) satış %19 arttı. Kampanya 2024 Kristal Elma'da \"Film – İçecek\" kategorisinde bronz aldı, Effie Türkiye'de finalist oldu.")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Konumlandırma",
                    "Sommelier'lerle 6 görüşme, fine dining'de tüketici gözlemi. \"Su konuşmamalı\" iç görüsü buradan doğdu."),
                new CaseProcessStep("02", "TVC senaryosu",
                    "30 saniye, üç senaryo: Yağmur, Cam, Taş. Üçü de aynı manifestoyu tekrar eder — sessiz olan değerlidir."),
                new CaseProcessStep("03", "Prodüksiyon",
                    "Bodrum'da 4 günlük çekim, ışık ve ses tasarımı için iki kıdemli ekip. Renk düzeltmesi Berlin'de."),
                new CaseProcessStep("04", "Lansman",
                    "TVC ana ve ikincil kanallar, OOH büyük şehir merkezleri, sosyal medya tekrar kesimleri. Eş zamanlı sommelier eğitim sunumu.")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-maden-g1/1400/900", "TVC'den kare: \"Sessiz\" anahtar görsel."),
                new CaseGalleryImage(ImgBase + "creida-maden-g2/1400/900", "OOH — şehrin yoğun caddelerinde tek kelime."),
                new CaseGalleryImage(ImgBase + "creida-maden-g3/900/1100", "Sommelier kılavuzu içinden bir sayfa.")
            },
            Quote: new CaseQuote(
                "Su kategorisinde olduğumuzu unuttuk. Bizim için artık ana metrik raf bilinirliği değil, sommelier'in masaya hangi şişeyi koyduğu.",
                "Cenk Akarca",
                "Marka Direktörü, Maden Su"),
            Credits: new[]
            {
                new CaseCredit("Yaratıcı Yönetmen", "Eda Karaman"),
                new CaseCredit("Stratejist", "Beril Demir"),
                new CaseCredit("Yönetmen (Film)", "Aris Manolopoulos"),
                new CaseCredit("Prodüksiyon", "Quay Films"),
                new CaseCredit("Müzik & Ses Tasarım", "Karbon Studio"),
                new CaseCredit("Medya Planlama", "Erkin Tekin")
            },
            Metrics: new[]
            {
                new CaseMetric("+19%", "HoReCa satış"),
                new CaseMetric("71%", "Aided awareness"),
                new CaseMetric("8.4M", "TV reach"),
                new CaseMetric("🏆", "Kristal Elma bronz")
            }),

        // ========= VOLTRA ELECTRIC =========
        new CaseStudy(
            Slug: "voltra-electric",
            Brand: "Voltra Electric",
            Tagline: "Şehir tekrar bizim olsun.",
            Category: "360° lansman · Performans",
            Industry: "Mobility · Elektrikli Scooter",
            Year: "2024",
            Duration: "20 hafta",
            Location: "İstanbul · Berlin (prodüksiyon)",
            CoverClass: "cover-6",
            Mark: "voltra",
            Badge: "Mobility · 360°",
            Lede: "Elektrikli scooter markası için 360° lansman: marka stratejisi, kampanya, ürün filmi, OOH ve dijital satın alma. İlk 60 günde 9.200 araç satıldı, dijital ROAS 3.9× oldu.",
            HeroImage: ImgBase + "creida-voltra-hero/1800/1100",
            Services: new[] { "Marka stratejisi", "Kampanya konsepti", "Ürün filmi", "OOH", "Performance media", "İçerik dizisi" },
            Sections: new[]
            {
                new CaseSection("Mesele",
                    "Şehir trafiğine alternatif sunan bir scooter markası, çevreci klişeden uzak ama hâlâ inandırıcı bir hikâye arıyordu. Rakipler ya teknik özelliklerle (kilowatt, menzil, ivmelenme) ya da \"yeşil gezegen\" mesajıyla konuşuyordu. Voltra ikisinden de sıyrılmak istiyordu."),
                new CaseSection("Yaklaşım",
                    "\"Şehir tekrar bizim olsun.\" Konseptiyle hem nostaljik hem kışkırtıcı bir kampanya kurguladık. Aktör yerine gerçek kuryeler, aktris yerine gerçek mahalleler. Film, üç şehirde (İstanbul, İzmir, Eskişehir) gerçek sokaklarda çekildi. Mesaj: \"Trafik bir mecburiyet değil, bir alışkanlık.\""),
                new CaseSection("Sonuç",
                    "İlk 60 günde 9.200 araç satıldı, abonelik (battery swap) müşterileri 2.100 kişi; OOH bilinirliği 4 büyükşehirde %42 sevdirilme oranına ulaştı. Dijital ROAS 3.9×, lansman sonrası 2 ay test sürüş randevusu %210 doluluğa ulaştı.")
            },
            Process: new[]
            {
                new CaseProcessStep("01", "Stratejik konum",
                    "Üç şehirde kullanıcı ve kurye gözlemi. \"Trafik bir mecburiyet değil\" konseptinin sözlü kanıtlarını topladık."),
                new CaseProcessStep("02", "Film konsepti",
                    "Üç senaryo. Seçilen senaryoda 8 gerçek kuryeyle bir hafta çalışma. Diyaloglar yapılandırılmış ama sözcükler onlardan."),
                new CaseProcessStep("03", "Çok kanallı lansman",
                    "TVC, OOH (vinyl + dijital), Spotify ses reklamları, Reels dizisi, mağaza içi iletişim, randevu sistemi UX."),
                new CaseProcessStep("04", "Performans",
                    "Meta, Google, TikTok'ta paralel satın alma. Test sürüş randevusu landing page CRO çalışması (3 iterasyon).")
            },
            Gallery: new[]
            {
                new CaseGalleryImage(ImgBase + "creida-voltra-g1/1400/900", "Film kapağı: gerçek bir Beyoğlu kuryesi."),
                new CaseGalleryImage(ImgBase + "creida-voltra-g2/900/1100", "OOH — alt sınıfsız bir manifesto görseli."),
                new CaseGalleryImage(ImgBase + "creida-voltra-g3/1400/900", "Mağaza içi randevu deneyimi.")
            },
            Quote: new CaseQuote(
                "Üç farklı ajansla görüştük; Creida tek soruyu sordu: \"Niye scooter satıyorsunuz?\" Cevabı kampanyaya yazdılar. Diğerleri brief'i alıp fiyat verdi.",
                "Görkem Önal",
                "CEO, Voltra"),
            Credits: new[]
            {
                new CaseCredit("Yaratıcı Yönetmen", "Eda Karaman"),
                new CaseCredit("Stratejist", "Mert Aksoy"),
                new CaseCredit("Film Yönetmeni", "Berke Çoban"),
                new CaseCredit("Sanat Yönetmeni", "Tuna Bakırcıoğlu"),
                new CaseCredit("Performans", "Hazal Üstün, Erkin Tekin"),
                new CaseCredit("Medya Satın Alma", "Onur Çetin")
            },
            Metrics: new[]
            {
                new CaseMetric("9.2K", "Satış"),
                new CaseMetric("2.1K", "Abone"),
                new CaseMetric("42%", "OOH sevdirilme"),
                new CaseMetric("3.9×", "Dijital ROAS")
            })
    };

    public static CaseStudy? FindBySlug(string slug) =>
        All.FirstOrDefault(c => string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));
}
