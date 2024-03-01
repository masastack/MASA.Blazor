﻿namespace Masa.Blazor
{
    public class MTreeview<TItem, TKey> : BTreeview<TItem, TKey>, IThemeable where TKey : notnull
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
        public string? ActiveClass { get; set; }

        [Parameter]
        [MasaApiParameter("accent")]
        public string? SelectedColor { get; set; } = "accent";

        [Parameter]
        [MasaApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public EventCallback<TItem> LoadChildren { get; set; }

        [Parameter]
        public bool Activatable { get; set; }

        [Parameter]
        [MasaApiParameter("$checkboxIndeterminate")]
        public string? IndeterminateIcon { get; set; } = "$checkboxIndeterminate";

        [Parameter]
        [MasaApiParameter("$checkboxOn")]
        public string? OnIcon { get; set; } = "$checkboxOn";

        [Parameter]
        [MasaApiParameter("$checkboxOff")]
        public string? OffIcon { get; set; } = "$checkboxOff";

        [Parameter]
        public bool OpenOnClick { get; set; }
        
        [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif

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
                        .AddTheme(IsDark, IndependentTheme);
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
                    attrs[nameof(AppendContent)] = AppendContent;
                    attrs[nameof(LabelContent)] = LabelContent;
                    attrs[nameof(PrependContent)] = PrependContent;
                });
        }
    }
}
