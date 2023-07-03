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

        public bool IsNeeded(CliParseResult result) => result.HasDirective(this.Name);

        public bool ShowIfNeeded(CliParseResult result)
        {
            if (result.HasDirective(this.Name))
            {
                // Write out the completion responses
                return true;
            }

            return false;
        }
    }
}