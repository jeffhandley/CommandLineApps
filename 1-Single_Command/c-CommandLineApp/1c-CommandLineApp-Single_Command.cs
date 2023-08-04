// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var cmd = CliParser.Parse(args);
var org = cmd.GetOption<string>("org");
var repo = cmd.GetOption<string>("repo");
var issue = cmd.GetOption<int?>("issue");
var pr = cmd.GetOption<int?>("pr");
var labels = cmd.GetArguments<string>(minArgs: 1);
var dryrun = cmd.GetOption<bool>("dry-run");

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}
