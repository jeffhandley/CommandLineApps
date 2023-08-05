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

cli.SetAction((ParseResult cmd) =>
{
    if ((int?)cmd[issue] is not null)
    {
        GitHubHelper.Labels.AddToIssue(cmd[org], cmd[repo], cmd[issue], (string[])cmd[labels], cmd[dryrun]);
    }
    else if ((int?)cmd[pr] is not null)
    {
        GitHubHelper.Labels.AddToPullRequest(cmd[org], cmd[repo], cmd[issue], (string[])cmd[labels], cmd[dryrun]);
    }
});

var cmd = cli.Parse(args);
return cmd.Invoke();
