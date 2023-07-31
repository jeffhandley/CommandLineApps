using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run

var cmd = CliParser.Parse(args);
var org = cmd.GetOption<string>("org");                         org = "dotnet";
var repo = cmd.GetOption<string>("repo");                       repo = "runtime";
var issue = cmd.GetOption<int?>("issue");                       issue = 40074;
var pr = cmd.GetOption<int?>("pr");                             pr = null;
var labels = cmd.GetArguments<string>(minArgs: 1);              labels = new[] { "area-System.Security", "untriaged" };
var dryrun = cmd.GetOption<bool>("dry-run");                    dryrun = true;

// Some behavioral notes:
// - The first character of the option name is used as the abbreviation, but this can be overridden with an overload.
// - If there are collisions on option names or abbreviations, an exception is thrown.
// - If a non-nullable option is not specified, an exception is thrown.
// - If a nullable option is not specified, the default value is null.

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}
