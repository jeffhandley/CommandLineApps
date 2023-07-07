using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 --labels area-System.Security untriaged --dry-run
var cmd = CliParser.ParseOptions(args);
var org = cmd.GetOption<string>("org", 'o');
var repo = cmd.GetOption<string>("repo", 'r');
var issue = cmd.GetOption<int?>("issue", 'i');
var pr = cmd.GetOption<int?>("pr", 'p');
var labels = cmd.GetOption<IEnumerable<string>>("labels", 'l');
var dryrun = cmd.HasOption("dry-run", 'd');

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
