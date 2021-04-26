using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MFooter : BFooter
    {
        /// <summary>
        /// Whether dark theme
        /// </summary>
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BFooter>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-footer")
                        .Add("m-sheet")
                        .AddTheme(Dark);
                });
        }
    }
}
