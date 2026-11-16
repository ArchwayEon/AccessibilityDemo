using System.Net;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AccessibilityDemo.Infrastructure;

/// <summary>
/// Renders its contents as an HTML-encoded code listing so a view can show the
/// markup it is teaching without hand-escaping every &lt; and &gt;.
///
/// Usage in a view:
///   &lt;code-sample label="Accessible image"&gt;
///       &lt;img src="dog.jpg" alt="A golden retriever catching a frisbee"&gt;
///   &lt;/code-sample&gt;
///
/// The listing is focusable (tabindex="0") because it may scroll horizontally,
/// and keyboard users must be able to reach scrollable regions.
/// </summary>
[HtmlTargetElement("code-sample")]
public class CodeSampleTagHelper : TagHelper
{
    /// <summary>Accessible name for the listing, read by screen readers.</summary>
    public string Label { get; set; } = "Code example";

    /// <summary>Optional style: "good" or "bad".</summary>
    public string? Kind { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var raw = (await output.GetChildContentAsync()).GetContent();

        output.TagName = "pre";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", Kind is null ? "code" : $"code code-{Kind}");
        output.Attributes.SetAttribute("tabindex", "0");
        output.Attributes.SetAttribute("aria-label", Label);
        output.Content.SetHtmlContent($"<code>{WebUtility.HtmlEncode(Dedent(raw))}</code>");
    }

    // Remove blank leading/trailing lines and the common indentation so the
    // sample lines up nicely no matter how deeply it is nested in the view.
    private static string Dedent(string text)
    {
        var lines = text.Replace("\r\n", "\n").Split('\n').ToList();
        while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0])) lines.RemoveAt(0);
        while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1])) lines.RemoveAt(lines.Count - 1);

        var indent = lines.Where(l => l.Trim().Length > 0)
                          .Select(l => l.Length - l.TrimStart().Length)
                          .DefaultIfEmpty(0)
                          .Min();

        return string.Join('\n', lines.Select(l => l.Length >= indent ? l[indent..] : l.TrimStart()));
    }
}
