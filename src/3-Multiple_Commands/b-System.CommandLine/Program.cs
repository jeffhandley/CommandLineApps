using System.CommandLine;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

var add = new CliCommand("add");
var addLabels = new CliArgument<IEnumerable<string>>("labels") { Arity = ArgumentArity.OneOrMore };
add.Add(addLabels);

var remove = new CliCommand("remove");
var removeLabels = new CliArgument<IEnumerable<string>>("labels") { Arity = ArgumentArity.OneOrMore };
remove.Add(removeLabels);

var org = new CliOption<string>("--org", new[] { "-o" }) { Required = true, Recursive = true };
var repo = new CliOption<string>("--repo", new[] { "-r" }) { Required = true, Recursive = true };
var issue = new CliOption<int?>("--issue", new[] { "-i" }) { Recursive = true };
var pr = new CliOption<int?>("--pr", new[] { "-p" }) { Recursive = true };
var dryrun = new CliOption<bool>("--dry-run", new[] { "-d" }) { Recursive = true };

var cli = new CliRootCommand { add, remove, org, repo, issue, pr, dryrun };
cli.Description = "Apply labels to a GitHub issue or PR";

cli.Validators.Add(result =>
{
    if (result.GetValue(issue) is null && result.GetValue(pr) is null)
    {
        result.AddError("Either --issue or --pr must be specified");
    }
});

add.SetAction(cmd => GitHubHelper.Labels.Add(
    cmd.GetValue(org)!,
    cmd.GetValue(repo)!,
    cmd.GetValue(issue),
    cmd.GetValue(pr),
    cmd.GetValue(addLabels)!,
    cmd.GetValue(dryrun)
));

remove.SetAction(cmd => GitHubHelper.Labels.Remove(
    cmd.GetValue(org)!,
    cmd.GetValue(repo)!,
    cmd.GetValue(issue),
    cmd.GetValue(pr),
    cmd.GetValue(addLabels)!,
    cmd.GetValue(dryrun)
));

var cmd = cli.Parse(args);
return cmd.Invoke();
