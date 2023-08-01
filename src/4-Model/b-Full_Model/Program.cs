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

if (CliCompletion.ShowIfNeeded(cmd, out int exitCode)) return exitCode;
if (CliHelp.ShowIfNeeded(cmd, out exitCode)) return exitCode;

if (cmd.HasCommand(add))
{
    return GitHubHelper.Labels.Add(
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
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
