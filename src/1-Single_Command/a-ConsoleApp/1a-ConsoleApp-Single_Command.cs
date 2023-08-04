// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

string org = string.Empty;
string repo = string.Empty;
int? issue = null;
int? pr = null;
List<string> labels = new();
bool dryrun = false;

for (int i = 0; i < args.Length; i++)
{
    if (args[i].StartsWith("--"))
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
                issue = int.Parse(args[++i]);
                break;
            case "--pr":
                pr = int.Parse(args[++i]);
                break;
            case "--dry-run":
                dryrun = true;
                break;
        }
    }
    else if (args[i].StartsWith("-"))
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
                    issue = int.Parse(args[++i]);
                    break;
                case 'p':
                    pr = int.Parse(args[++i]);
                    break;
                case 'd':
                    dryrun = true;
                    break;
            }
        }
    }
    else
    {
        labels.Add(args[i]);
    }
}

if (issue is not null)
{
    GitHubHelper.Labels.AddToIssue(org, repo, issue.Value, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.AddToPullRequest(org, repo, pr.Value, labels, dryrun);
}
