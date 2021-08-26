using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using MASA.Blazor.Components.Table;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace MASA.Blazor
{
    public partial class MTable<TItem> : BTable<TItem>,IThemeable
    {
        private bool _scrollRight;

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool FixedHeader { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

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

        [Parameter]
        public bool FixedRight { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-data-table";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                         .Add("m-data-table")
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--fixed-height", () => Height != null && !FixedHeader)
                        .AddIf($"{prefix}--fixed-header", () => FixedHeader)
                        .AddIf($"{prefix}--has-top", () => TopContent != default)
                        .AddIf($"{prefix}--has-bottom", () => BottomContent != default)
                        .AddTheme(IsDark);
                })
                .Apply("wrap", cssBuilder =>
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
                })
                .Apply("stripe", cssBuilder =>
                {
                    cssBuilder
                        .AddIf("stripe", () => Stripe);
                })
                .Apply("empty", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__empty-wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height);
                });

            AbstractProvider
                .Apply<BTableLoading, MTableLoading>()
                .Apply<BTableHeader, MTableHeader>(properties =>
                {
                    properties[nameof(MTableHeader.Headers)] = Headers;
                    properties[nameof(MTableHeader.Align)] = Align;
                })
                .Apply<BTableFooter, MTableFooter>(properties =>
                {
                    properties[nameof(MTableFooter.OnPrevClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, args =>
                    {
                        if (Page > 1)
                        {
                            Page--;
                        }
                    });
                    properties[nameof(MTableFooter.OnNextClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, args =>
                    {
                        if (Page < TotalPage)
                        {
                            Page++;
                        }
                    });
                    properties[nameof(MTableFooter.PrevDisabled)] = PrevDisabled;
                    properties[nameof(MTableFooter.NextDisabled)] = NextDisabled;
                    properties[nameof(MTableFooter.PageStart)] = PageStart;
                    properties[nameof(MTableFooter.PageStop)] = PageStop;
                    properties[nameof(MTableFooter.TotalCount)] = TotalCount;
                    properties[nameof(MTableFooter.PageSize)] = PageSize;
                    properties[nameof(MTableFooter.OnPageSizeChange)] = EventCallback.Factory.Create<string>(this, val =>
                    {
                        if (int.TryParse(val, out var pageSize))
                        {
                            PageSize = pageSize;
                        }
                        else if (val == "All")
                        {
                            PageSize = TotalCount;
                        }
                    });
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && Width == null && FixedRight)
            {
                var element = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, WrapRef);
                Width = element.ClientWidth * 1.5;

                StateHasChanged();
            }
        }

        public override async Task HandleScrollAsync(EventArgs args)
        {
            if (FixedRight)
            {
                var element = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, WrapRef);
                if (element.ScrollWidth == element.ScrollLeft + element.ClientWidth)
                {
                    _scrollRight = true;
                    StateHasChanged();
                }
                else
                {
                    if (_scrollRight)
                    {
                        _scrollRight = false;
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
