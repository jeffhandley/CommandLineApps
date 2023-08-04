using System.CommandLine.Invocation;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

if (args.Length == 0) args = new[] { "-?" };

var cli = new Cli();
var add = cli.AddCommand(new CliCommand("add") { Description = "Add labels to an issue or pull request" });
var remove = cli.AddCommand(new CliCommand("remove") { Description = "Remove labels from an issue or pull request" });

var org = cli.AddOption(new CliOption<string>("org", 'o', new[] { "owner" }) { Description = "The GitHub organization/owner" });
var repo = cli.AddOption(new CliOption<string>("repo", 'r') { Description = "The GitHub Repository" });
var issue = cli.AddOption(new CliOption<int?>("issue", 'i') { Description = "The GitHub issue number" });
var pr = cli.AddOption(new CliOption<int?>("pr", 'p', new[] { "pull", "pull-request" }) { Description = "The GitHub Pull Request number" });
var labels = cli.AddArgument(new CliArgument<string>(minArgs: 1) { Description = "The labels to add or remove" });
var dryrun = cli.AddOption(new CliOption<bool>("dry-run", 'd', new[] { "dryrun", "whatif", "what-if" }) { Description = "Perform a dry-run without adding or removing labels" });

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
        cmd.GetOption(org),
        cmd.GetOption(repo),
        cmd.GetOption(issue),
        cmd.GetOption(pr),
        cmd.GetArguments(labels),
        cmd.GetOption(dryrun)
    ) }
}, out int exitCode)) return exitCode;

return 1;
