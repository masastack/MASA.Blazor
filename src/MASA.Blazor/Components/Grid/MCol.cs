using BlazorComponent;
using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MCol : BCol
    {
        /// <summary>
        /// 'auto', 'start', 'end', 'center', 'baseline', 'stretch'
        /// </summary>
        [Parameter]
        public string Align { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply<BCol>(cssBuilder =>
                {
                    cssBuilder
                        .Add("col")
                        .AddIf(() => $"col-{Span.Value}", () => Span != null)
                        .AddIf(() => $"offset-{Offset.Value}", () => Offset != null)
                        .AddIf(() => $"order-{Order.Value}", () => Order != null)
                        .AddIf(() => $"align-self-{Align}", () => !string.IsNullOrEmpty(Align))
                        .AddIf(() => $"col-sm-{Sm.Value}", () => Sm.Value != default)
                        .AddIf(() => $"col-md-{Md.Value}", () => Md.Value != default)
                        .AddIf(() => $"col-lg-{Lg.Value}", () => Lg.Value != default)
                        .AddIf(() => $"col-xl-{Xl.Value}", () => Xl.Value != default);
                });
        }
    }
}
