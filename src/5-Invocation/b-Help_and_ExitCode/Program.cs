using System.CommandLine;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

var cli = new Cli();
var add = cli.Add(new CliCommand("add") { Description = "Add labels to an issue or pull request" });
var remove = cli.Add(new CliCommand("remove") { Description = "Remove labels from an issue or pull request" });

var org = cli.Add(new CliOption<string>("org", new[] { "o", "owner" }) { Description = "The GitHub organization/owner" });
var repo = cli.Add(new CliOption<string>("repo", 'r') { Description = "The GitHub Repository" });
var labels = cli.Add(new CliArgument<string>(minArgs: 1) { Description = "The labels to add or remove" });
var dryrun = cli.Add(new CliOption<bool>("dry-run", new[] { "d", "dryrun", "whatif", "what-if" }) { Description = "Perform a dry-run without adding or removing labels" });

var issueOrPr = cli.AddGroup(CliGroupType.MutuallyExclusive);
var issue = issueOrPr.Add(new CliOption<int?>("issue", 'i') { Description = "The GitHub issue number" });
var pr = issueOrPr.Add(new CliOption<int?>("pr", new[] { "p", "pull", "pull-request" }) { Description = "The GitHub Pull Request number" });

if (cli.Invoke(args, new() {
    { add, cmd => GitHubHelper.Labels.Add(
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
        cmd.GetOption(dryrun)
    ) },
    { remove, cmd => GitHubHelper.Labels.Remove(
        cmd.GetOption(org)                  ?? "dotnet",
        cmd.GetOption(repo)                 ?? "runtime",
        cmd.GetOption(issue)                ?? 40074,
        cmd.GetOption(pr),
        cmd.GetArguments(labels)            .Append("untriaged"),
        cmd.GetOption(dryrun)
    ) }
}, out int exitCode)) return exitCode;

return 1;
