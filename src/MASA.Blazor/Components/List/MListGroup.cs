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
        protected bool Disabled { get; set; }

        protected bool NoAction { get; set; }

        protected bool SubGroup { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public string Color { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-list-group";
            CssProvider
                .AsProvider<BListGroup>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--no-action", () => NoAction)
                        .AddIf($"{prefix}--sub-group", () => SubGroup)
                        .AddIf($"{Color}--text", () => !string.IsNullOrEmpty(Color));
                })
                .Apply("items", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-group__items");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !IsActive);
                });

            SlotProvider
                .Apply<BListItem, MListGroupItem>(props =>
                {
                    props[nameof(MListGroupItem.IsActive)] = IsActive;
                    props[nameof(MListGroupItem.Link)] = true;
                    props[nameof(MListGroupItem.Click)] = EventCallback.Factory.Create<MouseEventArgs>(this, args => IsActive = !IsActive);
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

            AppendIcon = "mdi-chevron-down";
        }
    }
}
