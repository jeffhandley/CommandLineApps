// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var cli = new Cli();
var org = cli.AddOption<string>("org", required: true);
var repo = cli.AddOption<string>("repo", required: true);
var issue = cli.AddOption<int?>("issue");
var pr = cli.AddOption<int?>("pr");
var labels = cli.AddArgument<string[]>();
var dryrun = cli.AddOption<bool>("dry-run");

var cmd = cli.Parse(args);

if ((int?)cmd[issue] is int issueNum)
{
    GitHubHelper.Labels.AddToIssue(cmd[org], cmd[repo], issueNum, (string[])cmd[labels], cmd[dryrun]);
}
else if ((int?)cmd[pr] is int prNum)
{
    GitHubHelper.Labels.AddToPullRequest(cmd[org], cmd[repo], prNum, (string[])cmd[labels], cmd[dryrun]);
}
