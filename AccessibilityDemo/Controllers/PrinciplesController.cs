using AccessibilityDemo.Infrastructure;
using AccessibilityDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AccessibilityDemo.Controllers;

/// <summary>
/// One action (and one view) per accessibility principle. Most pages are static
/// demonstrations; the Distinguishable, Predictable, and InputAssistance pages
/// also show server-side behavior (a contrast calculator, a "Go" button, and an
/// accessible validated form with a review step).
/// </summary>
public class PrinciplesController(IWebHostEnvironment env) : Controller
{
    /// <summary>
    /// Before every action, look up the principle with the same name as the action
    /// and hand it to the layout, which uses it for the page title, breadcrumbs,
    /// intro text, and previous/next links.
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"]?.ToString();
        ViewData["Principle"] = PrincipleCatalog.Find(action);
        base.OnActionExecuting(context);
    }

    public IActionResult Index() => RedirectToAction("Index", "Home");

    // ----- Perceivable -----------------------------------------------------

    public IActionResult TextAlternatives() => View();

    public IActionResult Captions()
    {
        // The video player is only shown if a sample video has been added.
        // See the README for how to add one.
        ViewData["HasVideo"] = System.IO.File.Exists(Path.Combine(env.WebRootPath, "media", "sample.mp4"));
        return View();
    }

    public IActionResult Adaptable() => View();

    /// <summary>
    /// The page includes a contrast checker. It is a plain GET form, so it works
    /// without JavaScript and the result can be bookmarked or shared.
    /// </summary>
    public IActionResult Distinguishable(ContrastViewModel model)
    {
        if (!ColorContrast.TryParse(model.Foreground, out _) || !ColorContrast.TryParse(model.Background, out _))
        {
            model.Error = "Enter both colors as hex values, for example #333333 or #333.";
        }
        else
        {
            model.Ratio = ColorContrast.Ratio(model.Foreground, model.Background);
        }
        return View(model);
    }

    // ----- Operable --------------------------------------------------------

    public IActionResult Keyboard() => View();

    public IActionResult EnoughTime() => View();

    public IActionResult Seizures() => View();

    public IActionResult Navigable() => View();

    public IActionResult InputModalities() => View();

    // ----- Understandable --------------------------------------------------

    public IActionResult Readable() => View();

    public IActionResult Predictable() => View();

    /// <summary>
    /// Target of the "Jump to a principle" form on the Predictable page. The page
    /// only changes when the user presses "Go" – never just because a <select>
    /// value changed.
    /// </summary>
    [HttpGet]
    public IActionResult Go(string? target)
    {
        var principle = PrincipleCatalog.Find(target);
        return principle is null
            ? RedirectToAction(nameof(Predictable))
            : RedirectToAction(principle.Action);
    }

    [HttpGet]
    public IActionResult InputAssistance() => View(new RegistrationViewModel());

    /// <summary>
    /// Step 1 of the form. If the data is valid, show a review page instead of
    /// saving immediately ("opportunity to review, correct, or reverse").
    /// The same action handles the "Change my answers" button from the review page.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult InputAssistance(RegistrationViewModel model, string? command)
    {
        if (command == "edit")
        {
            // Coming back from the review page: show the form with the user's
            // answers kept, but without error messages.
            ModelState.Clear();
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return View("Review", model);
    }

    /// <summary>Step 2: the user confirmed on the review page.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm(RegistrationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Principle"] = PrincipleCatalog.Find(nameof(InputAssistance));
            return View(nameof(InputAssistance), model);
        }

        // A real app would save to a database here. We use TempData so the
        // confirmation page survives the redirect (Post/Redirect/Get pattern,
        // which also prevents a duplicate submission if the user refreshes).
        TempData["RegisteredName"] = model.FullName;
        TempData["RegisteredSession"] = model.Session;
        return RedirectToAction(nameof(Registered));
    }

    [HttpGet]
    public IActionResult Registered()
    {
        ViewData["Principle"] = PrincipleCatalog.Find(nameof(InputAssistance));
        if (TempData.Peek("RegisteredName") is null)
        {
            return RedirectToAction(nameof(InputAssistance));
        }
        return View();
    }

    /// <summary>Lets the user reverse the submission.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CancelRegistration()
    {
        TempData.Remove("RegisteredName");
        TempData.Remove("RegisteredSession");
        TempData["Message"] = "Your registration was cancelled. Nothing was saved.";
        return RedirectToAction(nameof(InputAssistance));
    }

    // ----- Robust ----------------------------------------------------------

    public IActionResult Compatible() => View();
}
