using Creida.Data;
using Creida.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages.Sectors;

public class DetailModel : PageModel
{
    private readonly CaseStudiesService _cases;
    public SectorPage Sector { get; private set; } = default!;
    public IReadOnlyList<CaseStudy> SectorCases { get; private set; } = Array.Empty<CaseStudy>();

    public DetailModel(CaseStudiesService cases) => _cases = cases;

    public IActionResult OnGet(string slug)
    {
        var s = Creida.Data.Sectors.FindBySlug(slug);
        if (s is null) return NotFound();
        Sector = s;
        SectorCases = s.CaseSlugs
            .Select(cs => _cases.BySlug(cs))
            .Where(c => c is not null)
            .Select(c => c!)
            .ToList();
        return Page();
    }
}
