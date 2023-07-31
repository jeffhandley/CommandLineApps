namespace System.CommandLine
{
    public static partial class CliCompletion
    {
        public static bool ShowIfNeeded(CliParseResult result)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result);
        }

        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result, out exitCode);
        }

        public static CliCompletionDirective AddCompletion(this Cli cli)
        {
            var completion = new CliCompletionDirective();
            cli.AddDirective(completion);

            return completion;
        }
    }
}
