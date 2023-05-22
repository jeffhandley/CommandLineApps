using System.CommandLine;
using System.CommandLine.Validation;

namespace Stage2_ProgramMain;

internal class Program
{
    static void Main(string[] args)
    {
        var cli = new Cli("Generate repo automation scripts");
        
        var owner = cli.AddOption<string>("--org", new[] { "-o", "--owner" }, description: "The org/owner of the repository.", defaultValue: "dotnet");
        var repo = cli.AddOption<string>("--repo", "-r", description: "The name of the repository to generate automation scripts for");
        var target = cli.AddOption<DirectoryInfo>("--output", "-o", description: "The output directory for the generated automation scripts");
        var config = cli.AddArgument<FileInfo>("The path to the area pods configuration file", validators: new[] {
            new ExistingFilesOnly(),
            new CliSymbolValidator<FileInfo>(f => f.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase), "The config file must have a .json extension")
        });

        var help = cli.AddHelpOption();
        var completion = cli.AddCompletionDirective();

        var result = cli.Parse(args);

        
    }
}