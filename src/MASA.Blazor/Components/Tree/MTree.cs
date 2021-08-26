using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MTree<T> : BTree<T>,IThemeable
    {

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        } 

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview")
                        .AddTheme(IsDark);
                })
                .Apply("child", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__children");
                });

            AbstractProvider
                .Apply<BTreeItem<T>, MTreeItem<T>>(props =>
                {
                    props[nameof(MTreeItem<T>.OnItemClick)] = OnItemClick;
                    props[nameof(MTreeItem<T>.OnCheckboxClick)] = OnCheckboxClick;
                    props[nameof(MTreeItem<T>.DefaultCheckedExpression)] = DefaultCheckedExpression;
                    props[nameof(MTreeItem<T>.Checkable)] = Checkable;
                    props[nameof(MTreeItem<T>.PrependContent)] = PrependContent;
                    props[nameof(MTreeItem<T>.ItemDisabled)] = ItemDisabled;
                });
        }
    }
}
