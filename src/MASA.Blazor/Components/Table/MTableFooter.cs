using System;
using System.Collections.Generic;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor.Components.Table
{
    public partial class MTableFooter : BTableFooter
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnPrevClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnNextClick { get; set; }

        [Parameter]
        public bool PrevDisabled { get; set; }

        [Parameter]
        public bool NextDisabled { get; set; }

        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public EventCallback<string> OnPageSizeChange { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BTableFooter>()
                .Apply(cssBuilder =>
                {
                    cssBuilder.Add("m-data-footer");
                })
                .Apply("select", cssBuilder =>
                {
                    cssBuilder.Add("m-data-footer__select");
                })
                .Apply("pagination", cssBuilder =>
                {
                    cssBuilder.Add("m-data-footer__pagination");
                })
                .Apply("before", cssBuilder =>
                {
                    cssBuilder.Add("m-data-footer__icons-before");
                })
                .Apply("after", cssBuilder =>
                {
                    cssBuilder.Add("m-data-footer__icons-after");
                });

            SlotProvider
                .Apply<BButton, MTableFooterButton>("prev", properties =>
                {
                    properties[nameof(MTableFooterButton.IconName)] = "mdi-chevron-left";
                    properties[nameof(MTableFooterButton.HandlePageChange)] = OnPrevClick;
                    properties[nameof(MTableFooterButton.Disabled)] = PrevDisabled;
                })
                .Apply<BButton, MTableFooterButton>("next", properties =>
                {
                    properties[nameof(MTableFooterButton.IconName)] = "mdi-chevron-right";
                    properties[nameof(MTableFooterButton.HandlePageChange)] = OnNextClick;
                    properties[nameof(MTableFooterButton.Disabled)] = NextDisabled;
                })
                .Apply<BSelect<string>, MSelect<string>>(props =>
                {
                    props[nameof(MSelect<string>.Items)] = new List<string>
                    {
                        "5",
                        "10",
                        "15",
                        "All"
                    };
                    props[nameof(MSelect<string>.ItemText)] = new Func<string, string>(r => r);
                    props[nameof(MSelect<string>.ItemValue)] = new Func<string, string>(r => r);
                    props[nameof(MSelect<string>.MinWidth)] = 75;
                    props[nameof(MSelect<string>.ValueChanged)] = OnPageSizeChange;
                    props[nameof(MSelect<string>.Value)] = PageSize.ToString();
                });
        }
    }
}
