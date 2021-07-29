using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascader : MSelect<BCascaderNode, string>
    {
        [Parameter]
        public bool IsFull { get; set; }

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
            base.SetComponentClass();

            HasBody = true;
            Outlined = true;
            ItemText = r => IsFull ? string.Join('/', r.GetAllNodes().Select(t => t.Label)) : r.Label;
            ItemValue = r => r.Value;

            AbstractProvider
                .Merge<BMenu, MCascaderMenu>(props =>
                {
                    props[nameof(MCascaderMenu.OffsetY)] = true;
                    props[nameof(MCascaderMenu.ActivatorStyle)] = "display:flex";
                    props[nameof(MCascaderMenu.MinWidth)] = (StringNumber)180;
                    props[nameof(MCascaderMenu.CloseOnContentClick)] = false;
                })
                .Apply<ISelectBody, MCascaderSelectBody>(props =>
                {
                    props[nameof(MCascaderSelectBody.Items)] = Items;
                    props[nameof(MCascaderSelectBody.Visible)] = Visible;
                    props[nameof(MCascaderSelectBody.OnOptionSelect)] = EventCallback.Factory.Create<MCascaderSelectOption>(this, async option =>
                    {
                        await SetSelectedAsync(ItemText(option.Item), option.Value);
                    });
                });

        }
    }
}
