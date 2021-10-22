using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MStepperContent : BStepperContent
    {
        protected StringNumber Height { get; set; } = "auto";

        //protected override bool IsActive=> Stepper.i

        [CascadingParameter]
        public MStepper Stepper { get; set; }

        [Parameter]
        public int Step { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__content");
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("transform-origin: center top 0px")
                        .AddIf("display:none", () => Stepper.Value != Step);
                })
                .Apply("wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__wrapper");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"height:{Height.ToUnit()}", () => Height != null && Stepper.Vertical);
                });
        }

        protected override void OnInitialized()
        {
            Stepper.RegisterContent(this);
        }

        public void Toggle(int step, bool reverse)
        {
            IsActive = Step == step;
            IsReverse = reverse;

            StateHasChanged();
        }
    }
}
