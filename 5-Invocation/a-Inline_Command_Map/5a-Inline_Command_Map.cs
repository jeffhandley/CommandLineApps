using System.CommandLine.Invocation;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

if (args.Length == 0) args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };
// if (args.Length == 0) args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--pr", "40074", "untriaged", "--dry-run" };

var cli = new Cli();
var add = cli.AddCommand("add");
var remove = cli.AddCommand("remove");

var org = cli.AddOption<string>("org", 'o');
var repo = cli.AddOption<string>("repo", 'r');
var issue = cli.AddOption<int?>("issue", 'i');
var pr = cli.AddOption<int?>("pr", 'p');
var labels = cli.AddArguments<string>(minArgs: 1);
var dryrun = cli.AddOption<bool>("dry-run", 'd');

cli.Invoke(args, new() {
    { add, cmd => GitHubHelper.Labels.Add(
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
        cmd.GetOption(dryrun)
    ) },
    { remove, cmd => GitHubHelper.Labels.Remove(
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
        cmd.GetOption(dryrun)
    ) }
});
