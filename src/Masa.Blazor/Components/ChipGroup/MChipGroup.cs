namespace Masa.Blazor;

public class MChipGroup : MSlideGroup
{
    public MChipGroup() : base(GroupType.ChipGroup)
    {
    }

    [Parameter]
    public bool Column
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(Column), (val) =>
        {
            if (val)
            {
                ScrollOffset = 0;
            }

            NextTick(OnResize);
        }, immediate: true);
    }

    private Block _block = new("m-chip-group");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.Modifier(Column).GenerateCssClasses()
        );
    }
}