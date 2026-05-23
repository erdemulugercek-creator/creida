using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Creida.Pages;

public class NotFoundModel : PageModel
{
    public string StatusCode { get; private set; } = "404";

    public void OnGet(string? code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            StatusCode = code;
        }
        Response.StatusCode = 404;
    }
}
