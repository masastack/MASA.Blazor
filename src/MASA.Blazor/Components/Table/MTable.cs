using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
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

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-data-table";

            ConfigProvider
                .Add<BTable<TItem>>(cssBuilder =>
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
                .Add<BTable<TItem>>("wrap", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-data-table__wrapper");
                 }, styleBuilder =>
                 {
                     styleBuilder
                         .AddIf(() => $"height:{Height.Value}", () => Height.Value != default);
                 })
                .Add<BTable<TItem>>("empty", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__empty-wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"height:{Height.Value}", () => Height.Value != default);
                })
                .Add<BTableHeader>(cssBuilder =>
               {
                   cssBuilder
                       .Add("m-data-table-header");
               })
                .Add<BProcessLinear, MProcessLinear>(properties =>
                {
                    properties[nameof(MProcessLinear.Color)] = "primary";
                    properties[nameof(MProcessLinear.Indeterminate)] = true;
                })
                .Add<BTableLoading>("progress", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-data-table__progress");
                })
                .Add<BTableLoading>("column", cssBuilder =>
                {
                    cssBuilder
                        .Add("column");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("padding:0");
                })
                .Add<BButton, MTableFooterButton>("prev", properties =>
                {
                    properties[nameof(MTableFooterButton.IconName)] = "mdi-chevron-left";
                    properties[nameof(MTableFooterButton.HandlePageChange)] = EventCallback.Factory.Create<MouseEventArgs>(this, args =>
                    {
                        if (Page > 1)
                        {
                            Page--;
                        }
                        else
                        {
                            PrevDisabled = true;
                        }

                        if (Page < TotalPage)
                        {
                            NextDisabled = false;
                        }
                    });
                    properties[nameof(MTableFooterButton.Disabled)] = PrevDisabled;
                })
                .Add<BButton, MTableFooterButton>("next", properties =>
                {
                    properties[nameof(MTableFooterButton.IconName)] = "mdi-chevron-right";
                    properties[nameof(MTableFooterButton.HandlePageChange)] = EventCallback.Factory.Create<MouseEventArgs>(this, () =>
                    {
                        if (Page < TotalPage)
                        {
                            Page++;
                        }
                        else
                        {
                            NextDisabled = true;
                        }

                        if (Page > 1)
                        {
                            PrevDisabled = false;
                        }

                    });
                    properties[nameof(MTableFooterButton.Disabled)] = NextDisabled;
                })
                .Add<BTableFooter>(css =>
                {
                    css.Add("m-data-footer");
                })
                .Add<BTableFooter>("select", css =>
                 {
                     css.Add("m-data-footer__select");
                 })
                .Add<BTableFooter>("pagination", css =>
                {
                    css.Add("m-data-footer__pagination");
                })
                .Add<BTableFooter>("before", css =>
                {
                    css.Add("m-data-footer__icons-before");
                })
                .Add<BTableFooter>("after", css =>
                {
                    css.Add("m-data-footer__icons-after");
                });
        }
    }
}
