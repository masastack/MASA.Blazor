using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public class MSimpleTable : BSimpleTable, ISimpleTable
    {
        private bool _scrollRight;

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
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        public ElementReference WrapperElement { get; set; }

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
                        .AddIf("fixed-right", () => FixedRight)
                        .AddIf("not-scroll-right", () => !_scrollRight);
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
                .Apply(typeof(BSimpleTableWrapper<>), typeof(BSimpleTableWrapper<MSimpleTable>));
        }

        public async Task HandleOnScrollAsync(EventArgs args)
        {
            if (FixedRight)
            {
                var element = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, WrapperElement);
                if (element.ScrollWidth == element.ScrollLeft + element.ClientWidth)
                {
                    _scrollRight = true;
                }
                else
                {
                    if (_scrollRight)
                    {
                        _scrollRight = false;
                    }
                }
            }
        }
    }
}
