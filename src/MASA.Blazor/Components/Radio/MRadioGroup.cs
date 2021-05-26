using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
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
                });

            AbstractProvider
                .Apply<IInputBody, MRadioGroupInputBody>();
        }

        public void AddRadio(MRadio<TValue> radio)
        {
            Items.Add(radio);
            radio.Change = EventCallback.Factory.Create<BRadio<TValue>>(this, UpdateItemsState);
        }

        public async Task UpdateItemsState(BRadio<TValue> radio)
        {
            foreach (var item in Items)
            {
                if (item != radio)
                {
                    item.DeActive();
                }
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(radio.Value);
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                foreach (var item in Items)
                {
                    if (item.Value.Equals(Value))
                    {
                        item.Active();
                        break;
                    }
                }

                StateHasChanged();
            }
        }
    }
}
