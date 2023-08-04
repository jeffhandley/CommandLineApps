using System.CommandLineApp;

namespace System.CommandLine.Invocation
{
    public static partial class CliInvocation
    {
        public static bool Invoke(this Cli cli, string[] args, Dictionary<System.CommandLineApp.CliCommand, Func<CliParseResult, int>> actions)
        {
            cli.AddCompletion();
            cli.AddHelp();

            var result = cli.Parse(args);

            if (CliCompletion.ShowIfNeeded(result)) return true;
            if (CliHelp.ShowIfNeeded(result)) return true;

            foreach (var action in actions)
            {
                if (result.HasCommand(action.Key.Name))
                {
                    action.Value(result);
                    return true;
                }
            }

            return false;
        }

        public static bool Invoke(this Cli cli, IEnumerable<string> args, Dictionary<System.CommandLineApp.CliCommand, Func<CliParseResult, int>> actions, out int exitCode)
        {
            cli.AddHelp();
            cli.AddCompletion();

            var result = CliParser.Parse(cli, args);

            if (CliCompletion.ShowIfNeeded(result, out exitCode)) return true;
            if (CliHelp.ShowIfNeeded(result, out exitCode)) return true;

            foreach (var action in actions)
            {
                if (result.HasCommand(action.Key.Name))
                {
                    exitCode = action.Value(result);
                    return true;
                }
            }

            exitCode = 0;
            return false;
        }
    }
}
