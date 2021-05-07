using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;

namespace MASA.Blazor
{
    public class MTreeItem<T> : BTreeItem<T>
    {
        protected override void SetComponentClass()
        {
            CssBuilder.Add("m-treeview-node");
            CssBuilderRoot.Add("m-treeview-node__root");
            CssBuilderLevel.Add("m-treeview-node__level");

            CssProvider
                .Apply<BTreeItem<T>>("icon", styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add("width:14px");
                 })
                .Apply<BTreeItem<T>>("title", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("margin-left:3px")
                        .Add("cursor:default");
                });

            SlotProvider
                .Apply<BTreeItem<T>, MTreeItem<T>>(props =>
                {
                    props[nameof(HandleItemClick)] = HandleItemClick;
                    props[nameof(HandleCheckboxClick)] = HandleCheckboxClick;
                    props[nameof(DefaultCheckedExpression)] = DefaultCheckedExpression;
                    props[nameof(Checkable)] = Checkable;
                });
        }
    }
}
