using Masa.Blazor.Doc.CLI.Commands;
using Masa.Blazor.Doc.CLI.Interfaces;
using Masa.Blazor.Doc.CLI.Wrappers;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Linq;

namespace Masa.Blazor.Doc.CLI
{
    public class CliWorker
    {
        public int Execute(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = ConfigWrapper.Config.Name,
                FullName = ConfigWrapper.Config.FullName,
                Description = ConfigWrapper.Config.Descrption,
            };

            app.HelpOption();
            app.VersionOptionFromAssemblyAttributes(typeof(Program).Assembly);

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });

            new List<IAppCommand>()
            {
                new GenerateDemoJsonCommand(),
                new GenerateDocsToHtmlCommand(),
                new GenerateIconsToJsonCommand(),
                new GenerateMenuJsonCommand(),
                new GenerateApiJsonCommand(),
            }
            .ToList()
            .ForEach(cmd =>
            {
                app.Command(cmd.Name, cmd.Execute);
            });

            return app.Execute(args);
        }
    }
}
