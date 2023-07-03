namespace System.CommandLine
{
    public static class CliCompletion
    {
        public static bool ShowIfNeeded(CliParseResult result)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result);
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
