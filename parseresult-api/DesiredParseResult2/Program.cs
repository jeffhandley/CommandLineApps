// github-labels add --org dotnet --repo runtime --issue 40074 --labels area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 --labels untriaged --dry-run

using System.CommandLine;

var cmd = new CliRootCommand("Add or remove labels from a GitHub issue or pull request")
{
    new CliCommand("add", "Add labels to a GitHub issue or pull request") { new CliArgument<IEnumerable<string>>("labels") },
    new CliCommand("remove", "Remove labels from a GitHub issue or pull request") { new CliArgument<IEnumerable<string>>("labels") },
    new CliOption<string>("--org", new[] { "-o", "--owner"}) { Required = true, Recursive = true },
    new CliOption<string>("--repo", new[] { "-r" }) { Required = true, Recursive = true },
    new CliOption<int?>("--issue", new[] { "-i" }) { Recursive = true },
    new CliOption<int?>("--pr", new[] { "-p" }) { Recursive = true },
    new CliOption<bool>("--dry-run", new[] { "-d" }) { Recursive = true }
};

var cli = cmd.Parse("add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run");

if (cli.ShouldTerminate())
{
    return cli.Terminate();
}

var command = cli.CommandResult.Command.Name;
var org = cli.GetOption<string>("org");
var repo = cli.GetOption<string>("repo");
var issue = cli.GetOption<int?>("issue");
var pr = cli.GetOption<int?>("pr");
var labels = cli.GetArgument<IEnumerable<string>>("labels");
var dryrun = cli.GetOption<bool>("dry-run");

if (issue is not null || pr is not null)
{
    Console.WriteLine($"Command : {command}");
    Console.WriteLine(issue is not null ? $"Issue   : {issue}" : $"PR      : {pr}");
    Console.WriteLine($"Org     : {org}");
    Console.WriteLine($"Repo    : {repo}");
    Console.WriteLine($"Labels  : {string.Join(", ", labels)}");
    Console.WriteLine($"Dry Run : {dryrun}");

    return 0;
}
else
{
    return cli.Terminate("Either --issue or --pr must be specified");
}
