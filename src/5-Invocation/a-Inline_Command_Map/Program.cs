using System.CommandLine;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
// args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

var cli = new Cli();
var add = cli.AddCommand("add");
var remove = cli.AddCommand("remove");

var org = cli.AddOption<string>("org", 'o');
var repo = cli.AddOption<string>("repo", 'r');
var labels = cli.AddArguments<string>(minArgs: 1);
var dryrun = cli.AddOption<bool>("dry-run", 'd');

var issueOrPr = cli.AddGroup(CliGroupType.MutuallyExclusive);
var issue = issueOrPr.AddOption<int?>("issue", 'i');
var pr = issueOrPr.AddOption<int?>("pr", 'p');

cli.Invoke(args, new() {
    { add, cmd => GitHubHelper.Labels.Add(
        cmd.GetOption(org)                  ?? "dotnet",
        cmd.GetOption(repo)                 ?? "runtime",
        cmd.GetOption(issue)                ?? 40074,
        cmd.GetOption(pr),
        cmd.GetArguments(labels)            .Append("area-System.Security"),
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
