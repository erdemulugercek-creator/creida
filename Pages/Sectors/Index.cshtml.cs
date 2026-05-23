using Creida.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages.Sectors;

public class IndexModel : PageModel
{
    public IReadOnlyList<SectorPage> Sectors => Creida.Data.Sectors.All;

    public void OnGet() { }
}
