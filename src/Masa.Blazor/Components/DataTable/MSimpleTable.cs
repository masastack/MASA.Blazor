using Element = BlazorComponent.Web.Element;

namespace Masa.Blazor
{
    public class MSimpleTable : BSimpleTable, ISimpleTable
    {
        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool FixedHeader { get; set; }

        [Parameter]
        public bool FixedRight { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public RenderFragment WrapperContent { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        public ElementReference WrapperElement { get; set; }

        protected bool ScrollerOnRight { get; set; }

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
                        //REVIEW:Is this class name ok?
                        .AddIf("fixed-right", () => FixedRight)
                        .AddIf("not-scroll-right", () => FixedRight && !ScrollerOnRight);
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

        public async Task HandleOnScrollAsync(EventArgs args)
        {
            if (FixedRight)
            {
                var element = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, WrapperElement);
                if (element.ScrollWidth == element.ScrollLeft + element.ClientWidth)
                {
                    ScrollerOnRight = true;
                }
                else
                {
                    if (ScrollerOnRight)
                    {
                        ScrollerOnRight = false;
                    }
                }
            }
        }
    }
}
