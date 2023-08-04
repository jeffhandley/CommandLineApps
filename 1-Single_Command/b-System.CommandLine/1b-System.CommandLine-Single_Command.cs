// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var org = new CliOption<string>("--org", new[] { "-o" }) { Required = true };
var repo = new CliOption<string>("--repo", new[] { "-r" }) { Required = true };
var issue = new CliOption<int?>("--issue", new[] { "-i" });
var pr = new CliOption<int?>("--pr", new[] { "-p" });
var labels = new CliArgument<IEnumerable<string>>("labels") { Arity = ArgumentArity.OneOrMore };
var dryrun = new CliOption<bool>("--dry-run", new[] { "-d" });

var cli = new CliRootCommand { org, repo, issue, pr, labels, dryrun };
cli.Validators.Add(result =>
{
    if (result.GetValue(issue) is null && result.GetValue(pr) is null)
    {
        result.AddError("Either --issue or --pr must be specified");
    }
});

cli.SetAction(cmd =>
{
    if (cmd.GetValue(issue) is not null)
    {
        GitHubHelper.Labels.AddToIssue(
            cmd.GetValue(org)!,
            cmd.GetValue(repo)!,
            cmd.GetValue(issue)!.Value,
            cmd.GetValue(labels)!,
            cmd.GetValue(dryrun)
        );
    }
    else if (cmd.GetValue(pr) is not null)
    {
        GitHubHelper.Labels.AddToPullRequest(
            cmd.GetValue(org)!,
            cmd.GetValue(repo)!,
            cmd.GetValue(pr)!.Value,
            cmd.GetValue(labels)!,
            cmd.GetValue(dryrun)
        );
    }
});

var cmd = cli.Parse(args);
return cmd.Invoke();
