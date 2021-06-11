using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MExpansionPanelHeader : BExpansionPanelHeader
    {

        [Parameter]
        public string Icon { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-header")
                        .AddIf("m-expansion-panel-header--active", () => !ExpansionPanel.Active)
                        .AddIf("m-expansion-panel--disabled m-btn--disabled", () => ExpansionPanels.Disabled)
                        .AddIf("m-expansion-panel--disabled m-btn--disabled", () => ExpansionPanel.Disabled);
                })
                .Apply("headerIcon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-header__icon");
                })
                 .Apply("icons", cssBuilder =>
                 {
                     var prefix = "fas fa-angle";

                     if (!ExpansionPanel.Disabled)
                     {
                         cssBuilder
                           .AddIf(Icon, () => !string.IsNullOrWhiteSpace(Icon))
                           .AddIf($"{prefix}-up", () => ExpansionPanel.Active)
                           .AddIf($"{prefix}-down", () => !ExpansionPanel.Active);
                     }
                 });
        }

    }
}
