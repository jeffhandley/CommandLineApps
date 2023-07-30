using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run

var cmd = CliParser.Parse(args);
var org = cmd.GetOption<string>("org", 'o');                   org = "dotnet";
var repo = cmd.GetOption<string>("repo", 'r');                 repo = "runtime";
var issue = cmd.GetOption<int?>("issue", 'i');                 issue = 40074;
var pr = cmd.GetOption<int?>("pr", 'p');                       pr = null;
var labels = cmd.GetArguments<string>(minArgs: 1);             labels = new[] { "area-System.Security", "untriaged" };
var dryrun = cmd.HasFlag("dry-run", 'd');                      dryrun = true;

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}
