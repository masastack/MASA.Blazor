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
                .Apply<BMenu, MCascaderMenu>(props =>
                {
                    props[nameof(MCascaderMenu.ActiverRef)] = SelectSoltRef;
                    props[nameof(MCascaderMenu.OffsetLeft)] = -12.00;
                    props[nameof(MMenu.Visible)] = Visible;
                    props[nameof(MCascaderMenu.ContentStyle)] = "background-color: white";
                    props[nameof(MMenu.VisibleChanged)] = EventCallback.Factory.Create<bool>(this, (v) =>
                    {
                        Visible = v;
                    });
                    props[nameof(MMenu.OffsetY)] = MenuProps?.OffsetY;
                    props[nameof(MMenu.OffsetX)] = MenuProps?.OffsetX;
                    props[nameof(MMenu.Block)] = MenuProps?.Block ?? true;
                    props[nameof(MMenu.CloseOnContentClick)] = !HasBody && !Multiple;
                    props[nameof(MMenu.Top)] = MenuProps?.Top;
                    props[nameof(MMenu.Right)] = MenuProps?.Right;
                    props[nameof(MMenu.Bottom)] = MenuProps?.Bottom;
                    props[nameof(MMenu.Left)] = MenuProps?.Left;
                    props[nameof(MMenu.NudgeTop)] = MenuProps?.NudgeTop;
                    props[nameof(MMenu.NudgeRight)] = MenuProps?.NudgeRight;
                    props[nameof(MMenu.NudgeBottom)] = MenuProps?.NudgeBottom;
                    props[nameof(MMenu.NudgeLeft)] = MenuProps?.NudgeLeft;
                    props[nameof(MMenu.NudgeWidth)] = MenuProps?.NudgeWidth;
                    props[nameof(MMenu.MaxHeight)] = MenuProps?.MaxHeight ?? 400;
                    props[nameof(MMenu.MinWidth)] = (StringNumber)"auto";
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

            base.SetComponentClass();
        }
    }
}
