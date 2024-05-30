namespace Masa.Blazor;

public interface IDelayable
{
    int OpenDelay { get; }

    int CloseDelay { get; }
}
