using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MStepper : MSheet
    {
        protected bool IsBooted { get; set; } = true;

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool AltLabels { get; set; }

        [Parameter]
        public bool NonLinear { get; set; }

        [Parameter]
        public int Value { get; set; } = 1;

        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper")
                        .AddIf("m-stepper--flat", () => Flat)
                        .AddIf("m-stepper--is-booted", () => IsBooted)
                        .AddIf("m-stepper--vertical", () => Vertical)
                        .AddIf("m-stepper--alt-labels", () => AltLabels)
                        .AddIf("m-stepper--non-linear", () => NonLinear);
                });
        }

        public void StepClick(int step)
        {
            Value = step;
            StateHasChanged();
        }
    }
}
