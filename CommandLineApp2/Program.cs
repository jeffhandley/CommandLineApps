using System;
using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
var command = new Cli();
var orgOption = command.AddOption<string>("org", 'o');
var repoOption = command.AddOption<string>("repo", 'r');
var issueOrPr = command.AddGroup(CliGroupType.MutuallyExclusive);
var issueOption = issueOrPr.AddOption<int?>("issue", 'i');
var prOption = issueOrPr.AddOption<int?>("pr", 'p');
var labelArgs = command.AddArgument<string>(1);
var dryrunOption = command.AddOption<bool>("dry-run", 'd');
var help = command.AddHelp();
var completion = command.AddCompletion();

var cli = CliParser.Parse(args, command);

if (help.ShowIfNeeded(cli)) return;
if (completion.ShowIfNeeded(cli)) return;

var org = cli.GetOption(orgOption);
var repo = cli.GetOption(repoOption);
var issue = cli.GetOption(issueOption);
var pr = cli.GetOption(prOption);
var labels = cli.GetArgument(labelArgs);
var dryrun = cli.HasOption(dryrunOption);

if (issue is not null)
{
    // Add the labels to the specified issues
}
else if (pr is not null)
{
    // Add the labels to the specified PR
}
