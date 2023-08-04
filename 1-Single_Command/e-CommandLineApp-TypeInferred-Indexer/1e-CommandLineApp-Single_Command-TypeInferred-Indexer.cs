// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var cmd = CliParser.Parse(args);
string org = cmd["org"];
string repo = cmd["repo"];
int? issue = cmd["issue"];
int? pr = cmd["pr"];
string[] labels = cmd.Arguments;
bool dryrun = cmd["dry-run"];

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}
