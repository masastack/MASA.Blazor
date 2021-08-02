using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace MASA.Blazor
{
    public partial class MInput<TValue> : BInput, IThemeable
    {
        private NullableValue<TValue> _value;
        private EditContext _oldContext;

        [Parameter]
        public TValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    if (EditContext != null && ValueExpression != null)
                    {
                        EditContext.NotifyFieldChanged(FieldIdentifier);
                    }
                }
            }
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        protected FieldIdentifier FieldIdentifier { get; set; }

        [CascadingParameter]
        public EditContext EditContext { get; set; }

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
        public StringNumber Height { get; set; }

        public bool HasState
        {
            get
            {
                if (IsDisabled)
                {
                    return false;
                }

                return HasError;
            }
        }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public bool PersistentPlaceholder { get; set; }

        protected bool IsActive { get; set; }

        protected virtual bool IsDirty => Convert.ToString(_value).Length > 0;

        public bool LabelValue => IsFocused || IsLabelActive || PersistentPlaceholder;

        public bool IsLabelActive => IsDirty;

        public string ComputedColor => IsDisabled ? "" : Color ?? (Dark ? "white" : "primary");

        public bool HasError => Messages?.Count > 0;

        public string ValidationState
        {
            get
            {
                if (IsDisabled)
                {
                    return "";
                }

                if (HasError)
                {
                    return "error";
                }

                return ComputedColor;
            }
        }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
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
                        .AddIf($"{prefix}--is-label-active", () => IsDirty)
                        .AddIf($"{prefix}--is-dirty", () => IsDirty)
                        .AddIf($"{prefix}--is-disabled", () => IsDisabled)
                        .AddIf($"{prefix}--is-focused", () => IsFocused)
                        .AddIf($"{prefix}--is-loading", () => Loading)
                        .AddIf($"{prefix}--is-readonly", () => IsReadonly)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddTheme(IsDark);
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
                .Apply("append-inner", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__append-inner");
                })
                .Apply("icon-clearable", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__icon")
                        .Add($"{prefix}__icon--clear");
                })
                .Apply("icon", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__icon")
                        .Add($"{prefix}__icon--append");
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

            AbstractProvider
                .Apply<BMessage, MMessage>(properties =>
                {
                    properties[nameof(MMessage.Color)] = ValidationState;
                    properties[nameof(MMessage.Value)] = Messages;
                })
                .Apply<BIcon, MIcon>(properties =>
                {
                    properties[nameof(MIcon.Tag)] = IconTag.I;
                })
                .Apply<BIcon, MIcon>("clearable", properties =>
                {
                    properties[nameof(MIcon.Tag)] = IconTag.I;
                    properties[nameof(MIcon.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        if (ValueChanged.HasDelegate)
                            await ValueChanged.InvokeAsync(default);
                        else
                            Value = default;

                        if (OnClearClick.HasDelegate)
                            await OnClearClick.InvokeAsync();
                    });
                });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (_oldContext != EditContext)
            {
                if (_oldContext != null)
                {
                    _oldContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
                }

                if (ValueExpression != null)
                {
                    FieldIdentifier = FieldIdentifier.Create(ValueExpression);
                    EditContext.OnValidationStateChanged += EditContext_OnValidationStateChanged;
                }

                _oldContext = EditContext;
            }
        }

        private void EditContext_OnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            Messages = EditContext.GetValidationMessages(FieldIdentifier).ToList();
            StateHasChanged();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
            }
        }
    }
}
