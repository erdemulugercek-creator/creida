using Creida.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages.Insights;

public class IndexModel : PageModel
{
    private readonly InsightsService _service;

    public IndexModel(InsightsService service)
    {
        _service = service;
    }

    public IReadOnlyList<InsightPost> Posts { get; private set; } = Array.Empty<InsightPost>();

    public void OnGet()
    {
        Posts = _service.All();
    }
}
