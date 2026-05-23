using Creida.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages.Insights;

public class ArticleModel : PageModel
{
    private readonly InsightsService _service;

    public ArticleModel(InsightsService service)
    {
        _service = service;
    }

    public InsightPost Post { get; private set; } = default!;
    public IReadOnlyList<InsightPost> OtherPosts { get; private set; } = Array.Empty<InsightPost>();

    public IActionResult OnGet(string slug)
    {
        var found = _service.FindBySlug(slug);
        if (found is null) return NotFound();

        Post = found;
        OtherPosts = _service.All()
            .Where(p => p.Meta.Slug != slug)
            .Take(3)
            .ToList();

        return Page();
    }
}
