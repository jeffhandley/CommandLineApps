using System;
using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
var cli = CliParser.Parse(args);
var org = cli.GetOption<string>("org");
var repo = cli.GetOption<string>("repo");
var issue = cli.GetOption<int?>("issue");
var pr = cli.GetOption<int?>("pr");
var labels = cli.GetArguments<string>();
var isDryRun = cli.HasOption("dry-run");

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
