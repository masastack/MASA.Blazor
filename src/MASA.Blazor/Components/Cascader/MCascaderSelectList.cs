using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace MASA.Blazor
{
    internal partial class MCascaderSelectList<TItem, TValue> : MSelectList<TItem, TValue, TValue>
    {
        [Parameter]
        public EventCallback<TItem> OnItemClick { get; set; }

        [Parameter]
        public List<TItem> Children { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Icon = Children != null ? "mdi-chevron-right" : string.Empty;
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            AbstractProvider
                .Apply(typeof(BProgressCircular), typeof(MProgressCircular), attrs =>
                {
                    attrs[nameof(MProgressCircular.Indeterminate)] = true;
                    attrs[nameof(MProgressCircular.Size)] = (StringNumber)20;
                    attrs[nameof(MProgressCircular.Width)] = (StringNumber)2;
                });
        }

        protected override bool Selected => false;

        protected override async Task OnSelectAsync()
        {
            //TODO: remove this
            if (OnItemClick.HasDelegate)
            {
                await OnItemClick.InvokeAsync(Item);
            }

            if (Loading == true)
            {
                return;
            }

            if (Children == null || Children.Count == 0)
            {
                await Select.SetSelectedAsync(Label, Value);
                Select.SetVisible(false);
            }
        }
    }
}
