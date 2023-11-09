namespace Masa.Blazor
{
    public partial class MInput<TValue> : BInput<TValue>, IThemeable, IFilterInput
    {
        [Inject]
        private I18n I18n { get; set; } = null!;

        [CascadingParameter]
        protected MInputsFilter? InputsFilter { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        public string? TextColor { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public StringNumber? MinHeight { get; set; }

        [Parameter]
        public EventCallback<TValue> OnChange { get; set; }

        /// <summary>
        /// The required rule built-in.
        /// </summary>
        [Parameter]
        public bool Required
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        /// <summary>
        /// The error message when the required rule is not satisfied.
        /// </summary>
        [Parameter, MassApiParameter("$masaBlazor.required")]
        public string RequiredMessage
        {
            get => _requiredMessage ?? I18n.T("$masaBlazor.required");
            set => _requiredMessage = value;
        }

        #region built-in Required rule

        private static readonly Func<TValue, bool> s_defaultRequiredRule = v =>
        {
            if (v == null)
            {
                return false;
            }

            if (v is string str)
            {
                return !string.IsNullOrWhiteSpace(str);
            }

            return true;
        };

        private string? _requiredMessage;

        #endregion

        public virtual string ComputedColor => IsDisabled ? "" : Color ?? (IsDark ? "white" : "primary");

        public virtual bool HasColor => false;

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

                if (HasSuccess)
                {
                    return "success";
                }

                if (HasColor)
                {
                    return ComputedColor;
                }

                return "";
            }
        }

        protected virtual bool IsDirty => Convert.ToString(LazyValue)?.Length > 0;

        protected override IEnumerable<Func<TValue, StringBoolean>> InternalRules
        {
            get
            {
                if (Required)
                {
                    var rules = new List<Func<TValue, StringBoolean>>() { v => s_defaultRequiredRule(v) ? true : RequiredMessage };
                    return Rules is null ? rules : rules.Concat(Rules);
                }

                return Rules ?? Enumerable.Empty<Func<TValue, StringBoolean>>();
            }
        }

        public virtual bool IsLabelActive => IsDirty;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (InputsFilter != null)
            {
                InputsFilter.RegisterInput(this);
                Dense = InputsFilter.Dense;
                HideDetails = InputsFilter.HideDetails;
            }
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);
            watcher.Watch<bool>(nameof(Required), () =>
            {
                // waiting for InternalValue to be assigned
                NextTick(InternalValidate);
            }).Watch<IEnumerable<Func<TValue, StringBoolean>>>(nameof(Rules), () =>
            {
                // waiting for InternalValue to be assigned
                NextTick(InternalValidate);
            });
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
                        .AddMinHeight(MinHeight)
                        .AddBackgroundColor(BackgroundColor);
                });

            AbstractProvider
                .ApplyInputDefault<TValue>()
                .ApplyInputPrependIcon(typeof(MIcon), attrs =>
                {
                    attrs[nameof(MIcon.Color)] = ValidationState;
                    attrs[nameof(MIcon.Dark)] = Dark;
                    attrs[nameof(MIcon.Disabled)] = IsDisabled;
                    attrs[nameof(MIcon.Light)] = Light;
                })
                .ApplyInputLabel(typeof(MLabel), attrs =>
                {
                    attrs[nameof(MLabel.Color)] = ValidationState;
                    attrs[nameof(MLabel.Dark)] = Dark;
                    attrs[nameof(MLabel.Disabled)] = IsDisabled;
                    attrs[nameof(MLabel.Focused)] = HasState;
                    attrs[nameof(MLabel.For)] = Id;
                    attrs[nameof(MLabel.Light)] = Light;
                })
                .ApplyInputMessages(typeof(MMessages), attrs =>
                {
                    attrs[nameof(MMessages.Color)] = HasHint ? "" : ValidationState;
                    attrs[nameof(MMessages.Dark)] = Dark;
                    attrs[nameof(MMessages.Light)] = Light;
                    attrs[nameof(MMessages.Value)] = MessagesToDisplay;
                    attrs[nameof(MMessages.ChildContent)] = MessageContent;
                })
                .ApplyInputAppendIcon(typeof(MIcon), attrs =>
                {
                    attrs[nameof(MIcon.Color)] = ValidationState;
                    attrs[nameof(MIcon.Dark)] = Dark;
                    attrs[nameof(MIcon.Disabled)] = IsDisabled;
                    attrs[nameof(MIcon.Light)] = Light;
                });
        }

        protected async Task TryInvokeFieldChangeOfInputsFilter(bool isClear = false)
        {
            if (InputsFilter is null)
            {
                return;
            }

            await InputsFilter.FieldChange(ValueIdentifier.FieldName, isClear);
        }

        public void ResetFilter()
        {
            Reset();
        }
    }
}
