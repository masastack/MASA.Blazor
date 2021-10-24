using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Element = BlazorComponent.Web.Element;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MStepperContent : BStepperContent, IAsyncDisposable
    {
        protected StringNumber Height { get; set; } = "auto";

        //protected override bool IsActive=> Stepper.i

        [CascadingParameter]
        public MStepper Stepper { get; set; }

        [Parameter]
        public int Step { get; set; }

        [Inject]
        public Document Document { get; set; }

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

            Watcher
                .Watch<bool>(nameof(IsActive), async (oldVal, newVal) =>
                {
                    // If active and the previous state
                    // was null, is just booting up
                    if (newVal == default && oldVal == default)
                    {
                        Height = "auto";
                    }

                    if (Stepper.Vertical)
                    {

                        if (IsActive)
                            await Enter();
                        else
                            await Leave();
                    }
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var wrapper = Document.QuerySelector(".wrapper");
                await wrapper.AddEventListenerAsync("transitionend", CreateEventCallback<MouseEventArgs>(OnTransition), false);

            }
        }

        public void Toggle(int step, bool reverse)
        {
            IsActive = Step == step;
            IsReverse = reverse;

            StateHasChanged();
        }

        public Task OnTransition(MouseEventArgs args)
        {
            if (IsActive)
            {
                Height = "auto";
            }

            return Task.CompletedTask;
        }

        public async Task Enter()
        {
            double scrollHeight;

            //TODO:
            //requestAnimationFrame(() => {
            //    scrollHeight = this.$refs.wrapper.scrollHeight
            //})

            var el = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, Ref);
            scrollHeight = el.ScrollHeight;

            Height = 0;

            await Task.Delay(450);

            if (IsActive)
            {
                Height = scrollHeight != default ? scrollHeight : "auto";
            }
        }

        public async Task Leave()
        {
            var el = await JsInvokeAsync<Element>(JsInteropConstants.GetDomInfo, Ref);
            Height = el.ScrollHeight;

            await Task.Delay(10);
            Height = 0;
        }

        public async ValueTask DisposeAsync()
        {
            var wrapper = Document.QuerySelector(".wrapper");
            await wrapper.RemoveEventListenerAsync("transitionend");
        }
    }
}
