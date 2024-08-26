using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Extensions;

public static class ElementReferenceExtensions
{
    public static string GetSelector(this ElementReference elementReference)
    {
        if (elementReference.TryGetSelector(out var selector))
        {
            return selector;
        }

        throw new InvalidOperationException(
            "ElementReference has not been configured correctly. This issue may occur during pre-rendering; it can be ignored, or you can disable pre-rendering.");
    }

    public static bool TryGetSelector(this ElementReference elementReference, [NotNullWhen(true)] out string? selector)
    {
        selector = null;

        if (elementReference.Context is null)
        {
            return false;
        }

        selector = $"[_bl_{elementReference.Id}]";

        return true;
    }
}