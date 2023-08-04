// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var cmd = CliParser.Parse(args);
string org = cmd.GetOption("org");
string repo = cmd.GetOption("repo");
int? issue = cmd.GetOption("issue");
int? pr = cmd.GetOption("pr");
string[] labels = cmd.GetArguments();
bool dryrun = cmd.GetOption("dry-run");

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}
