using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MTextarea : MTextField<string>
    {
        [Parameter]
        public bool AutoGrow { get; set; }

        [Parameter]
        public bool NoResize { get; set; }

        [Parameter]
        public StringNumber RowHeight { get; set; } = 24;

        [Parameter]
        public int Rows { get; set; } = 5;

        [Inject]
        public Document Document { get; set; }

        public override string Tag => "textarea";

        protected double ElementHeight { get; set; }

        public override Dictionary<string, object> InputAttrs => new(Attributes)
        {
            { "rows", Rows },
            { "style", AutoGrow && ElementHeight > 0 ? $"height:{ElementHeight}px" : null }
        };

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-textarea";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--auto-grow", () => AutoGrow)
                        .AddIf($"{prefix}--no-resize", () => AutoGrow || NoResize);
                });
        }

        public override async Task HandleOnInputAsync(ChangeEventArgs args)
        {
            await base.HandleOnInputAsync(args);

            if (AutoGrow)
            {
                await CalculateInputHeight();
            }
        }

        private async Task CalculateInputHeight()
        {
            var input = Document.QuerySelector(InputElement);
            var height = await input.GetScrollHeightWithoutHeight();
            var minheight = Rows * RowHeight.ToInt32() * 1.0;

            ElementHeight = Math.Max(minheight, height);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && AutoGrow)
            {
                await CalculateInputHeight();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
