namespace Masa.Blazor
{
    public partial class MChipGroup : MSlideGroup
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

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder.Add("m-chip-group")
                              .AddIf("m-chip-group--column", () => Column);
                });
        }
    }
}
