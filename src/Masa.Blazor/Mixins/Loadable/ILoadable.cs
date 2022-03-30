namespace Masa.Blazor
{
    public interface ILoadable : BlazorComponent.ILoadable, IColorable
    {
        StringNumber LoaderHeight { get; }
    }
}
