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
                        .AddIf(() => $"col-{Span.Value}", () => !IsNullOrDefault(Span))
                        .AddIf(() => $"offset-{Offset.Value}", () => !IsNullOrDefault(Offset))
                        .AddIf(() => $"order-{Order.Value}", () => !IsNullOrDefault(Order))
                        .AddIf(() => $"align-self-{Align}", () => !string.IsNullOrEmpty(Align));
                });
        }

        private bool IsNullOrDefault(OneOf<string, int> span)
        {
            if (span.IsT0)
            {
                return string.IsNullOrEmpty(span.AsT0);
            }

            if (span.IsT1)
            {
                return span.AsT1 == default;
            }

            return true;
        }
    }
}
