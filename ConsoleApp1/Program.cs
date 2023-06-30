using System;
using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
var cli = CliParser.Parse(args);
string org = cli.GetOption<string>("org");
string repo = cli.GetOption<string>("repo");
int? issue = cli.GetOption<int?>("issue");
int? pr = cli.GetOption<int?>("pr");
IEnumerable<string> labels = cli.GetArguments<string>();
bool isDryRun = cli.HasOption("dry-run");

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
