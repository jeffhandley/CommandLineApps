using System;
using System.CommandLine;

namespace Source_Generated_Main
{
    partial class Program
    {
        static int Main(string[] args)
        {
            args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };
            // args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

            Cli cli = new();
            CliCommand add = cli.AddCommand(BuildCliCommand_Add(out Action<CliParseResult> addAction));
            CliCommand remove = cli.AddCommand(BuildCliCommand_Remove(out Action<CliParseResult> removeAction));

            CliCompletionDirective completion = cli.AddCompletion();
            CliHelpOption help = cli.AddHelp();

            CliParseResult result = cli.Parse(args);

            if (completion.ShowIfNeeded(result, out int exitCode)) return exitCode;
            if (help.ShowIfNeeded(result, out exitCode)) return exitCode;

            if (result.HasCommand(add))
            {
                addAction(result);
                return 0;
            }
            
            if (result.HasCommand(remove))
            {
                removeAction(result);
                return 0;
            }

            return 1;
        }

        static CliCommand BuildCliCommand_Add(out Action<CliParseResult> action)
        {
            CliCommand add = new CliCommand("add") { Description = "Add labels to an issue or pull request" };
            CliOption<string> org = add.AddOption<string>("org");
            CliOption<string> repo = add.AddOption<string>("repo");
            CliOption<int?> issue = add.AddOption<int?>("issue");
            CliOption<int?> pr = add.AddOption<int?>("pr");
            CliArgument<string> labels = add.AddArguments<string>(minArgs: 1);
            CliOption<bool> dryRun = add.AddOption<bool>("dry-run");

            action = (CliParseResult result) => Add(
                result.GetOption(org),
                result.GetOption(repo),
                result.GetOption(issue),
                result.GetOption(pr),
                result.GetOption(dryRun),
                result.GetArguments(labels).ToArray());

            return add;
        }

        static CliCommand BuildCliCommand_Remove(out Action<CliParseResult> action)
        {
            CliCommand add = new CliCommand("Remove") { Description = "Remove labels from an issue or pull request" };
            CliOption<string> org = add.AddOption<string>("org");
            CliOption<string> repo = add.AddOption<string>("repo");
            CliOption<int?> issue = add.AddOption<int?>("issue");
            CliOption<int?> pr = add.AddOption<int?>("pr");
            CliArgument<string> labels = add.AddArguments<string>(minArgs: 1);
            CliOption<bool> dryRun = add.AddOption<bool>("dry-run");

            action = (CliParseResult result) => Remove(
                result.GetOption(org),
                result.GetOption(repo),
                result.GetOption(issue),
                result.GetOption(pr),
                result.GetOption(dryRun),
                result.GetArguments(labels).ToArray());

            return add;
        }
    }
}
