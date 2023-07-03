using System;
using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
var cli = CliParser.Parse(args);
var org = cli.GetOption<string>("org", 'o');
var repo = cli.GetOption<string>("repo", 'r');
var issue = cli.GetOption<int?>("issue", 'i');
var pr = cli.GetOption<int?>("pr", 'p');
var labels = cli.GetArgument<string>();
var dryrun = cli.HasOption("dry-run", 'd');

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
