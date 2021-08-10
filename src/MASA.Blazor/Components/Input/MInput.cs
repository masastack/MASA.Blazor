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
        private EditContext _oldEditContext;
        private bool _isFocused;

        ////TODO:props
        //[Parameter]
        //public RenderFragment MessageContent { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        public virtual string ComputedColor => IsDisabled ? "" : Color ?? (Dark ? "white" : "primary");

        public virtual string ValidationState
        {
            get
            {
                if (IsDisabled)
                {
                    return "";
                }

                if (HasError)
                {
                    //TODO:shouldValidate
                    return "error";
                }

                //TODO:success

                return ComputedColor;
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

        [Parameter]
        public virtual TValue Value
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
                    NotifyValueChanged();
                }
            }
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        [CascadingParameter]
        public EditContext EditContext { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public bool Readonly { get; set; }

        [CascadingParameter]
        public BForm Form { get; set; }

        [Parameter]
        public string Hint { get; set; }

        [Parameter]
        public bool PersistentHint { get; set; }

        protected FieldIdentifier ValueIdentifier { get; set; }

        protected bool EditContextChanged => _oldEditContext != EditContext;

        public virtual bool IsReadonly => Readonly || (Form != null && Form.Readonly);

        public virtual bool IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    StateHasChanged();
                }
            }
        }

        public virtual bool IsDirty => Convert.ToString(_value).Length > 0;

        public virtual bool IsLabelActive => IsDirty;

        public virtual bool HasState
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

        public virtual bool IsDisabled => Disabled || (Form != null && Form.Disabled);

        public virtual bool HasError => Messages?.Count > 0;

        //TODO:
        public virtual bool HasMessages => false;

        public virtual bool HasHint => !HasMessages && Hint != null && (PersistentHint || IsFocused);

        public virtual List<string> MessagesToDisplay
        {
            get
            {
                if (HasHint)
                {
                    return new List<string>
                    {
                        Hint
                    };
                }

                //TODO:
                //if (!HasMessages)
                //{
                //    return new List<string>();
                //}

                return Messages;
            }
        }

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
                .Apply(typeof(BInputContent<>), typeof(BInputContent<MInput<TValue>>))
                .Apply(typeof(BInputPrependSlot<>), typeof(BInputPrependSlot<MInput<TValue>>))
                .Apply(typeof(BInputSlot<>), typeof(BInputSlot<MInput<TValue>>))
                .Apply(typeof(BInputIcon<>), typeof(BInputIcon<MInput<TValue>>))
                .Apply<BIcon, MIcon>("prepend-icon", props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = Disabled;
                    props[nameof(MIcon.Light)] = Light;
                })
                .Apply(typeof(BInputControl<>), typeof(BInputControl<MInput<TValue>>))
                .Apply(typeof(BInputInputSlot<>), typeof(BInputInputSlot<MInput<TValue>>))
                .Apply(typeof(BInputDefaultSlot<>), typeof(BInputDefaultSlot<MInput<TValue>>))
                .Apply(typeof(BInputLabel<>), typeof(BInputLabel<MInput<TValue>>))
                .Apply<BLabel, MLabel>(props =>
                {
                    props[nameof(MLabel.Color)] = ValidationState;
                    props[nameof(MLabel.Dark)] = Dark;
                    props[nameof(MLabel.Disabled)] = IsDisabled;
                    props[nameof(MLabel.Focused)] = HasState;
                    //TODO:for
                    props[nameof(MLabel.Light)] = Light;
                })
                .Apply(typeof(BInputMessages<>), typeof(BInputMessages<MInput<TValue>>))
                .Apply<BMessages, MMessages>(props =>
                {
                    props[nameof(MMessages.Color)] = HasHint ? "" : ValidationState;
                    props[nameof(MMessages.Value)] = MessagesToDisplay;
                    props[nameof(MMessages.Dark)] = Dark;
                    props[nameof(MMessages.Light)] = Light;
                    //TODO:添加插槽
                })
                .Apply(typeof(BInputAppendSlot<>), typeof(BInputAppendSlot<MInput<TValue>>))
                .Apply<BIcon, MIcon>("append-icon", props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = Disabled;
                    props[nameof(MIcon.Light)] = Light;
                });
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (EditContextChanged)
            {
                if (_oldEditContext != null)
                {
                    _oldEditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
                }

                if (ValueExpression != null)
                {
                    ValueIdentifier = FieldIdentifier.Create(ValueExpression);
                    EditContext.OnValidationStateChanged += EditContext_OnValidationStateChanged;
                }

                _oldEditContext = EditContext;
            }
        }

        private void EditContext_OnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            Messages = EditContext.GetValidationMessages(ValueIdentifier).ToList();
            StateHasChanged();
        }

        protected void NotifyValueChanged()
        {
            if (EditContext != null && ValueExpression != null)
            {
                EditContext.NotifyFieldChanged(ValueIdentifier);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged -= EditContext_OnValidationStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}
