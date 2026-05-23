using System.ComponentModel.DataAnnotations;
using Creida.Data;
using Creida.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IEmailService _email;
    private readonly CaseStudiesService _cases;
    private readonly SiteSettingsService _settings;

    public IndexModel(ILogger<IndexModel> logger, IEmailService email,
        CaseStudiesService cases, SiteSettingsService settings)
    {
        _logger = logger;
        _email = email;
        _cases = cases;
        _settings = settings;
    }

    [BindProperty]
    public BriefForm Form { get; set; } = new();

    public bool Submitted { get; private set; }
    public string? SubmittedName { get; private set; }

    public IReadOnlyList<CaseStudy> Cases => _cases.All();
    public SiteSettings Site => _settings.Get();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _logger.LogInformation(
            "Yeni brief: {Name} <{Email}> — {Company} — {Service}",
            Form.Name, Form.Email, Form.Company, Form.Service);

        try
        {
            await _email.SendBriefAsync(Form.Name, Form.Email, Form.Company, Form.Service, Form.Message, ct);
        }
        catch (Exception ex)
        {
            // SMTP başarısız olsa bile kullanıcıya başarısızlık göstermiyoruz —
            // log'a yazıyoruz, ekibin görmesi yeterli. İstenirse hata ekranı eklenebilir.
            _logger.LogError(ex, "Brief mail gönderimi başarısız.");
        }

        Submitted = true;
        SubmittedName = Form.Name;

        ModelState.Clear();
        Form = new BriefForm();

        return Page();
    }
}

public class BriefForm
{
    [Required(ErrorMessage = "Lütfen adınızı yazın.")]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta gerekli.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    [StringLength(180)]
    public string Email { get; set; } = string.Empty;

    [StringLength(180)]
    public string? Company { get; set; }

    [StringLength(80)]
    public string? Service { get; set; }

    [Required(ErrorMessage = "Kısa da olsa bir brief yazın.")]
    [StringLength(4000, MinimumLength = 10, ErrorMessage = "Biraz daha ayrıntı verin (en az 10 karakter).")]
    public string Message { get; set; } = string.Empty;
}
