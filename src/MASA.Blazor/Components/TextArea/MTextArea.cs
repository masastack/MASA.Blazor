using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MTextArea : MTextField<string>
    {
        [Parameter]
        public bool AutoGrow { get; set; }

        [Parameter]
        public bool NoResize { get; set; }

        //TODO:rowHeight

        [Parameter]
        public int Rows { get; set; } = 5;

        public override string Tag => "textarea";

        public override Dictionary<string, object> InputAttrs => new()
        {
            { "rows", Rows }
        };

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-textarea";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--auto-grow", () => AutoGrow)
                        .AddIf($"{prefix}--no-resize", () => AutoGrow || NoResize);
                });
        }

        //TODO:oninput
    }
}
