using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    //TODO:该组件需要完善
    public partial class MRadioGroup<TValue> : MInput<TValue>
    {
        [Parameter]
        public bool Column { get; set; } = true;

        [Parameter]
        public bool Row { get; set; }

        protected List<MRadio<TValue>> Items { get; set; } = new();

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
                .Merge(typeof(CascadingValue<>), typeof(CascadingValue<MRadioGroup<TValue>>))
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BRadioGroupDefaultSlot<TValue>));
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            SetActiveRadio();
        }

        private void SetActiveRadio()
        {
            foreach (var radio in Items)
            {
                if (EqualityComparer<TValue>.Default.Equals(radio.Value, Value))
                {
                    radio.Active();
                }
                else
                {
                    radio.DeActive();
                }
            }
        }

        public void AddRadio(MRadio<TValue> radio)
        {
            if (!Items.Contains(radio))
            {
                Items.Add(radio);
                radio.NotifyChange += UpdateItemsState;
            }

            SetActiveRadio();
        }

        public async Task UpdateItemsState(BRadio<TValue> radio)
        {
            foreach (var item in Items)
            {
                if (item == radio)
                {
                    item.Active();
                }
                else
                {
                    item.DeActive();
                }
            }

            Value = radio.Value;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(radio.Value);
            }
        }
    }
}
