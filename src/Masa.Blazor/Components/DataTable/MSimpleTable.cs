using Element = BlazorComponent.Web.Element;

namespace Masa.Blazor
{
    public class MSimpleTable : BSimpleTable, ISimpleTable
    {
        [Inject]
        private MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool FixedHeader { get; set; }

        /// <summary>
        /// TODO: internal use?
        /// </summary>
        [Parameter]
        public bool HasFixed { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public RenderFragment? WrapperContent { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        public ElementReference WrapperElement { get; set; }

        private int _scrollState;

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table")
                        .AddIf("m-data-table--dense", () => Dense)
                        .AddIf("m-data-table--fixed-height", () => Height != null && !FixedHeader)
                        .AddIf("m-data-table--fixed-header", () => FixedHeader)
                        .AddIf("m-data-table--has-top", () => TopContent != null)
                        .AddIf("m-data-table--has-bottom", () => BottomContent != null)
                        .AddTheme(IsDark);
                })
                .Apply("wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__wrapper")
                        .AddIf("scrolled-to-left", () => HasFixed && _scrollState == 0)
                        .AddIf("scrolling", () => HasFixed && _scrollState == 1)
                        .AddIf("scrolled-to-right", () => HasFixed && _scrollState == 2);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height);
                })
                .Apply("table", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .AddWidth(Width);
                });

            AbstractProvider
                .ApplySimpleTableDefault();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await HandleOnScrollAsync(EventArgs.Empty);
                StateHasChanged();
            }
        }

        public async Task HandleOnScrollAsync(EventArgs args)
        {
            if (!HasFixed)
            {
                return;
            }

            var element = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, WrapperElement);
            if (element != null)
            {
                if (Math.Abs(element.ScrollWidth - ((MasaBlazor.RTL ?  -element.ScrollLeft : element.ScrollLeft) + element.ClientWidth)) < 0.01)
                {
                    _scrollState = 2;
                }
                else if (Math.Abs(element.ScrollLeft - (MasaBlazor.RTL ? element.ScrollWidth - element.ClientWidth : 0)) < 0.01)
                {
                    _scrollState = 0;
                }
                else
                {
                    _scrollState = 1;
                }
            }
        }
    }
}
