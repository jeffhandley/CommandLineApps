namespace Source_Generated_Main
{
    partial class Program
    {
        static void Add(string org, string repo, int? issue, int? pr, bool dryRun, params string[] labels)
        {
            GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryRun);
        }

        static void Remove(string org, string repo, int? issue, int? pr, bool dryRun, params string[] labels)
        {
            GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryRun);
        }
    }
}
