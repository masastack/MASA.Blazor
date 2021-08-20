using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MTextField<TValue> : MInput<TValue>, ITextField<TValue>
    {
        [Parameter]
        public bool Clearable { get; set; }

        [Parameter]
        public bool PersistentPlaceholder { get; set; }

        [Parameter]
        public string ClearIcon { get; set; } = "mdi-close";

        [Parameter]
        public bool FullWidth { get; set; }

        [Parameter]
        public string Prefix { get; set; }

        [Parameter]
        public bool SingleLine { get; set; }

        [Parameter]
        public bool Solo { get; set; }

        [Parameter]
        public bool SoloInverted { get; set; }

        public bool IsSolo => Solo || SoloInverted;

        [Parameter]
        public bool Flat { get; set; }

        [Parameter]
        public bool Filled { get; set; }

        public bool IsBooted { get; set; } = true;

        public bool IsEnclosed => Filled || IsSolo || Outlined;

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Reverse { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public string Type { get; set; } = "text";

        [Parameter]
        public RenderFragment PrependInnerContent { get; set; }

        public bool ShowLabel => HasLabel && !(IsSingle && LabelValue);

        public bool IsSingle => IsSolo || SingleLine || FullWidth || (Filled && !HasLabel);

        [Parameter]
        public string AppendOuterIcon { get; set; }

        [Parameter]
        public RenderFragment AppendOuterContent { get; set; }

        [Parameter]
        public string PrependInnerIcon { get; set; }

        public virtual bool LabelValue => IsFocused || IsLabelActive || PersistentPlaceholder;

        public ElementReference InputRef { get; set; }

        //TODO:
        public int LabelWidth => LabelValue ? ComputeLabeLength * 6 : 0;

        public int ComputeLabeLength
        {
            get
            {
                if (string.IsNullOrEmpty(Label))
                {
                    return 0;
                }

                var length = 0;
                for (int i = 0; i < Label.Length; i++)
                {
                    if (Label[i] > 127)
                    {
                        length += 2;
                    }
                    else
                    {
                        length += 1;
                    }
                }

                return length + 1;
            }
        }

        public string LegendInnerHTML => "&#8203;";

        [Parameter]
        public string Suffix { get; set; }

        [Parameter]
        public bool Autofocus { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnInput { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter]
        public RenderFragment ProgressContent { get; set; }

        [Parameter]
        public StringNumber LoaderHeight { get; set; } = 2;

        [Parameter]
        public StringNumberBoolean Counter { get; set; }

        public bool HasCounter => Counter != false && Counter != null;

        public override string ComputedColor
        {
            get
            {
                if (!SoloInverted || !IsFocused)
                {
                    return base.ComputedColor;
                }

                return Color ?? "primary";
            }
        }

        public override bool HasColor => IsFocused;

        public override string ValidationState
        {
            get
            {
                if (IsDisabled)
                {
                    return "";
                }

                //TODO:shouldValidate
                if (HasError)
                {
                    return "error";
                }

                //TODO:success
                if (HasColor)
                {
                    return ComputedColor;
                }

                return "";
            }
        }

        public virtual string Tag => "input";

        public virtual Dictionary<string, object> InputAttrs => new()
        {
            { "type", Type }
        };

        public virtual async Task HandleOnChange(ChangeEventArgs args)
        {
            try
            {
                Value = (TValue)Convert.ChangeType(args.Value, typeof(TValue));
            }
            catch (Exception)
            {
                Value = default;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnBlur(FocusEventArgs args)
        {
            IsFocused = false;

            if (OnBlur.HasDelegate)
            {
                await OnBlur.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnInput(ChangeEventArgs args)
        {
            //TODO:badInput
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnFocus(FocusEventArgs args)
        {
            //TODO:focus element
            if (!IsFocused)
            {
                IsFocused = true;
                if (OnFocus.HasDelegate)
                {
                    await OnFocus.InvokeAsync(args);
                }
            }
        }

        public virtual async Task HandleOnKeyDown(KeyboardEventArgs args)
        {
            if (OnKeyDown.HasDelegate)
            {
                await OnKeyDown.InvokeAsync(args);
            }
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            var prefix = "m-text-field";
            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--full-width", () => FullWidth)
                        .AddIf($"{prefix}--prefix", () => Prefix != null)
                        .AddIf($"{prefix}--single-line", () => IsSingle)
                        .AddIf($"{prefix}--solo", () => IsSolo)
                        .AddIf($"{prefix}--solo-inverted", () => SoloInverted)
                        .AddIf($"{prefix}--solo-flat", () => Flat)
                        .AddIf($"{prefix}--filled", () => Filled)
                        .AddIf($"{prefix}--is-booted", () => IsBooted)
                        .AddIf($"{prefix}--enclosed", () => IsEnclosed)
                        .AddIf($"{prefix}--reverse", () => Reverse)
                        .AddIf($"{prefix}--outlined", () => Outlined)
                        .AddIf($"{prefix}--placeholder", () => Placeholder != null)
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--shaped", () => Shaped);
                })
                .Merge("text-field-details", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__details");
                })
                .Merge("input-slot", cssBuilder =>
                {
                    cssBuilder
                        .AddBackgroundColor(BackgroundColor);
                })
                .Apply("prepend-inner", cssBuilder =>
                {
                    cssBuilder
                        .Add($"m-input__prepend-inner");
                })
                .Apply("append-inner", cssBuilder =>
                {
                    cssBuilder
                        .Add($"m-input__append-inner");
                })
                .Apply("append-outer-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon")
                        .Add("m-input__icon--append-outter");
                })
                .Apply("prepend-inner-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon")
                        .Add("m-input__icon--prepend-inner");
                })
                .Apply("clear-icon", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__icon")
                        .Add("m-input__icon--clear");
                })
                .Apply("legend", styleAction: styleBuilder =>
                {
                    var width = (!SingleLine && (LabelValue || IsDirty)) ? LabelWidth : 0;
                    styleBuilder
                        .AddIf(() => $"width:{width}px", () => !IsSingle);
                })
                .Apply("legend-span", cssBuilder =>
                {
                    cssBuilder
                        .Add("notranslate");
                })
                .Apply("text-field-slot", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__slot");
                })
                .Apply("text-field-prefix", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__prefix");
                })
                .Apply("text-field-suffix", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-text-field__suffix");
                });

            AbstractProvider
                .Merge(typeof(BTextFieldAffix<,>), typeof(BTextFieldAffix<TValue, MTextField<TValue>>))
                .Merge(typeof(BInputAppendSlot<>), typeof(BTextFieldAppendSlot<TValue>))
                .Merge(typeof(BTextFieldClearIcon<,>), typeof(BTextFieldClearIcon<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldCounter<,>), typeof(BTextFieldCounter<TValue, MTextField<TValue>>))
                .Merge(typeof(BInputDefaultSlot<>), typeof(BTextFieldDefaultSlot<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldFieldset<,>), typeof(BTextFieldFieldset<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldIconSlot<,>), typeof(BTextFieldIconSlot<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldInput<,>), typeof(BTextFieldInput<TValue, MTextField<TValue>>))
                .Merge(typeof(BInputInputSlot<>), typeof(BTextFieldInputSlot<TValue>))
                .Merge(typeof(BTextFieldLegend<,>), typeof(BTextFieldLegend<TValue, MTextField<TValue>>))
                .Merge(typeof(BInputMessages<>), typeof(BTextFieldMessages<TValue>))
                .Merge(typeof(BTextFieldPrependInnerSlot<,>), typeof(BTextFieldPrependInnerSlot<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldProgress<,>), typeof(BTextFieldProgress<TValue, MTextField<TValue>>))
                .Merge(typeof(BTextFieldTextFieldSlot<,>), typeof(BTextFieldTextFieldSlot<TValue, MTextField<TValue>>))
                .Merge(typeof(BInputLabel<>), typeof(BTextFieldLabel<TValue>))
                .Merge<BLabel>(props =>
                {
                    props[nameof(MLabel.Absolute)] = true;
                    props[nameof(MLabel.Focused)] = !IsSingle && (IsFocused || ValidationState != null);
                    //TODO:left,right
                    props[nameof(MLabel.Value)] = LabelValue;
                })
                .Apply<BProcessLinear, MProcessLinear>(props =>
                {
                    props[nameof(MProcessLinear.Absolute)] = true;
                    props[nameof(MProcessLinear.Color)] = (Loading == true || Loading == "") ? (Color ?? "primary") : Loading.ToString();
                    props[nameof(MProcessLinear.Height)] = LoaderHeight;
                    props[nameof(MProcessLinear.Indeterminate)] = true;
                })
                .Apply<BIcon, MIcon>("clear-icon", props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = Disabled;
                    props[nameof(MIcon.Light)] = Light;
                })
                .Apply<BIcon, MIcon>("append-outer-icon", props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = Disabled;
                    props[nameof(MIcon.Light)] = Light;
                })
                .Apply<BIcon, MIcon>("prepend-inner-icon", props =>
                {
                    props[nameof(MIcon.Color)] = ValidationState;
                    props[nameof(MIcon.Dark)] = Dark;
                    props[nameof(MIcon.Disabled)] = Disabled;
                    props[nameof(MIcon.Light)] = Light;
                });
        }

        public async Task HandleOnClear(MouseEventArgs args)
        {
            Value = default;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
        }
    }
}
