using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MTree<T> : BTree<T>
    {
        /// <summary>
        /// Whether dark theme
        /// </summary>
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BTree<T>>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview")
                        .AddTheme(Dark);
                })
                .Apply("child", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__children");
                });

            AbstractProvider
                .Apply<BTreeItem<T>, MTreeItem<T>>(props =>
                {
                    props[nameof(MTreeItem<T>.HandleItemClick)] = HandleItemClick;
                    props[nameof(MTreeItem<T>.HandleCheckboxClick)] = HandleCheckboxClick;
                    props[nameof(MTreeItem<T>.DefaultCheckedExpression)] = DefaultCheckedExpression;
                    props[nameof(MTreeItem<T>.Checkable)] = Checkable;
                });
        }
    }
}
