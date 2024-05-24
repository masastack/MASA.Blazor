namespace Masa.Blazor
{
    public class MElement : ComponentBase
    {
        [Parameter] [MasaApiParameter("div")] public string? Tag { get; set; } = "div";

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string? Class { get; set; }

        [Parameter] public string? Style { get; set; }

        [Parameter] public Action<ElementReference>? ReferenceCaptureAction { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public virtual IDictionary<string, object> AdditionalAttributes { get; set; } =
            new Dictionary<string, object>();

        protected virtual string? ComputedClass => Class;

        protected virtual string? ComputedStyle => Style;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, (Tag ?? "div"));
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", ComputedClass);
            builder.AddAttribute(3, "style", ComputedStyle);
            builder.AddContent(4, ChildContent);

            if (ReferenceCaptureAction is not null)
            {
                builder.AddElementReferenceCapture(5, reference => ReferenceCaptureAction?.Invoke(reference));
            }

            builder.CloseElement();
        }
    }
}