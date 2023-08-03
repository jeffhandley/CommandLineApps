using System.CommandLine;

// apply-labels --org dotnet --repo runtime --issue 40074 area-System.Security untriaged --dry-run
if (args.Length == 0) args = new[] { "--org", "dotnet", "--repo", "runtime", "--issue", "40074", "area-System.Security", "untriaged", "--dry-run" };

var cmd = CliParser.Parse(args);
var org = cmd.GetOption<string>("org");
var repo = cmd.GetOption<string>("repo");
var issue = cmd.GetOption<int?>("issue");
var pr = cmd.GetOption<int?>("pr");
var labels = cmd.GetArguments<string>(minArgs: 1);
var dryrun = cmd.GetOption<bool>("dry-run");

if (issue is not null)
{
    GitHubHelper.Labels.Add(org, repo, issue, pr, labels, dryrun);
}
else if (pr is not null)
{
    GitHubHelper.Labels.Remove(org, repo, issue, pr, labels, dryrun);
}

/* Some behavioral notes for the APIs illustrated below:
 * - The '--' prefix is inferred for options
 * - The first character of the option name is used as the abbreviation, but this can be overridden with an overload
 * - The '-' prefix is inferred for option abbreviations, and bundling would work by default
 * - If there are collisions on option names or abbreviations, an exception is thrown
 * - If a non-nullable option is not specified, an exception is thrown
 * - If a nullable option is not specified, the default value is null
 *
 * How it would work:
 * - Under the covers, the parse result goes through iterative parsing
 * - At first, the result would have lots of "unrecognized tokens"
 * - After each call to `GetOption()` or `GetArguments()`, tokens would become recognized
 * - The unrecognized tokens list then shrinks, with the recognized tokens growing
 * - The consumer could check `UnrecognizedTokens` or `HasUnrecognizedTokens` if desired
 * - If the sequence of calls to get the options or arguments is detected as potentially
 *   producing ambiguous results, an exception would be thrown.
 *
 * What about more advanced features like sub-commands?
 * - You'd be guided toward modeling your CLI as shown in subsequent stages
 */
