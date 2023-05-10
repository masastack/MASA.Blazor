using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
{
    public partial class MTextarea : MTextField<string>
    {
        [Parameter]
        public bool AutoGrow
        {
            get => GetValue(false);
            set => SetValue(value);
        }

        [Parameter]
        public bool NoResize { get; set; }

        [Parameter]
        public StringNumber RowHeight { get; set; } = 24;

        [Parameter]
        public int Rows { get; set; } = 5;

        public override string Tag => "textarea";

        protected double ElementHeight { get; set; }

        public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

        protected override Dictionary<string, object?> InputAttrs => new(Attributes)
        {
            { "rows", Rows },
            { "style", AutoGrow && ElementHeight > 0 ? $"height:{ElementHeight}px" : null }
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && AutoGrow)
            {
                await CalculateInputHeight();
                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

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

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher
                .Watch<bool>(nameof(AutoGrow), ReCalculateInputHeight)
                .Watch<bool>(nameof(RowHeight), ReCalculateInputHeight);
        }

        protected override void OnLazyValueChange(string? val)
        {
            ReCalculateInputHeight();
        }

        private void ReCalculateInputHeight()
        {
            if (AutoGrow)
            {
                NextTick(async () =>
                {
                    await CalculateInputHeight();
                    StateHasChanged();
                });
            }
        }

        protected override async Task SetValueByJsInterop(string? val)
        {
            await base.SetValueByJsInterop(val);
            await CalculateInputHeight();
            StateHasChanged();
        }

        public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            if (OnKeyDown.HasDelegate)
                await OnKeyDown.InvokeAsync(args);
        }

        private async Task CalculateInputHeight()
        {
            var input = Document.GetElementByReference(InputElement);
            if (input is null) return;
            var height = await input.GetScrollHeightWithoutHeight();
            var minheight = Rows * RowHeight.ToInt32() * 1.0;

            ElementHeight = Math.Max(minheight, height ?? 0);
        }
    }
}
