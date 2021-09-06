using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MInput<TValue> : BInput<TValue>, IThemeable
    {
        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        public virtual string ComputedColor => IsDisabled ? "" : Color ?? (IsDark ? "white" : "primary");

        public virtual bool HasColor { get; }

        public virtual string ValidationState
        {
            get
            {
                if (IsDisabled)
                {
                    return "";
                }

                if (HasError && ShouldValidate)
                {
                    return "error";
                }

                if (HasColor)
                {
                    return ComputedColor;
                }

                return "";
            }
        }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public virtual bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        }

        public virtual bool IsDirty
        {
            get
            {
                if (Value == null)
                {
                    return false;
                }

                return Convert.ToString(Value).Length > 0;
            }
        }

        public virtual bool IsLabelActive => IsDirty;

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-input";
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--has-state", () => HasState)
                        .AddIf($"{prefix}--hide-details", () => !ShowDetails)
                        .AddIf($"{prefix}--is-label-active", () => IsLabelActive)
                        .AddIf($"{prefix}--is-dirty", () => IsDirty)
                        .AddIf($"{prefix}--is-disabled", () => IsDisabled)
                        .AddIf($"{prefix}--is-focused", () => IsFocused)
                        .AddIf($"{prefix}--is-loading", () => Loading != false && Loading != null)
                        .AddIf($"{prefix}--is-readonly", () => IsReadonly)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddTheme(IsDark)
                        .AddTextColor(ValidationState);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(ValidationState);
                })
                .Apply("prepend-outer", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__prepend-outer");
                })
                .Apply("prepend-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon")
                        .Add("m-input__icon--prepend");
                })
                .Apply("append-outer", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__append-outer");
                })
                .Apply("append-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon")
                        .Add("m-input__icon--append");
                })
                .Apply("control", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__control");
                })
                .Apply("input-slot", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__slot")
                        .AddBackgroundColor(BackgroundColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddBackgroundColor(BackgroundColor);
                });

            AbstractProvider
                .ApplyInputDefault<TValue>()
                .ApplyInputPrependIcon(typeof(MIcon), props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = IsDisabled;
                    props[nameof(MIcon.Light)] = Light;
                })
                .ApplyInputLabel(typeof(MLabel), props =>
                 {
                     props[nameof(MLabel.Color)] = ValidationState;
                     props[nameof(MLabel.Dark)] = Dark;
                     props[nameof(MLabel.Disabled)] = IsDisabled;
                     props[nameof(MLabel.Focused)] = HasState;
                     props[nameof(MLabel.For)] = Id;
                     props[nameof(MLabel.Light)] = Light;
                 })
                .ApplyInputMessages(typeof(MMessages), props =>
                 {
                     props[nameof(MMessages.Color)] = HasHint ? "" : ValidationState;
                     props[nameof(MMessages.Dark)] = Dark;
                     props[nameof(MMessages.Light)] = Light;
                     props[nameof(MMessages.Value)] = MessagesToDisplay;
                     props[nameof(MMessages.ChildContent)] = MessageContent;
                 })
                .ApplyInputAppendIcon(typeof(MIcon), props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = IsDisabled;
                    props[nameof(MIcon.Light)] = Light;
                });
        }
    }
}
