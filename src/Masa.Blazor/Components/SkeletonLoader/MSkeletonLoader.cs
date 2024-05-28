﻿using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public class MSkeletonLoader : ThemeContainer, IMeasurable
{
    [Parameter] public bool Boilerplate { get; set; }

    [Parameter] public bool Loading { get; set; }

    [Parameter] public bool Tile { get; set; }
    [Parameter] public string? Type { get; set; }

    [Parameter] public Dictionary<string, string>? Types { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public StringNumber? MaxHeight { get; set; }

    [Parameter] public StringNumber? MinHeight { get; set; }

    [Parameter] public StringNumber? Width { get; set; }

    [Parameter] public StringNumber? MaxWidth { get; set; }

    [Parameter] public StringNumber? MinWidth { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

    private Dictionary<string, string> RootTypes { get; set; } = new();

    private bool IsLoading => ChildContent is null || Loading;

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RootTypes = new Dictionary<string, string>
        {
            { "actions", "button@2" },
            { "article", "heading, paragraph" },
            { "avatar", "avatar" },
            { "button", "button" },
            { "card", "image, card-heading" },
            { "card-avatar", "image, list-item-avatar" },
            { "card-heading", "heading" },
            { "chip", "chip" },
            { "date-picker", "list-item, card-heading, divider, date-picker-options, date-picker-days, actions" },
            { "date-picker-options", "text, avatar@2" },
            { "date-picker-days", "avatar@28" },
            { "heading", "heading" },
            { "image", "image" },
            { "list-item", "text" },
            { "list-item-avatar", "avatar, text" },
            { "list-item-two-line", "sentences" },
            { "list-item-avatar-two-line", "avatar, sentences" },
            { "list-item-three-line", "paragraph" },
            { "list-item-avatar-three-line", "avatar, paragraph" },
            { "paragraph", "text@3" },
            { "sentences", "text@2" },
            { "table", "table-heading, table-thead, table-tbody, table-tfoot" },
            { "table-heading", "heading, text" },
            { "table-thead", "heading@6" },
            { "table-tbody", "table-row-divider@6" },
            { "table-row-divider", "table-row, divider" },
            { "table-row", "table-cell@6" },
            { "table-cell", "text" },
            { "table-tfoot", "text@2, avatar@2" },
            { "text", "text" },
            { "divider", "divider" },
        };
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }
#endif
        if (IsLoading && !Boilerplate)
        {
            Attributes["aria-busy"] = true;
            Attributes["aria-live"] = "polite";
            Attributes["role"] = "alert";
        }

        if (Types is not null)
        {
            foreach (var (key, value) in Types)
            {
                RootTypes.TryAdd(key, value);
            }
        }
    }

    protected override RenderFragment? GenChildContent()
        => IsLoading ? GenSkeleton() : ChildContent;

    private static Block _block = new("m-skeleton-loader");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(Boilerplate, IsLoading, Tile)
            .AddTheme(IsDark, IndependentTheme)
            .AddElevation(Elevation)
            .Build();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        var styleBuilder = StyleBuilder.Create();
        if (IsLoading)
        {
            styleBuilder.AddMeasurable(this);
        }

        return styleBuilder.GenerateCssStyles();
    }

    private RenderFragment GenSkeleton() => builder =>
    {
        var childrenHtml = string.Join("", GenStructure());
        builder.AddMarkupContent(0, childrenHtml);
    };

    private string Genbone(string text, List<string> children)
    {
        var divHtml = $"<div class=\"m-skeleton-loader__{text} m-skeleton-loader__bone\">";
        foreach (var child in children)
        {
            divHtml += child;
        }

        divHtml += "</div>";

        return divHtml;
    }

    private List<string> GenBones(string bones)
    {
        var children = new List<string>();
        var cutList = bones.Split('@').ToList();
        var bone = cutList.FirstOrDefault();
        var frequency = cutList.LastOrDefault();
        if (!string.IsNullOrWhiteSpace(bone) && !string.IsNullOrWhiteSpace(frequency))
        {
            for (int i = 0; i < int.Parse(frequency); i++)
            {
                children.AddRange(GenStructure(bone));
            }
        }

        return children;
    }

    private List<string> GenStructure(string? type = null)
    {
        var children = new List<string>();
        type ??= Type ?? "";
        RootTypes.TryGetValue(type, out var bone);

        if (type == bone)
        {
        }
        else if (type.Contains(","))
        {
            return MapBones(type);
        }
        else if (type.Contains("@"))
        {
            return GenBones(type);
        }
        else if (bone?.Contains(",") is true)
        {
            children = MapBones(bone);
        }
        else if (bone?.Contains("@") is true)
        {
            children = GenBones(bone);
        }
        else if (bone is not null)
        {
            children.AddRange(GenStructure(bone));
        }

        return new List<string> { Genbone(type, children) };
    }

    private List<string> MapBones(string bones)
    {
        var children = new List<string>();
        var types = bones.Replace(" ", "").Split(",");
        foreach (var type in types)
        {
            children.AddRange(GenStructure(type));
        }

        return children;
    }
}