namespace Masa.Blazor
{
    public class MTextarea : MTextField<string>
    {
        [Parameter] public bool AutoGrow { get; set; }

        [Parameter] public bool NoResize { get; set; }

        [Parameter] public StringNumber RowHeight { get; set; } = 24;

        [Parameter] public int Rows { get; set; } = 5;

        private static Block _block = new("m-textarea");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private int _jsInteropId = -1; // -1 means not registered
        private bool _jsInteropReady;
        private bool _prevAutoGrow;
        private StringNumber? _prevRowHeight;

        public override string Tag => "textarea";

        public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

        protected override Dictionary<string, object> InputAttrs => new(Attributes)
        {
            { "rows", Rows },
            { "data-row-height", RowHeight },
            { "data-auto-grow", AutoGrow }
        };

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (_prevAutoGrow != AutoGrow)
            {
                _prevAutoGrow = AutoGrow;
                if (AutoGrow)
                {
                    await RegisterAutoGrowEventAsync();
                }
                else
                {
                    await UnregisterAutoGrowEventAsync();
                }
            }

            if (_prevRowHeight != RowHeight)
            {
                _prevRowHeight = RowHeight;

                if (AutoGrow)
                {
                    await CalculateHeightAsync();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && AutoGrow)
            {
                _jsInteropReady = true;
                await RegisterAutoGrowEventAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(new[]
            {
                _modifierBuilder.Add(AutoGrow).Add("no-resize", AutoGrow || NoResize).Build()
            });
        }

        private async Task RegisterAutoGrowEventAsync()
        {
            if (!_jsInteropReady || _jsInteropId > -1)
            {
                return;
            }

            _jsInteropId = await Js.InvokeAsync<int>(JsInteropConstants.RegisterTextareaAutoGrowEvent, InputElement)
                .ConfigureAwait(false);

            _ = CalculateHeightAsync();
        }

        private async Task UnregisterAutoGrowEventAsync()
        {
            await Js.InvokeVoidAsync(JsInteropConstants.UnregisterTextareaAutoGrowEvent, InputElement, _jsInteropId)
                .ConfigureAwait(false);
            _jsInteropId = -1;
        }

        protected override async Task SetValueByJsInterop(string? val)
        {
            await base.SetValueByJsInterop(val);

            if (AutoGrow)
            {
                await CalculateHeightAsync();
            }
        }

        private async Task CalculateHeightAsync()
        {
            if (!_jsInteropReady)
            {
                return;
            }

            await Js.InvokeVoidAsync(JsInteropConstants.CalculateTextareaHeight, InputElement, Rows,
                    RowHeight.ToString())
                .ConfigureAwait(false);
        }

        protected override async ValueTask DisposeAsyncCore()
        {
            if (AutoGrow)
            {
                await UnregisterAutoGrowEventAsync();
            }

            await base.DisposeAsyncCore();
        }
    }
}