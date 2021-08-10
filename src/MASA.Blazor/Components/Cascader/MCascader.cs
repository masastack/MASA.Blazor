using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCascader : MSelect<BCascaderNode, string>, ICascader
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

            Outlined = true;
            ItemText = r => IsFull ? string.Join('/', r.GetAllNodes().Select(t => t.Label)) : r.Label;
            ItemValue = r => r.Value;

            CssProvider
                .Apply("cascader-menu-body", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("display: inline-flex");
                })
                .Apply("cascader-menu-body-wrap", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("vertical-align: top")
                        .Add("min-width: 180px")
                        .Add("background-color: white")
                        .Add("border-right: 1px solid #f0f0f0");
                });

            AbstractProvider
                .Merge<BMenu, MCascaderMenu>(props =>
                {
                    props[nameof(MCascaderMenu.OffsetY)] = true;
                    props[nameof(MCascaderMenu.MinWidth)] = (StringNumber)180;
                    props[nameof(MCascaderMenu.CloseOnContentClick)] = false;
                })
                .Apply<BList, MList>()
                .Apply<BListItemGroup, MListItemGroup>(props =>
                {
                    props[nameof(MListItemGroup.Color)] = "primary";
                })
                .Merge<BSelectOption<BCascaderNode, string>, MCascaderSelectOption>()
                .Merge(typeof(BSelectMenu<,,>), typeof(BCascaderMenu<MCascader>))
                .Apply(typeof(BCascaderMenuBody<>), typeof(BCascaderMenuBody<MCascader>));
        }
    }
}
