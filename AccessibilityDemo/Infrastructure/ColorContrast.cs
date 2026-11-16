using System.Globalization;

namespace AccessibilityDemo.Infrastructure;

/// <summary>
/// Implements the WCAG contrast-ratio formula.
/// https://www.w3.org/WAI/WCAG22/Understanding/contrast-minimum.html
///
///   ratio = (L1 + 0.05) / (L2 + 0.05)
///
/// where L1 is the relative luminance of the lighter color and L2 the darker.
/// Ratios range from 1:1 (no contrast) to 21:1 (black on white).
/// </summary>
public static class ColorContrast
{
    /// <summary>Minimum ratio for normal-size text at level AA.</summary>
    public const double AaNormalText = 4.5;

    /// <summary>Minimum ratio for large text (24px, or 18.66px bold) and UI components at level AA.</summary>
    public const double AaLargeText = 3.0;

    /// <summary>Minimum ratio for normal-size text at level AAA.</summary>
    public const double AaaNormalText = 7.0;

    public static double Ratio(string foregroundHex, string backgroundHex)
    {
        var l1 = RelativeLuminance(foregroundHex);
        var l2 = RelativeLuminance(backgroundHex);
        var lighter = Math.Max(l1, l2);
        var darker = Math.Min(l1, l2);
        return (lighter + 0.05) / (darker + 0.05);
    }

    public static double RelativeLuminance(string hex)
    {
        var (r, g, b) = Parse(hex);
        return 0.2126 * Linearize(r) + 0.7152 * Linearize(g) + 0.0722 * Linearize(b);
    }

    /// <summary>Accepts #rgb or #rrggbb (the leading # is optional).</summary>
    public static bool TryParse(string? hex, out (int R, int G, int B) rgb)
    {
        rgb = default;
        if (string.IsNullOrWhiteSpace(hex)) return false;

        var s = hex.Trim().TrimStart('#');
        if (s.Length == 3) s = string.Concat(s.Select(c => $"{c}{c}"));
        if (s.Length != 6 || !int.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value))
            return false;

        rgb = ((value >> 16) & 0xFF, (value >> 8) & 0xFF, value & 0xFF);
        return true;
    }

    /// <summary>Returns the color as a safe "#rrggbb" string (or null if invalid).</summary>
    public static string? Normalize(string? hex) =>
        TryParse(hex, out var c) ? $"#{c.R:x2}{c.G:x2}{c.B:x2}" : null;

    private static (int R, int G, int B) Parse(string hex) =>
        TryParse(hex, out var rgb) ? rgb : throw new FormatException($"'{hex}' is not a valid hex color.");

    // Convert an 8-bit sRGB channel to linear light.
    private static double Linearize(int channel)
    {
        var c = channel / 255.0;
        return c <= 0.04045 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}
