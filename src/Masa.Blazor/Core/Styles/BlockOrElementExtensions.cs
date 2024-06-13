using BemIt.Abstracts;

namespace Masa.Blazor.Core;

public static class BlockOrElementExtensions
{
    public static string AppendClasses(this IBlockOrElement blockOrElement, params string?[] classes)
    {
        var sb = new StringBuilder(blockOrElement.Name);

        foreach (var @class in classes)
        {
            if (string.IsNullOrWhiteSpace(@class))
            {
                continue;
            }
            
            sb.Append(' ');
            sb.Append(@class);
        }
        
        return sb.ToString();
    }
    
    public static string AppendClasses(this IBlockOrElement blockOrElement, params (string? @class, bool condition)[] classes)
    {
        var sb = new StringBuilder(blockOrElement.Name);

        foreach (var (classValue, condition) in classes)
        {
            if (!condition || string.IsNullOrWhiteSpace(classValue))
            {
                continue;
            }
            
            sb.Append(' ');
            sb.Append(classValue);
        }
        
        return sb.ToString();
    }
}