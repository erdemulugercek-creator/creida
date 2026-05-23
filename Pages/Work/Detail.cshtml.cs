using Creida.Data;
using Creida.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages.Work;

public class DetailModel : PageModel
{
    private readonly CaseStudiesService _cases;

    public CaseStudy Case { get; private set; } = default!;
    public CaseStudy? Next { get; private set; }

    public DetailModel(CaseStudiesService cases) => _cases = cases;

    public IActionResult OnGet(string slug)
    {
        var match = _cases.BySlug(slug);
        if (match is null)
        {
            return NotFound();
        }

        Case = match;

        var all = _cases.All();
        var index = all.FindIndex(c => c.Slug == match.Slug);
        if (index >= 0 && all.Count > 1)
        {
            var nextIndex = (index + 1) % all.Count;
            Next = all[nextIndex];
        }

        return Page();
    }
}
