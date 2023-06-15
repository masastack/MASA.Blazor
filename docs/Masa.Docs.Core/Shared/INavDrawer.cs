namespace Masa.Docs.Core.Shared;

public interface INavDrawer
{
    bool? Value { get; }

    string ElementId { get; }

    string ElementSelector => $"#{ElementId}";
}
