namespace Masa.Blazor
{
    public interface IActivatable
    {
        Dictionary<string, object> ActivatorAttributes { get; }

        bool IsActive { get; }

        RenderFragment? ComputedActivatorContent { get; }
    }
}