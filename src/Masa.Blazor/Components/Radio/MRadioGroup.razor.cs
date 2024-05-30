namespace Masa.Blazor
{
    public partial class MRadioGroup<TValue> : MInput<TValue>, IRadioGroup<TValue>
    {
        [Parameter] [MasaApiParameter(true)] public bool Column { get; set; } = true;

        [Parameter] public bool Mandatory { get; set; }

        [Parameter] public bool Row { get; set; }

        private bool _isImmediateCallbackForValueChange;

        private List<IRadio<TValue>> Items { get; } = new();

        protected override bool WatchValueChangeImmediately => false;

        protected override void OnValueChanged(TValue val)
        {
            base.OnValueChanged(val);

            RefreshItemsState();
        }

        private static Block _block = new("m-input--radio-group");
        private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
        private static Block _selectionBlock = new("m-input--selection-controls");

        protected override IEnumerable<string> BuildComponentClass()
        {
            return base.BuildComponentClass().Concat(
                new[]
                {
                    _modifierBuilder.Add("column", Column && !Row)
                        .Add(Row)
                        .AddClass(_selectionBlock.Name)
                        .Build()
                }
            );
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