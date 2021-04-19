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
        [Parameter]
        public string AlignSelf { get; set; }

        public override void SetComponentClass()
        {
            CssBuilder
                .AddIf("col", () => IsNullOrDefault(Span))
                .AddIf(() => $"col-{Span.Value}", () => !IsNullOrDefault(Span))
                .AddIf(() => $"offset-{Offset.Value}", () => !IsNullOrDefault(Offset))
                .AddIf(() => $"order-{Order.Value}", () => !IsNullOrDefault(Order))
                //'auto', 'start', 'end', 'center', 'baseline', 'stretch'
                .AddIf(() => $"align-self-{AlignSelf}", () => !string.IsNullOrEmpty(AlignSelf));
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
