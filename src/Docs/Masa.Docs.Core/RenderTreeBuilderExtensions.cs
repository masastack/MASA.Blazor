namespace Masa.Docs.Core;

public static class RenderTreeBuilderExtensions
{
    public static void AddChildContent(this RenderTreeBuilder builder, int sequence, string content)
    {
        builder.AddAttribute(sequence, "ChildContent", new RenderFragment(childBuilder => childBuilder.AddContent(0, content)));
    }

    public static void AddComponent<TComponent>(this RenderTreeBuilder builder, int sequence = 0) where TComponent : notnull, IComponent
    {
        builder.OpenComponent<TComponent>(sequence);
        builder.CloseComponent();
    }
}
