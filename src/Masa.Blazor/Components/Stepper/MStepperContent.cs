﻿namespace Masa.Blazor
{
    public partial class MStepperContent : BStepperContent, IAsyncDisposable
    {
        [Inject]
        public MasaBlazor MasaBlazor { get; set; } = null!;

        [CascadingParameter]
        public MStepper? Stepper { get; set; }

        [Parameter]
        public int Step { get; set; }

        private bool _firstRender = true;

        protected StringNumber Height { get; set; } = "auto";

        protected override bool IsVertical => Stepper?.Vertical is true;

        protected override bool IsRtl => MasaBlazor.RTL;

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
                        .AddIf($"height:{Height.ToUnit()}", () => Height != null && Stepper?.Vertical is true);
                });
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Stepper?.RegisterContent(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _firstRender = false;

                if (Stepper?.Vertical is true)
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
            Stepper?.UnRegisterContent(this);

            if (Ref.Context is not null)
            {
                await JsInvokeAsync(JsInteropConstants.RemoveStepperEventListener, Ref, IsActive);
            }
        }
    }
}
