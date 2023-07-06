using System.CommandLine;

static void AddLabels(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
{
    if (issue is not null) { /* Add the labels to the specified issues */ }
    else if (pr is not null) { /* Add the labels to the specified PR */ }
}

static void RemoveLabels(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
{
    if (issue is not null) { /* Remove the labels from the specified issues */ }
    else if (pr is not null) { /* Remove the labels from the specified PR */ }
}

// github-labels add --org dotnet --repo runtime --issue 40074 --labels area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 --labels untriaged --dry-run

var cli = new Cli();
cli.AddCommand("add");
cli.AddCommand("remove");

var cmd = CliParser.ParseOptions(args, cli);
var org = cmd.GetOption<string>("org", 'o');
var repo = cmd.GetOption<string>("repo", 'r');
var issue = cmd.GetOption<int?>("issue", 'i');
var pr = cmd.GetOption<int?>("pr", 'p');
var labels = cmd.GetOption<IEnumerable<string>>("labels", 'l');
var dryrun = cmd.HasOption("dry-run", 'd');

if (CliCompletion.ShowIfNeeded(cmd)) return;
if (CliHelp.ShowIfNeeded(cmd)) return;

if (cmd.HasCommand("add"))
{
    AddLabels(org, repo, issue, pr, labels, dryrun);
}
else if (cmd.HasCommand("remove"))
{
    RemoveLabels(org, repo, issue, pr, labels, dryrun);
}
