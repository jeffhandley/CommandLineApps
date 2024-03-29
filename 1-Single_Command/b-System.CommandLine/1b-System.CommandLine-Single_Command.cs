﻿// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var org = new CliOption<string>("--org", "-o") { Required = true };
var repo = new CliOption<string>("--repo", "-r") { Required = true };
var issue = new CliOption<int?>("--issue", "-i");
var pr = new CliOption<int?>("--pr", "-p");
var labels = new CliArgument<IEnumerable<string>>("labels") { Arity = ArgumentArity.OneOrMore };
var dryrun = new CliOption<bool>("--dry-run", "-d");

var cli = new CliRootCommand { org, repo, issue, pr, labels, dryrun };

cli.SetAction(cmd =>
{
    if (cmd.GetValue(issue) is not null)
    {
        GitHubHelper.Labels.AddToIssue(
            cmd.GetValue(org)!,
            cmd.GetValue(repo)!,
            cmd.GetValue(issue)!.Value,
            cmd.GetValue(labels)!,
            cmd.GetValue(dryrun)
        );
    }
    else if (cmd.GetValue(pr) is not null)
    {
        GitHubHelper.Labels.AddToPullRequest(
            cmd.GetValue(org)!,
            cmd.GetValue(repo)!,
            cmd.GetValue(pr)!.Value,
            cmd.GetValue(labels)!,
            cmd.GetValue(dryrun)
        );
    }
});

var cmd = cli.Parse(args);
return cmd.Invoke();
