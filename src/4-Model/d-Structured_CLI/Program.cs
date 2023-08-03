using System.CommandLine;
using System.CommandLine.ModelBinding;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

// if (args.Length == 0) args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };
if (args.Length == 0) args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--pr", "40074", "untriaged", "--dry-run" };

var cli = new ModelBoundCli<CliCommands>();
cli.AddCompletion();
cli.AddHelp();

var cmd = cli.Parse(args);

if (CliCompletion.ShowIfNeeded(cmd, out int exitCode)) return exitCode;
if (CliHelp.ShowIfNeeded(cmd, out exitCode)) return exitCode;

if (cmd.Commands.Add is CommandOptions add)
{
    return GitHubHelper.Labels.Add(
        add.Org,
        add.Repo,
        add.Issue,
        add.PR,
        add.Labels,
        add.DryRun
    );
}
else if (cmd.Commands.Remove is CommandOptions remove)
{
    return GitHubHelper.Labels.Remove(
        remove.Org,
        remove.Repo,
        remove.Issue,
        remove.PR,
        remove.Labels,
        remove.DryRun
    );
}

return 1;

record struct CommandOptions(string Org, string Repo, int? Issue, int? PR, [Argument(minArgs: 1)] IEnumerable<string> Labels, bool DryRun);

record struct CliCommands(CommandOptions? Add, CommandOptions? Remove)
{
    // Apply defaults for illustration
    public CliCommands() : this(null, new CommandOptions("dotnet", "runtime", 40074, null, new[] { "untriaged" }, true)) { }
}
