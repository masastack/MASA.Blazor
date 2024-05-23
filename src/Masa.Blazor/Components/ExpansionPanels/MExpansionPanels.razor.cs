namespace Masa.Blazor;

public partial class MExpansionPanels : MItemGroup
{
    public MExpansionPanels() : base(GroupType.ExpansionPanels)
    {
    }

    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public bool Accordion { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public bool Focusable { get; set; }

    [Parameter] public bool Flat { get; set; }

    [Parameter] public bool Hover { get; set; }

    [Parameter] public bool Inset { get; set; }

    [Parameter] public bool Popout { get; set; }

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public bool Tile { get; set; }

    public List<StringNumber?> NextActiveKeys { get; } = new();

    protected override List<StringNumber?> UpdateInternalValues(StringNumber? value)
    {
        var internalValues = InternalValues.ToList();

        if (internalValues.Contains(value))
        {
            internalValues.Remove(value);
            RemoveNextActiveKey(value);
        }
        else
        {
            if (!Multiple)
            {
                internalValues.Clear();
                NextActiveKeys.Clear();
            }

            if (Max == null || internalValues.Count < Max.TryGetNumber().number)
            {
                internalValues.Add(value);
                AddNextActiveKey(value);
            }
        }

        if (Mandatory && internalValues.Count == 0)
        {
            internalValues.Add(value);
            AddNextActiveKey(value);
        }

        return internalValues;
    }

    private void AddNextActiveKey(StringNumber? value)
    {
        var index = AllValues.IndexOf(value);
        if (index > 1)
        {
            NextActiveKeys.Add(AllValues[index - 1]);
        }
    }

    private void RemoveNextActiveKey(StringNumber? value)
    {
        var index = AllValues.IndexOf(value);
        if (index > 1)
        {
            NextActiveKeys.Remove(AllValues[index - 1]);
        }
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

    private Block _block = new("m-expansion-panels");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Accordion)
                .And(Flat)
                .And(Hover)
                .And(Focusable)
                .And(Inset)
                .And(Popout)
                .And(Tile)
                .GenerateCssClasses()
        );
    }
}