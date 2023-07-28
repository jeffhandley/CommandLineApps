// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

using System.CommandLine;
using System.Windows.Input;

var cmd = new CliRootCommand("Add or remove labels from a GitHub issue or pull request")
{
    new CliCommand("add") { new CliArgument<IEnumerable<string>>("labels") },
    new CliCommand("remove") { new CliArgument<IEnumerable<string>>("labels") },
    new CliOption<string>("--org", new[] { "-o", "--owner"}) { Required = true, Recursive = true },
    new CliOption<string>("--repo", new[] { "-r" }) { Required = true, Recursive = true },
    new CliOption<int?>("--issue", new[] { "-i" }) { Recursive = true },
    new CliOption<int?>("--pr", new[] { "-p" }) { Recursive = true },
    new CliOption<bool>("--dry-run", new[] { "-d" }) { Recursive = true },
};

//var cli = cmd.Parse(new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" });
var cli = cmd.Parse("add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run");
var exit = cli.Invoke();
if (exit > 0) return exit;

var command = cli.CommandResult.Command.Name;
var labels = cli.GetValue<IEnumerable<string>>("labels");
// var org = cli.GetValue<string>("--org");
// var repo = cli.GetValue<string>("--repo");
// var issue = cli.GetValue<int?>("--issue");
// var pr = cli.GetValue<int?>("--pr");
// var dryrun = cli.GetValue<bool>("--dry-run");

Console.WriteLine($"Command:  {command}");
// Console.WriteLine($"    org: {org}");
// Console.WriteLine($"   repo: {repo}");
// Console.WriteLine($"  issue: {issue}");
// Console.WriteLine($"     pr: {pr}");
// Console.WriteLine($" labels: {labels}");
// Console.WriteLine($" dryrun: {dryrun}");

return 0;
