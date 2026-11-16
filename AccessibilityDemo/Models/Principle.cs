namespace AccessibilityDemo.Models;

/// <summary>
/// The four top-level WCAG principles ("POUR"). Every accessibility principle
/// in the course document belongs to one of these groups.
/// </summary>
public enum WcagGroup
{
    Perceivable,
    Operable,
    Understandable,
    Robust
}

/// <summary>
/// One accessibility principle from the W3C "Accessibility Principles" page.
/// </summary>
/// <param name="Action">The PrinciplesController action (and view name) that demonstrates it.</param>
/// <param name="Number">Order in which the principle appears in the document.</param>
/// <param name="Title">The heading used in the document.</param>
/// <param name="ShortTitle">A shorter label for navigation menus.</param>
/// <param name="Group">The WCAG principle (POUR) this belongs to.</param>
/// <param name="Summary">One sentence describing the principle.</param>
/// <param name="Examples">The bullet points listed in the document.</param>
public record Principle(
    string Action,
    int Number,
    string Title,
    string ShortTitle,
    WcagGroup Group,
    string Summary,
    IReadOnlyList<string> Examples);
