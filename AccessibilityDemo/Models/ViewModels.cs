using System.ComponentModel.DataAnnotations;

namespace AccessibilityDemo.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class SearchViewModel
{
    public string Query { get; set; } = "";
    public IReadOnlyList<Principle> Results { get; set; } = [];
}

/// <summary>
/// Used by the contrast checker on the "Distinguishable" page.
/// </summary>
public class ContrastViewModel
{
    [Display(Name = "Text color")]
    public string Foreground { get; set; } = "#595959";

    [Display(Name = "Background color")]
    public string Background { get; set; } = "#ffffff";

    public double? Ratio { get; set; }
    public string? Error { get; set; }
}

/// <summary>
/// The workshop registration form on the "Avoid and correct mistakes" page.
/// The data-annotation messages are written to tell the user how to FIX the
/// problem, not just that there is one.
/// </summary>
public class RegistrationViewModel
{
    [Required(ErrorMessage = "Enter your full name, for example Jane Smith.")]
    [StringLength(80, ErrorMessage = "Your name must be 80 characters or fewer.")]
    [Display(Name = "Full name")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Enter your email address, for example name@example.com.")]
    [EmailAddress(ErrorMessage = "Enter an email address in the correct format, like name@example.com.")]
    [Display(Name = "Email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Enter your student ID: the letter E followed by 8 digits, for example E00123456.")]
    [RegularExpression(@"^[Ee]\d{8}$",
        ErrorMessage = "Your student ID must be the letter E followed by 8 digits, for example E00123456.")]
    [Display(Name = "Student ID")]
    public string? StudentId { get; set; }

    [Required(ErrorMessage = "Choose a workshop session.")]
    [Display(Name = "Workshop session")]
    public string? Session { get; set; }

    [Display(Name = "Accommodations needed (optional)")]
    [StringLength(500, ErrorMessage = "Accommodations must be 500 characters or fewer.")]
    public string? Accommodations { get; set; }

    public static IReadOnlyList<string> Sessions { get; } =
    [
        "Monday 10:00 AM – Screen reader basics",
        "Wednesday 2:00 PM – Keyboard testing",
        "Friday 1:00 PM – Automated accessibility tools"
    ];
}
