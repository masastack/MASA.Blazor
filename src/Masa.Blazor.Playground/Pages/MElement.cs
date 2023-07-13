using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Masa.Blazor.Playground.Pages;

    public class MElement : NextTickComponentBase
    {
        [Parameter]
        public string Tag
        {
            get => _tag ?? "div";
            set => _tag = value;
        }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public string? Style { get; set; }

        [Parameter]
        public Action<ElementReference>? ReferenceCaptureAction { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public virtual IDictionary<string, object?> AdditionalAttributes { get; set; } = new Dictionary<string, object?>();

        private string? _tag;
        private ElementReference? _reference;

        protected bool ElementReferenceChanged { get; set; }

        protected virtual string? ComputedClass => Class;

        protected virtual string? ComputedStyle => Style;

        public ElementReference Reference
        {
            get => _reference ?? new ElementReference();
            protected set
            {
                if (_reference.HasValue && _reference.Value.Id != value.Id)
                {
                    ElementReferenceChanged = true;
                }

                _reference = value;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenElement(sequence++, (Tag ?? "div"));

            builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
            builder.AddAttribute(sequence++, "class", ComputedClass);
            builder.AddAttribute(sequence++, "style", ComputedStyle);
            builder.AddContent(sequence++, ChildContent);
            builder.AddElementReferenceCapture(sequence, reference =>
            {
                ReferenceCaptureAction?.Invoke(reference);
                Reference = reference;
            });

            builder.CloseElement();
        }
    }