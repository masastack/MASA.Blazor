namespace Masa.Blazor
{
    public class MVirtualScroll<TItem> : BVirtualScroll<TItem>, IMeasurable
    {
        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public StringNumber? MaxHeight { get; set; }

        [Parameter]
        public StringNumber? MaxWidth { get; set; }

        [Parameter]
        public StringNumber? MinHeight { get; set; }

        [Parameter]
        public StringNumber? MinWidth { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider.Apply(cssBuilder =>
            {
                cssBuilder.Add("m-virtual-scroll");
            }, styleBuilder =>
            {
                styleBuilder.AddMeasurable(this);
            })
            .Apply("container", cssBuilder =>
            {
                cssBuilder.Add("m-virtual-scroll__container");
            });
        }
    }
}
