using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor.Doc.CLI.Interfaces
{
    public interface IAppCommand
    {
        string Name { get; }

        void Execute(CommandLineApplication command);
    }
}
