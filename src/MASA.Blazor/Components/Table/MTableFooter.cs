using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

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
                .Apply<BSelect<string, string>, MSelect<string, string>>(props =>
                {
                    props[nameof(MSelect<string, string>.Items)] = new List<string>
                    {
                        "5",
                        "10",
                        "15",
                        "All"
                    };
                    props[nameof(MSelect<string, string>.ItemText)] = new Func<string, string>(r => r);
                    props[nameof(MSelect<string, string>.ItemValue)] = new Func<string, string>(r => r);
                    props[nameof(MSelect<string, string>.MinWidth)] = (StringNumber)75;
                    props[nameof(MSelect<string, string>.ValueChanged)] = OnPageSizeChange;
                    props[nameof(MSelect<string, string>.Value)] = PageSize.ToString();
                });
        }
    }
}
