using BlazorComponent.Web;

namespace Masa.Blazor
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

        public override string Tag => "textarea";

        protected double ElementHeight { get; set; }

        public override Action<TextFieldNumberProperty> NumberProps { get; set; }

        protected override Dictionary<string, object> InputAttrs => new(Attributes)
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
            var input = Document.GetElementByReference(InputElement);
            var height = await input.GetScrollHeightWithoutHeight();
            var minheight = Rows * RowHeight.ToInt32() * 1.0;

            ElementHeight = Math.Max(minheight, height ?? 0);
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
