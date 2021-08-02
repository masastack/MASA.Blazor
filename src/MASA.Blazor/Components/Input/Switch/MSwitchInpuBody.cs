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
    internal partial class MSwitchInputBody : BSwitchInputBody, IInputBody, IThemeable
    {
        [Parameter]
        public string ValidationState { get; set; }

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
                        .AddTheme(IsDark);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--switch__thumb")
                        .AddTextColor(ValidationState, () => IsActive)
                        .AddTheme(IsDark);
                });

            AbstractProvider
                .Apply<BLabel, MLabel>();
        }
    }
}
