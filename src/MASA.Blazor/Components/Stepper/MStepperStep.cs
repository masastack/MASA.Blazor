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
    public partial class MStepperStep : BStepperStep
    {
        [CascadingParameter]
        public MStepper Stepper { get; set; }

        //protected bool IsActive => Stepper.Value == Step;

        protected bool IsActive { get; set; }

        //protected bool IsInactive => Stepper.Value < Step;

        protected bool IsInactive { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__step")
                        .AddIf("m-stepper__step--active", () => IsActive)
                        .AddIf("m-stepper__step--editable", () => Editable)
                        .AddIf("m-stepper__step--inactive", () => IsInactive)
                        .AddIf("m-stepper__step--error error--text", () => HasError)
                        .AddIf("m-stepper__step--complete", () => Complete);
                })
                .Apply("step", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__step__step")
                        .AddBackgroundColor(Color, () => !HasError && (Complete || IsActive));
                })
                .Apply("label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__label");
                });

            AbstractProvider
                .Apply<BIcon, MIcon>();
        }

        protected override void OnInitialized()
        {
            Stepper.RegisterStep(this);

        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
            }
        }

        public override Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync();
            }

            if (Editable)
            {
                Stepper.StepClick(Step);
            }

            return base.HandleOnClickAsync(args);
        }

        public void Toggle(int step)
        {
            IsActive = this.Step == step;
            IsInactive = step < this.Step;

            StateHasChanged();
        }
    }
}
