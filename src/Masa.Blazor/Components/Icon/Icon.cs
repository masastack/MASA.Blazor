﻿namespace Masa.Blazor;

[GenerateOneOf]
public partial class Icon : OneOfBase<string, SvgPath, SvgPath[]>
{
    public bool IsSvg => IsT1 || IsT2;

    public SvgPath[] GetSvgPaths()
    {
        if (IsT0)
        {
            return Array.Empty<SvgPath>();
        }

        return IsT2 ? AsT2 : new[] { AsT1 };
    }
}