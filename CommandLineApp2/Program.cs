using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
var cmd = CliParser.Parse(args);
var org = cmd.GetOption<string>("org", 'o');
var repo = cmd.GetOption<string>("repo", 'r');
var issue = cmd.GetOption<int?>("issue", 'i');
var pr = cmd.GetOption<int?>("pr", 'p');
var labels = cmd.GetArgument("labels");
var dryrun = cmd.HasOption("dry-run", 'd');

if (CliCompletion.ShowIfNeeded(cmd)) return;
if (CliHelp.ShowIfNeeded(cmd)) return;

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
