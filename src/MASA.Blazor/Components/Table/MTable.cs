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
    using StringNumber = OneOf<string, int>;

    public partial class MTable<TItem> : BTable<TItem>
    {
        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool FixedHeader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-data-table";

            CssProvider
                .AsProvider<BTable<TItem>>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                         .Add("m-data-table")
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--fixed-height", () => Height.Value != default && !FixedHeader)
                        .AddIf($"{prefix}--fixed-header", () => FixedHeader)
                        .AddIf($"{prefix}--has-top", () => Top != default)
                        .AddIf($"{prefix}--has-bottom", () => Bottom != default)
                        .AddTheme(Dark);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"height:{Height.Value}", () => Height.Value != default);
                })
                .Apply("empty", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__empty-wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"height:{Height.Value}", () => Height.Value != default);
                });

            SlotProvider
                .Apply<BTableLoading, MTableLoading>()
                .Apply<BTableHeader, MTableHeader>(properties =>
                {
                    properties[nameof(MTableHeader.Headers)] = Headers;
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
                });
        }
    }
}
