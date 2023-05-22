using System.CommandLine;
using System.CommandLine.Validation;

namespace Stage2_ProgramMain;

internal class Program
{
    static void Main(string[] args)
    {
        // dotnet run -- -r machinelearning -o ../machinelearning/.github areapods.json
        // dotnet run -- --org jeffhandley --repo CommandLineApps --output ../../jeffhandley/CommandLineApps/.github areapods.json

        var cli = new Cli("Generate repo automation scripts");

        var org = cli.AddOption(new CliOption<string>("--org", new[] { "-o", "--owner" }, "The org/owner of the repository.") { DefaultValue = "dotnet" });
        var repo = cli.AddOption(new CliOption<string>("--repo", "-r", "The name of the repository to generate automation scripts for"));
        var target = cli.AddOption(new CliOption<DirectoryInfo>("--output", "-o", "The output directory for the generated automation scripts"));
        var config = cli.AddArgument(new CliArgument<FileInfo>("The path to the area pods configuration file") { Validators = {
            new ExistingFilesOnly(),
            new CliSymbolValidator<FileInfo>(f => f.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase), "The config file must have a .json extension")
        }});

        cli.ProvideHelp("--help", "-h", "-?");
        cli.ProvideCompletion("[complete]");
        cli.ProvideErrorHandling(exitCode: 100);

        cli.Parse(args);

        GenerateRepoAutomation(org.Value, repo.Value, config.Value, target.Value);
    }

    static void GenerateRepoAutomation(string org, string repo, FileInfo areaPodConfig, DirectoryInfo targetDirectory)
    {

    }
}
