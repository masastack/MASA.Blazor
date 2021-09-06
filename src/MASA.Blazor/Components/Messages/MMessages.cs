using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MMessages : BMessages, IThemeable
    {
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
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-messages";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(IsDark)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__wrapper");
                })
                .Apply("message", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__message");
                });

            AbstractProvider
                .Apply(typeof(BMessagesChildren<>), typeof(BMessagesChildren<MMessages>))
                .Apply(typeof(BMessagesMessage<>), typeof(BMessagesMessage<MMessages>));
        }
    }
}
