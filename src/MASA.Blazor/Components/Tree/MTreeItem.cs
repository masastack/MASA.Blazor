using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public class MTreeItem<T> : BTreeItem<T>
    {
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BTreeItem<T>>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node")
                        .Add("m-treeview-node__root")
                        .AddIf("m-treeview-node--active primary--text", () => IsActive);
                })
                .Apply("level", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-treeview-node__level");
                 })
                .Apply("toggle", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-icon  ")
                        .Add("notranslate")
                        .Add("m-treeview-node__toggle")
                        .Add("m-icon--link")
                        .Add("mdi mdi-menu-down")
                        .AddTheme(Dark)
                        .AddIf("m-treeview-node__toggle--open", () => Expanded);
                })
                .Apply("checkbox", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-icon  ")
                        .Add("notranslate")
                        .Add("m-treeview-node__checkbox")
                        .Add("m-icon--link")
                        .Add("mdi")
                        .AddIf("mdi-checkbox-marked accent--text", () => Checked && !Indeterminate)
                        .AddIf("mdi-minus-box accent--text", () => Indeterminate)
                        .AddIf("mdi-checkbox-blank-outline", () => !Checked)
                        .AddTheme(Dark);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__content");
                })
                .Apply("prepend", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__prepend");
                })
                .Apply("label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__label");
                });

            AbstractProvider
                .Apply<BTreeItem<T>, MTreeItem<T>>(props =>
                {
                    props[nameof(OnItemClick)] = OnItemClick;
                    props[nameof(HandleCheckboxClick)] = HandleCheckboxClick;
                    props[nameof(DefaultCheckedExpression)] = DefaultCheckedExpression;
                    props[nameof(Checkable)] = Checkable;
                    props[nameof(PrependContent)] = PrependContent;
                });
        }
    }
}
