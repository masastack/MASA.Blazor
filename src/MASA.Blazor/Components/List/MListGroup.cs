using BlazorComponent;
using MASA.Blazor.Components.List;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MListGroup : BListGroup
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool NoAction { get; set; }

        [Parameter]
        public bool SubGroup { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public string Color { get; set; } = "primary";

        protected override void SetComponentClass()
        {
            var prefix = "m-list-group";
            CssProvider
                .AsProvider<BListGroup>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => Expanded)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--no-action", () => NoAction)
                        .AddIf($"{prefix}--sub-group", () => SubGroup)
                        .AddTextColor(Color);
                })
                .Apply("items", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-group__items");
                });

            AbstractProvider
                .Apply<BListItem, MListGroupItem>(props =>
                {
                    props[nameof(MListGroupItem.NoGroup)] = true;
                    props[nameof(MListGroupItem.IsActive)] = Expanded;
                    props[nameof(MListGroupItem.Link)] = true;
                    props[nameof(MListGroupItem.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, args => ToggleExpansion());
                    props[nameof(MListGroupItem.Dark)] = Dark;
                })
                .Apply<BListItemIcon, MListGroupItemIcon>("prepend", props =>
                {
                    props[nameof(MListGroupItemIcon.Type)] = "prepend";
                })
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Dark)] = Dark;
                })
                .Apply<BListItemIcon, MListGroupItemIcon>("append", props =>
                {
                    props[nameof(MListGroupItemIcon.Type)] = "append";
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
