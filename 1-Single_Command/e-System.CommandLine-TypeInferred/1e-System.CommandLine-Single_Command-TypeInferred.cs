// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var org = new CliOption<string>("--org", "-o") { Required = true };
var repo = new CliOption<string>("--repo", "-r") { Required = true };
var issue = new CliOption<int?>("--issue", "-i");
var pr = new CliOption<int?>("--pr", "-p");
var labels = new CliArgument<string[]>("labels") { Arity = ArgumentArity.OneOrMore };
var dryrun = new CliOption<bool>("--dry-run", "-d");

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
    if ((int?)issue is not null)
    {
        GitHubHelper.Labels.AddToIssue(org, repo, issue, labels, dryrun);
    }
    else if ((int?)pr is not null)
    {
        GitHubHelper.Labels.AddToPullRequest(org, repo, issue, labels, dryrun);
    }
});

var cmd = cli.Parse(args);
return cmd.Invoke();
