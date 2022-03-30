namespace Masa.Blazor
{
    public class MECharts : BECharts, IDisposable
    {
        [Parameter]
        public StringNumber Width { get; set; } = 600;

        [Parameter]
        public StringNumber Height { get; set; } = 400;

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public object Option { get; set; } = new { };

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(styleAction: styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(Width)
                        .AddHeight(Height)
                        .AddMinWidth(MinWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight)
                        .AddMaxWidth(MaxWidth);
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsDisposed)
            {
                return;
            }

            var echarts = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Blazor/js/echarts-helper.js");
            await echarts.InvokeVoidAsync("init", Ref, Option);
        }
    }
}
