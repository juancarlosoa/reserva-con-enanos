using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RCE_Providers.Common;

public static class Slug
{
    public static string Generate(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var normalized = input.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();
        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }
        var withoutDiacritics = builder.ToString().Normalize(NormalizationForm.FormC);
        var replaced = Regex.Replace(withoutDiacritics, "[^a-z0-9]+", "-").Trim('-');
        replaced = Regex.Replace(replaced, "-+", "-");
        return replaced;
    }
}


