namespace AccessibilityDemo.Models;

/// <summary>
/// The single source of truth for the list of principles. The navigation menu,
/// home page, site map, breadcrumbs, previous/next links, and site search are all
/// generated from this list, which keeps navigation consistent on every page
/// (see the "Predictable" principle).
/// </summary>
public static class PrincipleCatalog
{
    public static IReadOnlyList<Principle> All { get; } =
    [
        new("TextAlternatives", 1, "Text Alternatives for Non-text Content", "Text alternatives",
            WcagGroup.Perceivable,
            "Provide text that conveys the same information as images, icons, charts, and controls.",
            [
                "Short equivalents for images, including icons, buttons, and graphics.",
                "Description of data represented on charts, diagrams, and illustrations.",
                "Brief descriptions of non-text content such as audio and video files.",
                "Labels for form controls, input, and other user interface components."
            ]),

        new("Captions", 2, "Captions and Other Alternatives for Multimedia", "Captions & multimedia",
            WcagGroup.Perceivable,
            "People who cannot hear audio or see video need alternatives.",
            [
                "Text transcripts and captions for audio content, such as recordings of a radio interview.",
                "Audio descriptions, which are narrations to describe important visual details in a video.",
                "Sign language interpretation of audio content, including relevant auditory experiences."
            ]),

        new("Adaptable", 3, "Content can be Presented in Different Ways", "Adaptable content",
            WcagGroup.Perceivable,
            "Mark up structure properly so browsers and assistive technologies can re-present it.",
            [
                "Headings, lists, tables, input fields, and content structures are marked-up properly.",
                "Sequences of information or instructions are independent of any presentation.",
                "Browsers and assistive technologies provide settings to customize the presentation."
            ]),

        new("Distinguishable", 4, "Content is Easier to See and Hear", "Easier to see & hear",
            WcagGroup.Perceivable,
            "Use sufficient contrast, never rely on color alone, and let text resize and reflow.",
            [
                "Color is not used as the only way of conveying information or identifying content.",
                "Default foreground and background color combinations provide sufficient contrast.",
                "When users resize text up to 400% or change text spacing, no information is lost.",
                "Text reflows in small windows (“viewports”) and when users make the text larger.",
                "Images of text are resizable, replaced with actual text, or avoided where possible.",
                "Users can pause, stop, or adjust the volume of audio that is played on a website.",
                "Background audio is low or can be turned off, to avoid interference or distraction."
            ]),

        new("Keyboard", 5, "Functionality is Available from a Keyboard", "Keyboard access",
            WcagGroup.Operable,
            "Everything that works with a mouse must also work with a keyboard alone.",
            [
                "All functionality that is available by mouse is also available by keyboard.",
                "Keyboard focus does not get trapped in any part of the content.",
                "Web browsers, authoring tools, and other tools provide keyboard support."
            ]),

        new("EnoughTime", 6, "Users Have Enough Time to Read and Use the Content", "Enough time",
            WcagGroup.Operable,
            "Let people stop, extend, or adjust time limits and pause moving content.",
            [
                "Stop, extend, or adjust time limits, except where necessary.",
                "Pause, stop, or hide moving, blinking, or scrolling content.",
                "Postpone or suppress interruptions, except where necessary.",
                "Re-authenticate when a session expires without losing data."
            ]),

        new("Seizures", 7, "Content Does Not Cause Seizures and Physical Reactions", "Seizures & motion",
            WcagGroup.Operable,
            "Avoid flashing content and let users switch off non-essential animation.",
            [
                "Do not include content that flashes at particular rates and patterns.",
                "Warn users before flashing content is presented and provide alternatives.",
                "Provide mechanisms to switch off animations unless they are essential."
            ]),

        new("Navigable", 8, "Users Can Easily Navigate, Find Content, and Determine Where They Are", "Navigation",
            WcagGroup.Operable,
            "Use clear titles, headings, landmarks, skip links, visible focus, and descriptive links.",
            [
                "Pages have clear titles and are organized using descriptive section headings.",
                "There is more than one way to find relevant pages within a set of web pages.",
                "Users are informed about their current location within a set of related pages.",
                "There are ways to bypass blocks of content that are repeated on multiple pages.",
                "The keyboard focus is visible, and the focus order follows a meaningful sequence.",
                "The purpose of a link is evident, ideally even when the link is viewed on its own."
            ]),

        new("InputModalities", 9, "Users Can Use Different Input Modalities Beyond Keyboard", "Touch, voice & gestures",
            WcagGroup.Operable,
            "Support touch, voice, and pointer users with simple alternatives to complex gestures.",
            [
                "Gestures that require dexterity or fine movement have alternatives that do not require high dexterity.",
                "Components are designed to avoid accidental activation, for example by providing undo functionality.",
                "Labels presented to users match corresponding object names in the code, to support activation by voice.",
                "Functionality that is activated by movement can also be activated through user interface components.",
                "Buttons, links, and other active components are large enough to make them easier to activate by touch."
            ]),

        new("Readable", 10, "Text is Readable and Understandable", "Readable text",
            WcagGroup.Understandable,
            "Declare languages, define unusual terms, and write in plain language.",
            [
                "Identifying the primary language of a web page, such as Arabic, Dutch, or Korean.",
                "Identifying the language of text passages, phrases, or other parts of a web page.",
                "Providing definitions for any unusual words, phrases, idioms, and abbreviations.",
                "Using the clearest and simplest language possible or providing simplified versions."
            ]),

        new("Predictable", 11, "Content Appears and Operates in Predictable Ways", "Predictable behavior",
            WcagGroup.Understandable,
            "Keep navigation and labels consistent and never change context without consent.",
            [
                "Navigation mechanisms that are repeated on multiple pages appear in the same place each time.",
                "User interface components that are repeated on web pages have the same labels each time.",
                "Significant changes on a web page do not happen without the consent of the user."
            ]),

        new("InputAssistance", 12, "Users are Helped to Avoid and Correct Mistakes", "Avoiding mistakes",
            WcagGroup.Understandable,
            "Give clear instructions, helpful error messages, and a chance to review before submitting.",
            [
                "Descriptive instructions, error messages, and suggestions for correction.",
                "Context-sensitive help for more complex functionality and interaction.",
                "Opportunity to review, correct, or reverse submissions if necessary."
            ]),

        new("Compatible", 13, "Content is Compatible with Current and Future User Tools", "Robust & compatible",
            WcagGroup.Robust,
            "Write valid markup and give custom widgets a name, role, and value.",
            [
                "Ensuring markup can be reliably interpreted, for instance by ensuring it is valid.",
                "Providing a name, role, and value for non-standard user interface components."
            ])
    ];

    public static Principle? Find(string? action) =>
        All.FirstOrDefault(p => string.Equals(p.Action, action, StringComparison.OrdinalIgnoreCase));

    public static Principle? Previous(Principle p) => All.FirstOrDefault(x => x.Number == p.Number - 1);

    public static Principle? Next(Principle p) => All.FirstOrDefault(x => x.Number == p.Number + 1);

    /// <summary>Simple case-insensitive search over titles, summaries, and examples.</summary>
    public static IReadOnlyList<Principle> Search(string query)
    {
        var terms = query.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return All.Where(p =>
        {
            var haystack = string.Join(' ', [p.Title, p.Summary, p.Group.ToString(), .. p.Examples]);
            return terms.All(t => haystack.Contains(t, StringComparison.OrdinalIgnoreCase));
        }).ToList();
    }
}
