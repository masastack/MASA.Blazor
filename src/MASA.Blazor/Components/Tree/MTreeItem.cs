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
            CssProvider
                .AsProvider<BTreeItem<T>>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node")
                        .Add("m-treeview-node__root");
                })
                .Apply("level", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-treeview-node__level");
                 })
                .Apply("icon", styleAction: styleBuilder =>
                 {
                     styleBuilder
                         .Add("width:14px");
                 })
                .Apply("title", styleAction: styleBuilder =>
                {
                    styleBuilder
                        .Add("margin-left:3px")
                        .Add("cursor:default");
                });

            AbstractProvider
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
