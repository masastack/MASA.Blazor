using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MTreeviewNode<TItem, TKey> : BTreeviewNode<TItem, TKey>
    {
        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public string ActiveClass { get; set; } = "m-treeview-node--active";

        [Parameter]
        public string SelectedColor { get; set; } = "accent";

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node")
                        .AddIf("m-treeview-node--leaf", () => !HasChildren)
                        .AddIf("m-treeview-node--click", () => OpenOnClick)
                        .AddIf("m-treeview-node--disabled", () => Disabled)
                        .AddIf("m-treeview-node--rounded", () => Rounded)
                        .AddIf("m-treeview-node--shaped", () => Shaped)
                        .AddIf("m-treeview-node--selected", () => IsSelected);
                })
                .Apply("root", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__root")
                        .AddIf(ActiveClass, () => IsActive)
                        .AddTextColor(Color, () => IsActive);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color, () => IsActive);
                })
                .Apply("prepend", cssBuilder =>
                 {
                     cssBuilder
                         .Add("m-treeview-node__prepend");
                 })
                .Apply("append", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__append");
                })
                .Apply("label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__label");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__content");
                })
                .Apply("children", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__children");
                })
                .Apply("level", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview-node__level");
                });

            AbstractProvider
                .Apply<BIcon, MIcon>("checkbox", props =>
                 {
                     props[nameof(Class)] = "m-treeview-node__checkbox";
                     props[nameof(MIcon.Color)] = (IsSelected || IsIndeterminate) ? SelectedColor : null;
                     props[nameof(MIcon.Disabled)] = Disabled;
                     props[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnCheck);
                 })
                .Apply<BIcon, MIcon>("toggle", props =>
                {
                    var css = new CssBuilder()
                        .Add("m-treeview-node__toggle")
                        .AddIf("m-treeview-node__toggle--open", () => IsOpen)
                        .AddIf("m-treeview-node__toggle--loading", () => IsLoading)
                        .Class;
                    props[nameof(Class)] = css;
                    props[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnToggle);
                })
                .Apply(typeof(BTreeviewNodeNode<,,>), typeof(BTreeviewNodeNode<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeChildrenWrapper<,,>), typeof(BTreeviewNodeChildrenWrapper<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeLevel<,,>), typeof(BTreeviewNodeLevel<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeToggle<,,>), typeof(BTreeviewNodeToggle<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeCheckbox<,,>), typeof(BTreeviewNodeCheckbox<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeContent<,,>), typeof(BTreeviewNodeContent<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeChild<,,>), typeof(BTreeviewNodeChild<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodePrependSlot<,,>), typeof(BTreeviewNodePrependSlot<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeLabel<,,>), typeof(BTreeviewNodeLabel<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNodeAppendSlot<,,>), typeof(BTreeviewNodeAppendSlot<TItem, TKey, MTreeviewNode<TItem, TKey>>))
                .Apply(typeof(BTreeviewNode<,>), typeof(MTreeviewNode<TItem, TKey>), props =>
                {
                    props[nameof(Activatable)] = Activatable;
                    props[nameof(ActiveClass)] = ActiveClass;
                    props[nameof(Selectable)] = Selectable;
                    props[nameof(SelectedColor)] = SelectedColor;
                    props[nameof(Color)] = Color;
                    props[nameof(ExpandIcon)] = ExpandIcon;
                    props[nameof(IndeterminateIcon)] = IndeterminateIcon;
                    props[nameof(OffIcon)] = OffIcon;
                    props[nameof(OnIcon)] = OnIcon;
                    props[nameof(LoadingIcon)] = LoadingIcon;
                    props[nameof(ItemKey)] = ItemKey;
                    props[nameof(ItemText)] = ItemText;
                    props[nameof(ItemDisabled)] = ItemDisabled;
                    props[nameof(ItemChildren)] = ItemChildren;
                    //TODO:transition
                    props[nameof(LoadChildren)] = LoadChildren;
                    props[nameof(OpenOnClick)] = OpenOnClick;
                    props[nameof(Rounded)] = Rounded;
                    props[nameof(Shaped)] = Shaped;
                    props[nameof(Level)] = Level + 1;
                    props[nameof(SelectionType)] = SelectionType;
                    props[nameof(ParentIsDisabled)] = ParentIsDisabled;
                    props[nameof(AppendContent)] = AppendContent;
                    props[nameof(LabelContent)] = LabelContent;
                    props[nameof(PrependContent)] = PrependContent;
                });
        }

        public async Task HandleOnCheck(MouseEventArgs args)
        {
            if (IsLoading)
            {
                return;
            }

            CheckChildren();
            Treeview.UpdateSelected(Key);
            await Treeview.EmitSelectedAsync();
        }

        public async Task HandleOnToggle(MouseEventArgs args)
        {
            if (IsLoading)
            {
                return;
            }

            CheckChildren();
            await OpenAsync();
        }
    }
}
