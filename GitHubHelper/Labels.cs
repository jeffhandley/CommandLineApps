namespace GitHubHelper;

public static class Labels
{
    public static int Add(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
    {
        if (issue is not null)
        {
            return AddToIssue(org, repo, issue.Value, labels.ToArray(), dryrun);
        }
        else if (pr is not null)
        {
            return AddToPullRequest(org, repo, pr.Value, labels.ToArray(), dryrun);
        }

        return 1;
    }

    public static int AddToIssue(string org, string repo, int issue, IEnumerable<string> labels, bool dryrun) =>
        AddToIssue(org, repo, issue, labels.ToArray(), dryrun);

    public static int AddToIssue(string org, string repo, int issue, string[] labels, bool dryrun)
    {
        Console.WriteLine($"Adding labels to an issue.{(dryrun ? " DRY-RUN" : "")}");
        Console.WriteLine($"   org: {org}");
        Console.WriteLine($"  repo: {repo}");
        Console.WriteLine($" issue: {issue}");
        Console.WriteLine($"labels: {string.Join(", ", labels)}");

        return 0;
    }

    public static int AddToPullRequest(string org, string repo, int pr, IEnumerable<string> labels, bool dryrun) =>
        AddToPullRequest(org, repo, pr, labels.ToArray(), dryrun);

    public static int AddToPullRequest(string org, string repo, int pr, string[] labels, bool dryrun)
    {
        Console.WriteLine($"Adding labels to a pull request.{(dryrun ? " DRY-RUN" : "")}");
        Console.WriteLine($"   org: {org}");
        Console.WriteLine($"  repo: {repo}");
        Console.WriteLine($"    PR: {pr}");
        Console.WriteLine($"labels: {string.Join(", ", labels)}");

        return 0;
    }

    public static int Remove(string org, string repo, int? issue, int? pr, IEnumerable<string> labels, bool dryrun)
    {
        if (issue is not null)
        {
            return RemoveFromIssue(org, repo, issue.Value, labels, dryrun);
        }
        else if (pr is not null)
        {
            return RemoveFromPullRequest(org, repo, pr.Value, labels, dryrun);
        }

        return 1;
    }

    public static int RemoveFromIssue(string org, string repo, int issue, IEnumerable<string> labels, bool dryrun)
    {
        Console.WriteLine($"Removing labels from an issue.{(dryrun ? " DRY-RUN" : "")}");
        Console.WriteLine($"   org: {org}");
        Console.WriteLine($"  repo: {repo}");
        Console.WriteLine($" issue: {issue}");
        Console.WriteLine($"labels: {string.Join(", ", labels)}");

        return 0;
    }

    public static int RemoveFromPullRequest(string org, string repo, int pr, IEnumerable<string> labels, bool dryrun)
    {
        Console.WriteLine($"Removing labels from a pull request.{(dryrun ? " DRY-RUN" : "")}");
        Console.WriteLine($"   org: {org}");
        Console.WriteLine($"  repo: {repo}");
        Console.WriteLine($"    PR: {pr}");
        Console.WriteLine($"labels: {string.Join(", ", labels)}");

        return 0;
    }
}
