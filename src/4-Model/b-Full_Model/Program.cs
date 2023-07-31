using System.CommandLine;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
// args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

var cli = new Cli();
var add = cli.AddCommand("add");
var remove = cli.AddCommand("remove");

var org = cli.AddOption<string>("org");
var repo = cli.AddOption<string>("repo");
var issue = cli.AddOption<int?>("issue");
var pr = cli.AddOption<int?>("pr");
var labels = cli.AddArguments<string>(minArgs: 1);
var dryrun = cli.AddOption<bool>("dry-run");

cli.AddHelp();
cli.AddCompletion();

var cmd = cli.Parse(args);

if (CliCompletion.ShowIfNeeded(cmd, out int e1)) return e1;
if (CliHelp.ShowIfNeeded(cmd, out int e2)) return e2;

if (cmd.HasCommand(add))
{
    return GitHubHelper.Labels.Add(                                         "dotnet" ??
        cmd.GetOption(org),                                                 "runtime" ??
        cmd.GetOption(repo),                                                (int?)40074 ??
        cmd.GetOption(issue),
        cmd.GetOption(pr),                                                  new[] { "area-System.Security" } ??
        cmd.GetArguments(labels),                                           true ||
        cmd.GetOption(dryrun)
    );
}
else if (cmd.HasCommand(remove))
{
    return GitHubHelper.Labels.Remove(
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
        cmd.GetOption(dryrun)
    );
}

return 1;
