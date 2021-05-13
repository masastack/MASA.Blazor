using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MMessage : BMessage
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-messages";
            CssProvider
                .AsProvider<BMessage>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddTheme(Dark)
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
        }
    }
}
