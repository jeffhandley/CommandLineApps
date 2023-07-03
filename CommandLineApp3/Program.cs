using System.CommandLine;

static int AddLabels(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
{
    if (issue is not null) { /* Add the labels to the specified issues */ }
    else if (pr is not null) { /* Add the labels to the specified PR */ }

    return 0;
}

static int RemoveLabels(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
{
    if (issue is not null) { /* Remove the labels from the specified issues */ }
    else if (pr is not null) { /* Remove the labels from the specified PR */ }

    return 0;
}

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

var cli = new Cli();
cli.AddCommand("add");
cli.AddCommand("remove");

var cmd = CliParser.Parse(args, cli);
var org = cmd.GetOption<string>("org", 'o');
var repo = cmd.GetOption<string>("repo", 'r');
var issue = cmd.GetOption<int?>("issue", 'i');
var pr = cmd.GetOption<int?>("pr", 'p');
var labels = cmd.GetArgument("labels");
var dryrun = cmd.HasOption("dry-run", 'd');

if (CliCompletion.ShowIfNeeded(cmd, out int e1)) return e1;
if (CliHelp.ShowIfNeeded(cmd, out int e2)) return e2;

if (cmd.HasCommand("add"))
{
    return AddLabels(org, repo, issue, pr, labels, dryrun);
}
else if (cmd.HasCommand("remove"))
{
    return RemoveLabels(org, repo, issue, pr, labels, dryrun);
}

return 1;
