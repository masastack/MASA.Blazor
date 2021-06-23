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
            HasBody = true;
            Outlined = true;
            ItemText = r => IsFull ? string.Join('/', r.GetAllNodes().Select(t => t.Label)) : r.Label;
            ItemValue = r => r.Value;

            AbstractProvider
                .Apply<BPopover, MCascaderPopover>(props =>
                {
                    props[nameof(MPopover.Class)] = "m-menu__content menuable__content__active";
                    props[nameof(MPopover.Visible)] = (Visible && Items != null);
                    props[nameof(MPopover.ClientX)] = (StringNumber)Left;
                    props[nameof(MPopover.ClientY)] = (StringNumber)Top;
                })
                .Apply<ISelectBody, MCascaderSelectBody>(props =>
                {
                    props[nameof(MCascaderSelectBody.Items)] = Items;
                    props[nameof(MCascaderSelectBody.Visible)] = Visible;
                    props[nameof(MCascaderSelectBody.Left)] = Left;
                    props[nameof(MCascaderSelectBody.Top)] = Top;
                    props[nameof(MCascaderSelectBody.OnOptionSelect)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
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
