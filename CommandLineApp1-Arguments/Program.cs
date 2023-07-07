using System.CommandLine;

// apply-labels dotnet runtime issue 40074 area-System.Security untriaged --dry-run
var cmd = CliParser.ParseArguments(args);
var org = cmd.GetArgument<string>();
var repo = cmd.GetArgument<string>();
var type = cmd.GetArgument<IssueOrPr>();
var number = cmd.GetArgument<int>();
var labels = cmd.GetArguments<string>(minArgs: 1);
var dryrun = false;

if (labels.Contains("--dry-run", StringComparer.OrdinalIgnoreCase))
{
    dryrun = true;
    labels = labels.Where(l => !string.Equals(l, "--dry-run", StringComparison.OrdinalIgnoreCase));
}

if (type == IssueOrPr.Issue)
{
    // Add the labels to the specified issues
}
else
{
    // Add the labels to the specified PR
}

public enum IssueOrPr
{
    Issue,
    PR
}
