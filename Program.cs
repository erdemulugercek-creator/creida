using System.Globalization;
using Creida.Data;
using Creida.Localization;
using Creida.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// --- E-posta (SMTP) ---
var smtpOptions = builder.Configuration.GetSection("Smtp").Get<SmtpOptions>() ?? new SmtpOptions();
builder.Services.AddSingleton(smtpOptions);
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// --- Insights (markdown tabanlı yazı arşivi) ---
builder.Services.AddSingleton<InsightsService>();

// --- İçerik servisleri (JSON tabanlı, salt okunur) ---
builder.Services.AddSingleton<ContentStore>();
builder.Services.AddSingleton<TeamService>();
builder.Services.AddSingleton<SiteSettingsService>();
builder.Services.AddSingleton<CaseStudiesService>();
builder.Services.AddSingleton<AppearanceService>();

// --- Localization (JSON tabanlı kendi provider'ımız) ---
var resourcesPath = Path.Combine(builder.Environment.ContentRootPath, "Localization", "Resources");
builder.Services.AddSingleton<IStringLocalizerFactory>(new JsonStringLocalizerFactory(resourcesPath, "tr"));
builder.Services.AddSingleton(typeof(IStringLocalizer<>), typeof(JsonStringLocalizerOfT<>));

var supportedCultures = new[] { new CultureInfo("tr"), new CultureInfo("en") };
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    opts.DefaultRequestCulture = new RequestCulture("tr");
    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
});

builder.Services
    .AddRazorPages()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// 404 (ve diğer status code) sayfasını ele al — geliştirmede de güzel görünsün
app.UseStatusCodePagesWithReExecute("/NotFound", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization();

app.UseAuthorization();

// "Coming Soon" modu — Site:ComingSoonMode true ise sadece /coming-soon
// görünür; ekip "preview" cookie ile tam siteyi görebilir.
app.Use(async (ctx, next) =>
{
    var cfg = ctx.RequestServices.GetRequiredService<IConfiguration>();
    var comingSoon = cfg.GetValue<bool>("Site:ComingSoonMode");

    if (!comingSoon)
    {
        await next();
        return;
    }

    var path = ctx.Request.Path.Value ?? "/";
    var hasPreview = ctx.Request.Cookies.ContainsKey("creida-preview");

    var allowed = new[] { "/coming-soon", "/preview", "/sitemap.xml", "/robots.txt", "/culture/set", "/NotFound", "/css", "/js", "/img", "/lib", "/fonts", "/favicon" };
    var isAllowed = allowed.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase));

    if (hasPreview || isAllowed)
    {
        await next();
        return;
    }

    ctx.Response.Redirect("/coming-soon");
});

// Önizleme erişimi: /preview/<token> → preview cookie set edip ana sayfaya yönlendir
app.MapGet("/preview/{token}", (HttpContext ctx, string token, IConfiguration cfg) =>
{
    var expected = cfg["Site:PreviewToken"];
    if (!string.IsNullOrEmpty(expected) && token == expected)
    {
        ctx.Response.Cookies.Append("creida-preview", "1", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            IsEssential = true
        });
        return Results.Redirect("/");
    }
    return Results.NotFound();
});

// Preview cookie temizleme — ekipten biri test etmek isterse
app.MapGet("/preview/exit", (HttpContext ctx) =>
{
    ctx.Response.Cookies.Delete("creida-preview");
    return Results.Redirect("/coming-soon");
});

// Dil değiştirme uç noktası: /culture/set?c=en&returnUrl=/
app.MapGet("/culture/set", (HttpContext ctx, string c, string? returnUrl) =>
{
    if (c != "tr" && c != "en") c = "tr";
    ctx.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(c)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, SameSite = SameSiteMode.Lax }
    );
    return Results.LocalRedirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
});

// Sitemap — case'leri ve insight'ları otomatik içerir
app.MapGet("/sitemap.xml", (InsightsService insights, CaseStudiesService cases) =>
{
    var origin = "https://creida.co";
    var today = DateTime.UtcNow.ToString("yyyy-MM-dd");

    var urls = new List<(string Loc, string LastMod, double Priority)>
    {
        ($"{origin}/",            today, 1.0),
        ($"{origin}/process",     today, 0.8),
        ($"{origin}/team",        today, 0.8),
        ($"{origin}/insights",    today, 0.8),
        ($"{origin}/sectors",     today, 0.8),
        ($"{origin}/pricing",     today, 0.8),
        ($"{origin}/careers",     today, 0.7),
        ($"{origin}/results",     today, 0.7),
        ($"{origin}/availability",today, 0.7),
        ($"{origin}/report",      today, 0.7),
        ($"{origin}/brief",       today, 0.7),
        ($"{origin}/manifesto",   today, 0.6),
        ($"{origin}/press",       today, 0.4),
        ($"{origin}/resources",   today, 0.4)
    };

    foreach (var c in cases.All())
        urls.Add(($"{origin}/work/{c.Slug}", today, 0.7));

    foreach (var s in Creida.Data.Sectors.All)
        urls.Add(($"{origin}/sectors/{s.Slug}", today, 0.7));

    foreach (var p in insights.All())
        urls.Add(($"{origin}/insights/{p.Meta.Slug}", p.Meta.Date.ToString("yyyy-MM-dd"), 0.6));

    var sb = new System.Text.StringBuilder();
    sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
    foreach (var (loc, lastMod, priority) in urls)
    {
        sb.AppendLine("  <url>");
        sb.AppendLine($"    <loc>{System.Net.WebUtility.HtmlEncode(loc)}</loc>");
        sb.AppendLine($"    <lastmod>{lastMod}</lastmod>");
        sb.AppendLine($"    <priority>{priority.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)}</priority>");
        sb.AppendLine("  </url>");
    }
    sb.AppendLine("</urlset>");

    return Results.Content(sb.ToString(), "application/xml; charset=utf-8");
});

app.MapRazorPages();

app.Run();
