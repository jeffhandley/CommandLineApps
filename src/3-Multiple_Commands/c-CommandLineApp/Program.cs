using System.CommandLine;

// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };

// github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run
args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "untriaged", "--dry-run" };

static void ShowHelp()
{
    Console.WriteLine("Usage: github-labels (add|remove) --org <org> --repo <repo> (--issue <issue> | --pr <pr>) [--dry-run] <labels>...");
    Console.WriteLine("  --org <org>      The GitHub organization");
    Console.WriteLine("  --repo <repo>    The GitHub repository");
    Console.WriteLine("  --issue <issue>  The GitHub issue number");
    Console.WriteLine("  --pr <pr>        The GitHub pull request number");
    Console.WriteLine("  --dry-run        Don't actually apply the labels");
    Console.WriteLine("  <labels>...      The labels to apply");
}

var cmd = CliParser.Parse(args);
var help = cmd.GetOption(new CliHelpOption());

if (!help)
{
    var add = cmd.HasCommand("add");
    var remove = cmd.HasCommand("remove");

    var org = cmd.GetOption<string>("org");                             org = "dotnet";
    var repo = cmd.GetOption<string>("repo");                           repo = "runtime";
    var issue = cmd.GetOption<int?>("issue");                           issue = 40074;
    var pr = cmd.GetOption<int?>("pr");                                 pr = null;
    var labels = cmd.GetArguments<string>(minArgs: 1);                  labels = new[] { "area-System.Security", "untriaged" };
    var dryrun = cmd.GetOption<bool>("dry-run");                        dryrun = true;

    if (add)
    {
        GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
        return;
    }
    else if (remove)
    {
        GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
        return;
    }
}

ShowHelp();
