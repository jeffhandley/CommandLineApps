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
    }

    public class CliCompletionDirective : CliDirective
    {
        public CliCompletionDirective() : base("complete")
        {
        }

        public bool ShowIfNeeded(CliParseResult cli) => false;
    }
}