// apply-labels --org dotnet --repo runtime --issue 40074 --labels area-System.Security untriaged --dry-run

using System.CommandLine;

var cmd = new CliRootCommand("Add or remove labels from a GitHub issue or pull request")
{
    new CliOption<string>("--org", new[] { "-o", "--owner"}) { Required = true },
    new CliOption<string>("--repo", new[] { "-r" }) { Required = true },
    new CliOption<int?>("--issue", new[] { "-i" }),
    new CliOption<int?>("--pr", new[] { "-p" }),
    new CliOption<bool>("--dry-run", new[] { "-d" }),
    new CliArgument<IEnumerable<string>>("labels"),
};

var cli = cmd.Parse("--org dotnet --repo runtime --issue 40074 area-System.Security --dry-run");

if (cli.ShouldTerminate())
{
    return cli.Terminate();
}

var org = cli.GetOption<string>("org");
var repo = cli.GetOption<string>("repo");
var issue = cli.GetOption<int?>("issue");
var pr = cli.GetOption<int?>("pr");
var labels = cli.GetArgument<IEnumerable<string>>("labels");
var dryrun = cli.GetOption<bool>("dry-run");

if (issue is not null || pr is not null)
{
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
