using Creida.Data;
using Creida.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages;

public class TeamModel : PageModel
{
    private readonly TeamService _team;
    public IReadOnlyList<TeamMember> Members { get; private set; } = Array.Empty<TeamMember>();

    public TeamModel(TeamService team) => _team = team;

    public void OnGet()
    {
        Members = _team.All();
    }
}
