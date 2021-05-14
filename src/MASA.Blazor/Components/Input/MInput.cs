using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MInput<TValue> : BInput
    {
        [Parameter]
        public bool HasState { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public bool IsFocused { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool IsReadonly { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public string ValidationState { get; set; } = "primary";

        protected bool? IsActive { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-input";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--has-state", () => HasState)
                        .AddIf($"{prefix}--hide-details", () => !ShowDetails)
                        .AddIf($"{prefix}--is-label-active", () => IsActive ?? Value != null)
                        .AddIf($"{prefix}--is-dirty", () => IsActive ?? Value != null)
                        .AddIf($"{prefix}--is-disabled", () => IsDisabled)
                        .AddIf($"{prefix}--is-focused", () => IsFocused)
                        .AddIf($"{prefix}--is-loading", () => Loading)
                        .AddIf($"{prefix}--is-readonly", () => IsReadonly)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddTheme(Dark);
                })
                .Apply("prepend", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__prepend-outer");
                })
                .Apply("append", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__append-outer");
                })
                .Apply("control", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__control");
                })
                .Apply("input-slot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__slot");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height);
                });

            SlotProvider
                .Apply<BMessage, MMessage>(properties =>
                {
                    properties[nameof(MMessage.Color)] = ValidationState;
                    properties[nameof(MMessage.Value)] = Messages;
                });
        }
    }
}
