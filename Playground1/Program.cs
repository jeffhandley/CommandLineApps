namespace Stage1_ProgramMain;

using System.CommandLine;
using System.CommandLine.Validation;

internal class Program
{
    static void Main(string[] args)
    {
        // dotnet run -- -r machinelearning -o ../machinelearning/.github areapods.json
        // dotnet run -- --org jeffhandley --repo CommandLineApps --output ../../jeffhandley/CommandLineApps/.github areapods.json

        var cli = new Cli
        {
            new CliOption<string>("--org", new[] { "-o", "--owner" }, "The org/owner of the repository.") { DefaultValue = "dotnet" },
            new CliOption<string>("--repo", new[] { "-r" }, "The name of the repository to generate automation scripts for"),
            new CliOption<DirectoryInfo>("--output", new[] { "-o" }, "The output directory for the generated automation scripts") { Key = "target" },
            new CliArgument<FileInfo>("The path to the area pods configuration file")
            {
                Key = "config",
                Validators = {
                    new ExistingFilesOnly(),
                    new CliSymbolValidator<FileInfo>(f => f.Extension.Equals(".json", StringComparison.OrdinalIgnoreCase), "The config file must have a .json extension")
                }
            }
        };

        var cliArgs = cli.Parse(args);

        if (cliArgs.Errors.Any())
        {
            foreach (var error in cliArgs.Errors)
            {
                Console.WriteLine(error.Message);
            }

            Environment.Exit(100);
        }

        GenerateRepoAutomation(cliArgs.GetValue<string>("org"), cliArgs.GetValue<string>("repo"), cliArgs.GetValue<FileInfo>("config"), cliArgs.GetValue<DirectoryInfo>("target"));
    }

    static void GenerateRepoAutomation(string org, string repo, FileInfo areaPodConfig, DirectoryInfo targetDirectory)
    {

    }
}