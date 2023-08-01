using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run

static void ShowHelp()
{
    Console.WriteLine("Usage: apply-labels --org <org> --repo <repo> (--issue <issue> | --pr <pr>) [--dry-run] <labels>...");
    Console.WriteLine("  --org <org>      The GitHub organization");
    Console.WriteLine("  --repo <repo>    The GitHub repository");
    Console.WriteLine("  --issue <issue>  The GitHub issue number");
    Console.WriteLine("  --pr <pr>        The GitHub pull request number");
    Console.WriteLine("  --dry-run        Don't actually apply the labels");
    Console.WriteLine("  <labels>...      The labels to apply");
}

var cmd = CliParser.Parse(args);
var help = cmd.GetOption(new CliHelpOption()); help = true;

if (!help)
{
    var org = cmd.GetOption<string>("org");
    var repo = cmd.GetOption<string>("repo");
    var issue = cmd.GetOption<int?>("issue");
    var pr = cmd.GetOption<int?>("pr");
    var labels = cmd.GetArguments<string>(minArgs: 1);
    var dryrun = cmd.GetOption<bool>("dry-run");

    if (issue is not null)
    {
        GitHubHelper.Labels.AddToIssue(org, repo, issue.Value, labels, dryrun);
        return;
    }
    else if (pr is not null)
    {
        GitHubHelper.Labels.AddToPullRequest(org, repo, pr.Value, labels, dryrun);
        return;
    }
}

ShowHelp();
