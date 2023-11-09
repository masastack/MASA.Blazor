namespace Masa.Blazor
{
    public class MRadioGroup<TValue> : MInput<TValue>, IRadioGroup<TValue>
    {
        [Parameter]
        [MassApiParameter(true)]
        public bool Column { get; set; } = true;

        [Parameter]
        public bool Mandatory { get; set; }

        [Parameter]
        public bool Row { get; set; }

        private bool _isImmediateCallbackForValueChange;

        private List<IRadio<TValue>> Items { get; } = new();

        protected override void OnValueChanged(TValue val)
        {
            base.OnValueChanged(val);

            RefreshItemsState();
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-input";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}--selection-controls")
                        .Add($"{prefix}--radio-group")
                        .AddIf($"{prefix}--radio-group--column", () => Column && !Row)
                        .AddIf($"{prefix}--radio-group--row", () => Row);
                })
                .Apply("radio-group", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input--radio-group__input");
                });

            AbstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BRadioGroupDefaultSlot<TValue>));
        }

        public void AddRadio(IRadio<TValue> radio)
        {
            if (!Items.Contains(radio))
            {
                Items.Add(radio);
            }

            if (Mandatory && Value == null)
            {
                Value = radio.Value;
                ValueChanged.InvokeAsync(radio.Value);
            }

            RefreshItemsState();
        }

        private void RefreshItemsState()
        {
            Items.ForEach(item => item.RefreshState());
        }

        public async Task Toggle(TValue value)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }

            await OnChange.InvokeAsync(value);

            await TryInvokeFieldChangeOfInputsFilter();

            NextTick(RefreshItemsState);
        }
    }
}
