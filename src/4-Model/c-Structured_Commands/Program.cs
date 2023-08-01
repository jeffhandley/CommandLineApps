﻿using System.CommandLine;
using System.CommandLine.ModelBinding;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
// args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

var cli = new Cli();
var add = cli.AddCommand<CommandOptions>("add");
var remove = cli.AddCommand<CommandOptions>("remove");

cli.AddHelp();
cli.AddCompletion();

var cmd = cli.Parse(args);

if (CliCompletion.ShowIfNeeded(cmd, out int exitCode)) return exitCode;
if (CliHelp.ShowIfNeeded(cmd, out exitCode)) return exitCode;

if (cmd.TryGetCommand(add, out var options))
{
    return GitHubHelper.Labels.Add(                                             "dotnet" ??
        options.Org,                                                            "runtime" ??
        options.Repo,                                                           (int?)40074 ??
        options.Issue,
        options.PR,                                                             new[] { "area-System.Security" } ??
        options.Labels,                                                         true ||
        options.DryRun
    );
}
else if (cmd.TryGetCommand(remove, out options))
{
    return GitHubHelper.Labels.Remove(
        options.Org,
        options.Repo,
        options.Issue,
        options.PR,
        options.Labels,
        options.DryRun
    );
}

return 1;

record struct CommandOptions(string Org, string Repo, int? Issue, int? PR, [Argument(minArgs: 1)] IEnumerable<string> Labels, bool DryRun);
