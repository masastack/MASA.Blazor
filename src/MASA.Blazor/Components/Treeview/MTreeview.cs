using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MTreeview<TItem, TKey> : BTreeview<TItem, TKey>, IThemeable
    {
        [Parameter]
        public bool Hoverable { get; set; }

        [Parameter]
        public bool Dense { get; set; }

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
                    props[nameof(LoadChildren)] = LoadChildren;
                    //TODO:transition
                    props[nameof(LoadChildren)] = LoadChildren;
                    props[nameof(OpenOnClick)] = OpenOnClick;
                    props[nameof(Rounded)] = Rounded;
                    props[nameof(Shaped)] = Shaped;
                    props[nameof(SelectionType)] = SelectionType;
                    props[nameof(ParentIsDisabled)] = ParentIsDisabled;
                    props[nameof(AppendContent)] = AppendContent;
                    props[nameof(LabelContent)] = LabelContent;
                    props[nameof(PrependContent)] = PrependContent;
                });
        }
    }
}
