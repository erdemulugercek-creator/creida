# Creida — Reklam Ajansı

`creida.co` için ASP.NET Core Razor Pages (net10.0) ile yapılmış reklam ajansı sitesi.

## İçerik

- **Hero, Hizmetler, İşler, Hakkımızda, İletişim** — tek sayfa yapısı
- **İş detay sayfaları** — `/work/{slug}` üzerinden 6 case study
- **TR / EN iki dil** — header'daki TR/EN düğmesinden anlık değişim
- **İletişim formu + SMTP gönderim** — MailKit ile gerçek e-posta

## Çalıştırma

.NET 10 SDK gerekli.

```bash
cd creida
rm -rf bin obj    # eski derleme artıkları varsa
dotnet restore
dotnet run
```

Sonra: <http://localhost:5057>

### Geliştirme modu (otomatik yeniden yükleme)

```bash
dotnet watch --non-interactive run
```

`--non-interactive` flag'i önemli: C# dosyalarındaki "rude edit"lerde dotnet watch sana sormadan otomatik restart eder. CSS ve Razor değişiklikleri tarayıcıyı kendi kendine yeniler.

## "Coming Soon" modu

Sitenin halka sadece "Yakında" sayfasını göstermesini istiyorsan `appsettings.json` içinde:

```json
"Site": {
  "ComingSoonMode": true,
  "PreviewToken": "creida-2026-preview"
}
```

- **`ComingSoonMode: true`** → her ziyaretçi otomatik `/coming-soon` sayfasına yönlenir
- **`ComingSoonMode: false`** → tam site açılır (her şey yerinde, hiçbir şey silinmez)

### Ekibin tam siteyi görmesi

Coming Soon modu açıkken sen ya da ekip tam siteyi şu şekilde görebilirsiniz:

```
https://creida.co/preview/creida-2026-preview
```

Bu URL'i bir kere ziyaret edince tarayıcına 30 gün geçerli bir cookie düşer; bundan sonra normal site açılır.

Cookie'yi silip tekrar Coming Soon'a dönmek için:

```
https://creida.co/preview/exit
```

### Tam siteyi yayına almak

Sadece `ComingSoonMode` değerini `false` yap, dotnet watch'ı yeniden başlat (ya da prod sunucusunu yenile). Tüm site bir saniyede halka açılır.

## SMTP / e-posta kurulumu

`appsettings.json` içindeki `Smtp` bölümü boş; brief gönderildiğinde **şu an mail çıkmaz, sadece log'a yazılır.** Gerçek SMTP bilgilerinin koda commit edilmemesi için **User Secrets** kullan:

```bash
cd creida
dotnet user-secrets init
dotnet user-secrets set "Smtp:Username" "hello@creida.co"
dotnet user-secrets set "Smtp:Password" "uygulama-sifren"
```

**Gmail için:** Google hesabında 2FA aç, sonra <https://myaccount.google.com/apppasswords> üzerinden bir "uygulama şifresi" oluştur ve `Smtp:Password` olarak onu kullan.

**Başka sağlayıcılar:**
- SendGrid: `Host=smtp.sendgrid.net Port=587 Username=apikey Password=<API_KEY>`
- Mailgun: `Host=smtp.mailgun.org Port=587 Username=postmaster@... Password=<smtp pass>`
- Resend: `Host=smtp.resend.com Port=465 UseStartTls=false`

Production'da bu değerleri **çevre değişkeni** olarak ver:

```bash
export Smtp__Username="..."
export Smtp__Password="..."
```

## Dil eklemek / çeviriyi düzenlemek

Çeviriler `Localization/Resources/tr.json` ve `en.json` altında. Yeni anahtar eklemek için:

1. `tr.json` ve `en.json`'a aynı `key`'i ekle
2. Razor'da `@L["Senin.Anahtarın"]` şeklinde kullan

Yeni dil eklemek için (`de.json` örneği):

1. `Localization/Resources/de.json` oluştur
2. `Program.cs`'te `supportedCultures` listesine `new CultureInfo("de")` ekle
3. Header'daki switcher'da üçüncü dile yer açmak istersen `_Layout.cshtml`'i güncelle

> Not: Hizmetler listesi (Marka & Kimlik vb.) ve Hakkımızda manifesto'su şu anda hâlâ Türkçe sabit. Çevirmek istersen Index.cshtml'deki bu bölümleri de `@L[...]` ile değiştirebilirsin.

## Case study eklemek

`Data/CaseStudies.cs` içindeki `All` listesine yeni bir `CaseStudy` ekle. Index ve detay sayfaları otomatik olarak ondan beslenir.

## Proje yapısı

```
creida/
├── Creida.csproj
├── Program.cs                  (DI, localization, smtp, culture endpoint)
├── appsettings.json            (SMTP slot — secret'ı buraya YAZMA)
├── Data/CaseStudies.cs         (case data)
├── Localization/
│   ├── JsonStringLocalizer.cs  (JSON tabanlı localizer)
│   └── Resources/
│       ├── tr.json
│       └── en.json
├── Services/EmailService.cs    (MailKit SMTP)
├── Pages/
│   ├── _ViewImports.cshtml     (IViewLocalizer injection)
│   ├── _ViewStart.cshtml
│   ├── Index.cshtml(.cs)       (ana sayfa + form)
│   ├── Error.cshtml(.cs)
│   ├── Work/
│   │   └── Detail.cshtml(.cs)  (/work/{slug})
│   └── Shared/_Layout.cshtml
└── wwwroot/
    ├── css/site.css
    ├── js/site.js
    └── robots.txt
```

## Yayına alma seçenekleri

| Platform | Notlar |
|---|---|
| Azure App Service | `dotnet publish -c Release` + Azure publish profile |
| Linux VPS | publish + `nginx` reverse proxy + `systemd` |
| Docker | `mcr.microsoft.com/dotnet/aspnet:10.0` tabanlı image |

## Genişletme fikirleri

- Case study görselleri (`wwwroot/img/cases/{slug}.jpg`) ve `.work-cover` arka planını CSS yerine gerçek görsel yap
- Blog / insights bölümü (Markdown veya headless CMS)
- Form gönderiminden sonra teşekkür sayfası + `dataLayer` push (GTM)
- Slack webhook (e-posta yerine veya yanında)
- DB'ye lead yazma (`Microsoft.EntityFrameworkCore` + PostgreSQL)
