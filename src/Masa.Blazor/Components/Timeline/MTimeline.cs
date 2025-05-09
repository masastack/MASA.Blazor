namespace Masa.Blazor
{
    public class MTimeline : ThemeContainer
    {
        [Parameter] public bool AlignTop { get; set; }

        [Parameter] public bool Dense { get; set; }

        [Parameter] public bool Reverse { get; set; }

        private static Block _block = new("m-timeline");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();

        protected override IEnumerable<string> BuildComponentClass()
        {
            yield return _modifierBuilder
                .Add(AlignTop)
                .Add(Dense)
                .Add(Reverse)
                .AddTheme(ComputedTheme)
                .Build();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<bool>>(0);
            builder.AddAttribute(1, "Value", Reverse);
            builder.AddAttribute(2, "Name", "Reverse");
            builder.AddAttribute(3, "ChildContent", (RenderFragment)base.BuildRenderTree);
            builder.CloseComponent();
        }
    }
}