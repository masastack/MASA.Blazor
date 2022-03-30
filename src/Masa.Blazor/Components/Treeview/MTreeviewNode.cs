using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor
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
                .Apply<BIcon, MIcon>("checkbox", attrs =>
                 {
                     attrs[nameof(Class)] = "m-treeview-node__checkbox";
                     attrs[nameof(MIcon.Color)] = (IsSelected || IsIndeterminate) ? SelectedColor : null;
                     attrs[nameof(MIcon.Disabled)] = Disabled;
                     attrs[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnCheckAsync);
                 })
                .Apply<BIcon, MIcon>("toggle", attrs =>
                {
                    var css = new CssBuilder()
                        .Add("m-treeview-node__toggle")
                        .AddIf("m-treeview-node__toggle--open", () => IsOpen)
                        .AddIf("m-treeview-node__toggle--loading", () => IsLoading)
                        .Class;
                    attrs[nameof(Class)] = css;
                    attrs[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnToggleAsync);
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
                .Apply(typeof(BTreeviewNode<,>), typeof(MTreeviewNode<TItem, TKey>), attrs =>
                {
                    attrs[nameof(Activatable)] = Activatable;
                    attrs[nameof(ActiveClass)] = ActiveClass;
                    attrs[nameof(Selectable)] = Selectable;
                    attrs[nameof(SelectedColor)] = SelectedColor;
                    attrs[nameof(Color)] = Color;
                    attrs[nameof(ExpandIcon)] = ExpandIcon;
                    attrs[nameof(IndeterminateIcon)] = IndeterminateIcon;
                    attrs[nameof(OffIcon)] = OffIcon;
                    attrs[nameof(OnIcon)] = OnIcon;
                    attrs[nameof(LoadingIcon)] = LoadingIcon;
                    attrs[nameof(ItemKey)] = ItemKey;
                    attrs[nameof(ItemText)] = ItemText;
                    attrs[nameof(ItemDisabled)] = ItemDisabled;
                    attrs[nameof(ItemChildren)] = ItemChildren;
                    //TODO:transition
                    attrs[nameof(LoadChildren)] = LoadChildren;
                    attrs[nameof(OpenOnClick)] = OpenOnClick;
                    attrs[nameof(Rounded)] = Rounded;
                    attrs[nameof(Shaped)] = Shaped;
                    attrs[nameof(Level)] = Level + 1;
                    attrs[nameof(SelectionType)] = SelectionType;
                    attrs[nameof(ParentIsDisabled)] = ParentIsDisabled;
                    attrs[nameof(AppendContent)] = AppendContent;
                    attrs[nameof(LabelContent)] = LabelContent;
                    attrs[nameof(PrependContent)] = PrependContent;
                });
        }

        public async Task HandleOnCheckAsync(MouseEventArgs args)
        {
            if (IsLoading)
            {
                return;
            }

            await CheckChildrenAsync();
            Treeview.UpdateSelected(Key);
            await Treeview.EmitSelectedAsync();
        }

        public async Task HandleOnToggleAsync(MouseEventArgs args)
        {
            if (IsLoading)
            {
                return;
            }

            await CheckChildrenAsync();
            await OpenAsync();
        }
    }
}
