namespace System.CommandLine
{
    public static partial class CliCompletion
    {
        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result, out exitCode);
        }

        public static CliCompletionDirective AddCompletion(this Cli cli)
        {
            var completion = new CliCompletionDirective();
            cli.Add(completion);

            return completion;
        }
    }

    public partial class CliCompletionDirective
    {
        public bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            exitCode = 0;

            if (IsNeeded(result))
            {
                // Write out the completion responses
                return true;
            }

            return false;
        }
    }
}
