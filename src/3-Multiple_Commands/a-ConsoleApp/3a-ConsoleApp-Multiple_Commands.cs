// github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
// github-labels remove --org dotnet --repo runtime --issue 40074 untriaged --dry-run

// if (args.Length == 0) args = new[] { "add", "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "--dry-run" };
if (args.Length == 0) args = new[] { "remove", "--org", "dotnet", "--repo", "runtime", "--pr", "40074", "untriaged", "--dry-run" };

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

static void ShowArgumentError(string arg)
{
    throw new ArgumentException($"Unknown argument '{arg}'");
}

static int? ParseInt(string arg, string argName)
{
    if (int.TryParse(arg, out int val))
    {
        return val;
    }

    throw new ArgumentException($"Argument '{arg}' must be an integer");
}

string command = args[0];
string org = string.Empty;
string repo = string.Empty;
int? issue = null;
int? pr = null;
List<string> labels = new();
bool dryrun = false;

// Parse the command line args

for (int i = 1; i < args.Length; i++)
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
                    issue = ParseInt(args[++i], "i");
                    break;
                case 'p':
                    pr = ParseInt(args[++i], "p");
                    break;
                case 'd':
                    dryrun = true;
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
                dryrun = true;
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

ArgumentException.ThrowIfNullOrWhiteSpace(org);
ArgumentException.ThrowIfNullOrWhiteSpace(repo);
ArgumentOutOfRangeException.ThrowIfZero(labels.Count, "labels");
if (!issue.HasValue && !pr.HasValue) throw new ArgumentException("Either --issue or --pr must be specified");

if (command == "add")
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
    return;
}
else if (command == "remove")
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
    return;
}
