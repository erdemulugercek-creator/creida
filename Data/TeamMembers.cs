namespace Creida.Data;

public record TeamMember(
    string Slug,
    string Name,
    string Role,
    string ShortRole,
    string Bio,
    string PhotoUrl,
    string Quote,
    IReadOnlyList<string> Highlights);

public static class TeamMembers
{
    // Portre görselleri pravatar.cc üzerinden (ücretsiz, anahtarsız).
    // Kendi profesyonel çekiminiz hazırsa PhotoUrl'i değiştirin.
    private const string AvatarBase = "https://i.pravatar.cc/600?img=";

    public static IReadOnlyList<TeamMember> All { get; } = new[]
    {
        new TeamMember(
            Slug: "erdem-ulugercek",
            Name: "Erdem Ulugerçek",
            Role: "Kurucu Ortak · Strateji",
            ShortRole: "Strateji",
            Bio: "İstanbul Üniversitesi İşletme mezunu. Kariyere uluslararası bir ajansta strateji stajyeri olarak başladı; sonraki dokuz yıl boyunca Türkiye'nin önde gelen tüketici ve B2B markalarına konumlandırma, mesaj mimarisi ve büyüme stratejileri üzerine danışmanlık verdi. Yöneticiliğini yaptığı projeler arasında ulusal bir bankanın marka yeniden konumlandırılması, bir B2B SaaS şirketinin Avrupa açılımı ve iki specialty marka lansmanı yer alıyor. 2024'te Creida'yı kreatif düşünce ile veri okuryazarlığını aynı masada tutma fikriyle kurdu. İşinin temelinde tek bir prensip var: doğru soruyu sormadan doğru cevabı bulmak mümkün değil.",
            PhotoUrl: AvatarBase + "12",
            Quote: "Brief'in ilk yarısında konuşmazsam, kalan yarısında doğru kreatifi yapamayız.",
            Highlights: new[]
            {
                "9+ yıl marka stratejisi",
                "B2B & DTC arasında köprü",
                "İstanbul Üniversitesi, İşletme"
            }),

        new TeamMember(
            Slug: "giray-can-mustu",
            Name: "Giray Can Muştu",
            Role: "Kurucu Ortak · Büyüme",
            ShortRole: "Büyüme",
            Bio: "Boğaziçi Üniversitesi Endüstri Mühendisliği mezunu. Kariyere bir fintech startup'ında büyüme analistliğiyle başladı; sonraki sekiz yılı performans pazarlaması, attribution kurulumu ve dönüşüm optimizasyonu üzerine yoğunlaştırdı. SaaS, fintech ve e-ticaret tarafında 40'tan fazla markaya çalıştı; Meta Business Partner ve Google Premier Partner sertifikalı bir ekibin baş analisti olarak görev aldı. Creida'da kreatife inanan ama her TL'nin nereye gittiğini bilmek isteyen müşterilerin yanında — özellikle 0-1 ölçeklenme aşamasında.",
            PhotoUrl: AvatarBase + "8",
            Quote: "Performans, kreatifin düşmanı değil; ona cesaret veren kanıttır.",
            Highlights: new[]
            {
                "8+ yıl performans pazarlaması",
                "40+ marka, $40M+ medya yönetimi",
                "Boğaziçi Üniversitesi, Endüstri Mühendisliği"
            }),

        new TeamMember(
            Slug: "begum-yasli",
            Name: "Begüm Yaşlı",
            Role: "Kurucu Ortak · Creative Director",
            ShortRole: "Creative",
            Bio: "Mimar Sinan Güzel Sanatlar Üniversitesi Grafik Tasarım mezunu. Yedi yıl bağımsız tasarım stüdyolarında sanat yönetmeni olarak çalıştı; özellikle ambalaj sistemleri, marka kimliği ve editöryel tasarımda derinleşti. Çalışmaları Brand New, It's Nice That ve Türkiye Tasarım Vakfı yıllıklarında yer aldı; ikisi Type Directors Club'tan, biri D&AD'den ödül aldı. Begüm'e göre tasarımın görevi gösteri yapmak değil, doğru kararı görünür kılmak. Creida'da görsel dilin mimarisini kuruyor — sade ama sıcak, sistemli ama esnek bir tasarım iskeleti.",
            PhotoUrl: AvatarBase + "47",
            Quote: "Bir kimlik sistemi, ekipten birinin onu unutması ne kadar kolaysa o kadar iyidir.",
            Highlights: new[]
            {
                "7+ yıl sanat yönetmenliği",
                "TDC + D&AD ödüllü",
                "Mimar Sinan Üniversitesi, Grafik Tasarım"
            })
    };
}
