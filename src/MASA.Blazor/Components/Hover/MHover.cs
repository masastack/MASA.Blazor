using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MHover : BHover
    {
        [Parameter]
        public bool Value { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public StringNumber OpenDelay { get; set; } = 0;

        [Parameter]
        public StringNumber CloseDelay { get; set; } = 0;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();
            CssProvider
                .Apply(cssBuilder => 
                {
                    cssBuilder
                        .AddIf("on-hover", () => IsActive);
                },styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => Value);
                });
        }

        protected virtual async Task MouseEnter(MouseEventArgs e)
        {
            if(OpenDelay != null && OpenDelay.ToInt32() > 0)
                await Task.Delay(OpenDelay.ToInt32());

            IsActive = !Disabled || IsActive;
        }

        protected virtual async Task MouseOut(MouseEventArgs e)
        {
            if (CloseDelay != null && CloseDelay.ToInt32() > 0)
                await Task.Delay(CloseDelay.ToInt32());

            IsActive = Disabled && IsActive;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var element = Document.QuerySelector($"[_b_{Id}]");
                await element.AddEventListenerAsync("mouseenter", 
                    EventCallback.Factory.Create<MouseEventArgs>(this, MouseEnter), false);
                await element.AddEventListenerAsync("mouseleave", 
                    EventCallback.Factory.Create<MouseEventArgs>(this, MouseOut), false);
            }
        }
    }
}
