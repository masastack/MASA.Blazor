namespace Masa.Blazor
{
    public partial class MListGroup : BListGroup
    {
        [Parameter]
        public string ActiveClass { get; set; }

        [Parameter]
        public bool NoAction { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            var prefix = "m-list-group";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--no-action", () => NoAction)
                        .AddIf($"{prefix}--sub-group", () => SubGroup)
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color);
                })
                .Apply("items", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-group__items");
                });

            AbstractProvider
                .Apply<BListItem, MListItem>(attrs =>
                {
                    attrs[nameof(MListItem.IsActive)] = IsActive;
                    attrs[nameof(MListItem.Link)] = true;
                    attrs[nameof(MListItem.Dark)] = Dark;
                    attrs[nameof(MListItem.Class)] = IsActive ? ActiveClass + " " + "m-list-group__header" : "m-list-group__header";
                    attrs["role"] = "button";
                    attrs["aria-expanded"] = IsActive;
                })
                .Apply<BListItemIcon, MListItemIcon>("prepend", attrs =>
                {
                    attrs[nameof(MListItemIcon.Class)] = "m-list-group__header__prepend-icon";
                })
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Dark)] = Dark;
                })
                .Apply<BListItemIcon, MListItemIcon>("append", attrs =>
                {
                    attrs[nameof(MListItemIcon.Class)] = "m-list-group__header__append-icon";
                });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (SubGroup)
            {
                if (PrependIcon == null)
                {
                    PrependIcon = "mdi-menu-down";
                }
            }
            else
            {
                if (AppendIcon == null)
                {
                    AppendIcon = "mdi-chevron-down";
                }
            }
        }
    }
}