namespace Masa.Blazor
{
    public class MTreeview<TItem, TKey> : BTreeview<TItem, TKey>, IThemeable
    {
        [Parameter]
        public bool Hoverable { get; set; }

        [Parameter]
        public bool Dense { get; set; }


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

        [Parameter]
        public Func<TItem, Task> LoadChildren { get; set; }

        [Parameter]
        public bool Activatable { get; set; }

        [Parameter]
        public string IndeterminateIcon { get; set; } = "mdi-minus-box";

        [Parameter]
        public string OnIcon { get; set; } = "mdi-checkbox-marked";

        [Parameter]
        public string OffIcon { get; set; } = "mdi-checkbox-blank-outline";

        [Parameter]
        public bool OpenOnClick { get; set; }

        [Parameter]
        public bool ParentIsDisabled { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-treeview")
                        .AddIf("m-treeview--hoverable", () => Hoverable)
                        .AddIf("m-treeview--dense", () => Dense)
                        .AddTheme(IsDark);
                });

            AbstractProvider
                .Apply(typeof(BTreeviewNodeChild<,,>), typeof(BTreeviewNodeChild<TItem, TKey, MTreeview<TItem, TKey>>))
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
                    attrs[nameof(SelectionType)] = SelectionType;
                    attrs[nameof(ParentIsDisabled)] = ParentIsDisabled;
                    attrs[nameof(AppendContent)] = AppendContent;
                    attrs[nameof(LabelContent)] = LabelContent;
                    attrs[nameof(PrependContent)] = PrependContent;
                });
        }
    }
}
