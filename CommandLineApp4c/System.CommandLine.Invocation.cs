namespace System.CommandLine
{
    public static partial class CliInvocation
    {
        public static bool Invoke(this Cli cli, string[] args, Dictionary<CliCommand, Func<CliParseResult, int>> actions, out int exitCode)
        {
            cli.AddHelp();
            cli.AddCompletion();

            var result = cli.Parse(args);

            if (CliCompletion.ShowIfNeeded(result, out exitCode)) return true;
            if (CliHelp.ShowIfNeeded(result, out exitCode)) return true;

            if (result.InvokedCommand is not null && actions.TryGetValue(result.InvokedCommand, out var action))
            {
                exitCode = action(result);
                return true;
            }

            exitCode = 0;
            return false;
        }
    }
}
