// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run

using Octokit;

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

static void ShowArgumentError(string arg)
{
    throw new ArgumentException($"Unknown argument {arg}");
}

static int? ParseInt(string arg, string argName)
{
    if (int.TryParse(arg, out int val))
    {
        return val;
    }

    ShowArgumentError(argName);
    return null;
}

string org = string.Empty;
string repo = string.Empty;
int? issue = null;
int? pr = null;
List<string> labels = new();
bool dryRun = false;

for (int i = 0; i < args.Length; i++)
{
    if (args[i].StartsWith("-") && !args[i].StartsWith("--"))
    {
        foreach (var c in args[i].Substring(1))
        {
            switch (c)
            {
                case 'o':
                    org = args[++i];
                    break;
                case 'r':
                    repo = args[++i];
                    break;
                case 'i':
                    issue = ParseInt(c.ToString(), "i");
                    break;
                case 'p':
                    pr = ParseInt(c.ToString(), "p");
                    break;
                case 'd':
                    dryRun = true;
                    break;
                case 'h':
                    ShowHelp();
                    return;
                default:
                    ShowArgumentError(c.ToString());
                    return;
            }
        }
    }
    else if (args[i].StartsWith("--"))
    {
        switch (args[i])
        {
            case "--org":
                org = args[++i];
                break;
            case "--repo":
                repo = args[++i];
                break;
            case "--issue":
                issue = ParseInt(args[++i], "issue");
                break;
            case "--pr":
                pr = ParseInt(args[++i], "pr");
                break;
            case "--dry-run":
                dryRun = true;
                break;
            case "--help":
                ShowHelp();
                return;
            default:
                ShowArgumentError(args[i]);
                return;
        }
    }
    else
    {
        labels.Add(args[i]);
    }
}

if (string.IsNullOrEmpty(org))
{
    throw new ArgumentException("Missing --org argument");
}

if (string.IsNullOrEmpty(repo))
{
    throw new ArgumentException("Missing --repo argument");
}

if (!issue.HasValue && !pr.HasValue)
{
    throw new ArgumentException("Missing --issue or --pr argument");
}

if (labels.Count == 0)
{
    throw new ArgumentException("Missing labels");
}

if (issue.HasValue)
{
    if (!dryRun)
    {
        var client = new GitHubClient(new ProductHeaderValue("apply-labels"));
        var issueLabels = await client.Issue.Labels.AddToIssue(org, repo, issue.Value, labels.ToArray());
        Console.WriteLine($"Added {string.Join(", ", issueLabels.Select(l => l.Name))} to issue #{issue.Value}");
    }
    else
    {
        Console.WriteLine($"Would have added {string.Join(", ", labels)} to issue #{issue.Value}");
    }
}
else if (pr.HasValue)
{
    if (!dryRun)
    {
        var client = new GitHubClient(new ProductHeaderValue("apply-labels"));
        var prLabels = await client.Issue.Labels.AddToIssue(org, repo, pr.Value, labels.ToArray());
        Console.WriteLine($"Added {string.Join(", ", prLabels.Select(l => l.Name))} to pull request #{pr.Value}");
    }
    else
    {
        Console.WriteLine($"Would have added {string.Join(", ", labels)} to pull request #{pr.Value}");
    }
}