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
var add = cli.AddCommand("add");
var remove = cli.AddCommand("remove");
var org = cli.AddOption<string>("org", 'o');
var repo = cli.AddOption<string>("repo", 'r');
var issueOrPr = cli.AddGroup(CliGroupType.MutuallyExclusive);
var issue = issueOrPr.AddOption<int?>("issue", 'i');
var pr = issueOrPr.AddOption<int?>("pr", 'p');
var labels = cli.AddArgument<string>("labels", minArgs: 1);
var dryrun = cli.AddOption<bool>("dry-run", 'd');

cli.AddHelp();
cli.AddCompletion();

var cmd = cli.Parse(args);

if (CliCompletion.ShowIfNeeded(cmd, out int e1)) return e1;
if (CliHelp.ShowIfNeeded(cmd, out int e2)) return e2;

if (cmd.HasCommand(add))
{
    return AddLabels(cmd.GetOption(org), cmd.GetOption(repo), cmd.GetOption(issue), cmd.GetOption(pr), cmd.GetArgument(labels), cmd.GetOption(dryrun));
}
else if (cmd.HasCommand(remove))
{
    return RemoveLabels(cmd.GetOption(org), cmd.GetOption(repo), cmd.GetOption(issue), cmd.GetOption(pr), cmd.GetArgument(labels), cmd.GetOption(dryrun));
}

return 1;
