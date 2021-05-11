using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascader : MSelect<BCascaderNode>
    {
        public MCascader()
            : base()
        {
            Slot = true;
            Outlined = true;
            ItemText = r => IsFull ? string.Join('/', r.GetAllNodes().Select(t => t.Label)) : r.Label;
            ItemValue = r => r.Value;
        }

        [Parameter]
        public bool IsFull { get; set; }

        protected double Left { get; set; }

        protected double Top { get; set; }

        protected override void SetComponentClass()
        {
            SlotProvider
                .Apply<BPopover, MCascaderPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = (_visible && Items != null);
                    props[nameof(MPopover.ClientX)] = (StringOrNumber)Left;
                    props[nameof(MPopover.ClientY)] = (StringOrNumber)Top;
                })
                .Apply<BSelectSlot, MCascaderSelectSlot>(props =>
                {
                    props[nameof(MCascaderSelectSlot.Items)] = Items;
                    props[nameof(MCascaderSelectSlot.SelectNode)] = SelectNode;
                    props[nameof(MCascaderSelectSlot.Visible)] = _visible;
                    props[nameof(MCascaderSelectSlot.Left)] = Left;
                    props[nameof(MCascaderSelectSlot.Top)] = Top;
                    props[nameof(MCascaderSelectSlot.OnOptionSelect)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
                     {
                         await SetSelectedAsync(option.Value);
                     });
                });

            base.SetComponentClass();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var rect = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Ref);
                Left = rect.Left;
                Top = rect.Top + rect.Height;

                if (!string.IsNullOrEmpty(Value))
                {
                    var nodes = new List<BCascaderNode>();
                    var node = GetNode(Value, Items);
                    if (node != null)
                    {
                        await SetSelectedAsync(node);
                    }
                }

                StateHasChanged();
            }
        }

        private BCascaderNode GetNode(string value, IReadOnlyList<BCascaderNode> items)
        {
            if (items == null || items.Count == 0)
            {
                return null;
            }

            foreach (var item in items)
            {
                if (item.Value == value)
                {
                    return item;
                }
                else
                {
                    return GetNode(value, item.Children);
                }
            }

            return null;
        }
    }
}
