using Masa.Blazor.JSModules.LongPress;

namespace Masa.Blazor.JSModules;

public interface ILongPressJSModule
{
    ValueTask<LongPressJSObject?> RegisterAsync(string selector, Func<Task> handle, int delay = 500);
}