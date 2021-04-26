using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BTableFooter>()
                .Apply(css =>
                {
                    css.Add("m-data-footer");
                })
                .Apply("select", css =>
                {
                    css.Add("m-data-footer__select");
                })
                .Apply("pagination", css =>
                {
                    css.Add("m-data-footer__pagination");
                })
                .Apply("before", css =>
                {
                    css.Add("m-data-footer__icons-before");
                })
                .Apply("after", css =>
                {
                    css.Add("m-data-footer__icons-after");
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
                });
        }
    }
}
