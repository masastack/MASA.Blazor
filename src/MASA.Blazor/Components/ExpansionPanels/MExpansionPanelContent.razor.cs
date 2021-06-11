using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MExpansionPanelContent : BExpansionPanelContent
    {
        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content mt-2");
                }, styleBuilder =>
                {
                    styleBuilder.AddIf("display:none", () => !ExpansionPanel.Active);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-expansion-panel-content__wrap");
                });

        }

    }
}
