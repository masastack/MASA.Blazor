using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    internal partial class MSwitchInputBody : BSwitchInputBody, IInputBody
    {
        [Parameter]
        public string ValidationState { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--selection-controls__input");
                })
                .Apply("ripple", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-input--selection-controls__ripple")
                         .AddTextColor(ValidationState, () => IsActive);
                 })
                .Apply("track", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__track")
                        .AddTextColor(ValidationState, () => IsActive)
                        .AddTheme(Dark);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__thumb")
                        .AddTextColor(ValidationState, () => IsActive)
                        .AddTheme(Dark);
                });

            AbstractProvider
                .Apply<BLabel, MLabel>();
        }
    }
}
