using System.Diagnostics;
using AccessibilityDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessibilityDemo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View(PrincipleCatalog.All);

    /// <summary>
    /// Site search – one of the "more than one way to find relevant pages"
    /// (the others are the navigation menu and the site map).
    /// </summary>
    public IActionResult Search(string? q)
    {
        var query = q?.Trim() ?? "";
        var model = new SearchViewModel
        {
            Query = query,
            Results = query.Length == 0 ? [] : PrincipleCatalog.Search(query)
        };
        return View(model);
    }

    public IActionResult SiteMap() => View(PrincipleCatalog.All);

    /// <summary>
    /// Friendly page for 404 and other HTTP status codes (wired up in Program.cs).
    /// </summary>
    [Route("Home/Status/{code:int}")]
    public IActionResult Status(int code)
    {
        Response.StatusCode = code;
        ViewData["StatusCode"] = code;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
