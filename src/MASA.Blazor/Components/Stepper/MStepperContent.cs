using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Web;

namespace MASA.Blazor
{
    public partial class MStepperContent : BStepperContent, IAsyncDisposable
    {
        private bool _firstRender = true;

        protected StringNumber Height { get; set; } = "auto";

        protected override bool IsVertical => Stepper.Vertical;

        [CascadingParameter]
        public MStepper Stepper { get; set; }

        [Parameter]
        public int Step { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        protected override bool IsRtl => GlobalConfig.RTL;

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
                        .AddIf("display:none", () => IsVertical && !IsActive && _firstRender);
                })
                .Apply("wrapper", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-stepper__wrapper")
                        .AddIf("active", () => IsActive);
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _firstRender = false;

                if (Stepper.Vertical)
                {
                    await JsInvokeAsync(JsInteropConstants.InitStepperWrapper, Ref);
                }

                await JsInvokeAsync(JsInteropConstants.AddStepperEventListener, Ref, IsActive);
            }
        }

        public void Toggle(int step, bool reverse)
        {
            IsActive = Step == step;
            IsReverse = reverse;

            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            Stepper.UnRegisterContent(this);

            await JsInvokeAsync(JsInteropConstants.RemoveStepperEventListener, Ref, IsActive);
        }
    }
}
