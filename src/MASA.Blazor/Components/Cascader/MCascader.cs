using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascader : MSelect<BCascaderNode, string>
    {
        [Parameter]
        public bool IsFull { get; set; }

        protected double Left { get; set; }

        protected double Top { get; set; }

        private BCascaderNode GetNodeByValue(IEnumerable<BCascaderNode> items, string value)
        {
            BCascaderNode result = null;

            foreach (var item in items)
            {
                if (result != null) break;

                if (item.Value == value)
                {
                    result = item;
                    break;
                }
                else if (item.Children != null)
                {
                    result = GetNodeByValue(item.Children, value);
                }
            }

            return result;
        }

        protected override List<string> FormatText(string value)
        {
            var list = new List<string>();
            var result = GetNodeByValue(Items, value);

            if (result != null)
                list.Add(ItemText(result));

            return list;
        }

        protected override void SetComponentClass()
        {
            Slot = true;
            Outlined = true;
            ItemText = r => IsFull ? string.Join('/', r.GetAllNodes().Select(t => t.Label)) : r.Label;
            ItemValue = r => r.Value;

            SlotProvider
                .Apply<BPopover, MCascaderPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = (_visible && Items != null);
                    props[nameof(MPopover.ClientX)] = (StringNumber)Left;
                    props[nameof(MPopover.ClientY)] = (StringNumber)Top;
                })
                .Apply<BSelectSlot, MCascaderSelectSlot>(props =>
                {
                    props[nameof(MCascaderSelectSlot.Items)] = Items;
                    //props[nameof(MCascaderSelectSlot.SelectNode)] = SelectNode;
                    props[nameof(MCascaderSelectSlot.Visible)] = _visible;
                    props[nameof(MCascaderSelectSlot.Left)] = Left;
                    props[nameof(MCascaderSelectSlot.Top)] = Top;
                    props[nameof(MCascaderSelectSlot.OnOptionSelect)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
                    {
                        await SetSelectedAsync(ItemText(option.Item), option.Value);
                    });
                });

            base.SetComponentClass();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var rect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);
                Left = rect.AbsoluteLeft;
                Top = rect.AbsoluteTop + rect.ClientHeight;

                StateHasChanged();
            }
        }
    }
}
