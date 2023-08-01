using System.CommandLine.ModelBinding;

namespace Source_Generated_Main
{
    partial class Program
    {
        // github-labels add --org dotnet --repo runtime --issue 40074 area-System.Security --dry-run
        // github-labels remove --org dotnet --repo runtime --pr 40074 untriaged --dry-run

        static void Add(string org, string repo, int? issue, int? pr, [Argument(minArgs: 1)] IEnumerable<string> labels, bool dryRun)
        {
            GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryRun);
        }

        static void Remove(string org, string repo, int? issue, int? pr, [Argument(minArgs: 1)] IEnumerable<string> labels, bool dryRun)
        {
            GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryRun);
        }
    }
}