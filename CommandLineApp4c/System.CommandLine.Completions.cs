namespace System.CommandLine
{
    public static class CliCompletion
    {
        public static CliCompletionDirective AddCompletion(this Cli cli)
        {
            var completion = new CliCompletionDirective();
            cli.AddDirective(completion);

            return completion;
        }

        public static bool ShowIfNeeded(CliParseResult result) => ShowIfNeeded(result, out _);

        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result, out exitCode);
        }
    }

    public class CliCompletionDirective : CliDirective
    {
        public CliCompletionDirective() : base("complete")
        {
        }

        public bool IsNeeded(CliParseResult result) => result.HasDirective(this.Name);

        public bool ShowIfNeeded(CliParseResult result) => ShowIfNeeded(result, out _);

        public bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            exitCode = 0;

            if (result.HasDirective(this.Name))
            {
                // Write out the completion responses
                return true;
            }

            return false;
        }
    }
}
