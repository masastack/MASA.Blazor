using Microsoft.Extensions.CommandLineUtils;

namespace Masa.Blazor.Doc.CLI.Interfaces
{
    public interface IAppCommand
    {
        string Name { get; }

        void Execute(CommandLineApplication command);
    }
}
