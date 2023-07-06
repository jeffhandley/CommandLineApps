namespace System.CommandLine
{
    public static partial class CliCompletion
    {
        public static bool ShowIfNeeded(CliParseResult result)
        {
            var completion = new CliCompletionDirective();
            return completion.ShowIfNeeded(result);
        }
    }

    public partial class CliCompletionDirective : CliDirective
    {
        public CliCompletionDirective() : base("complete") { }
        public bool IsNeeded(CliParseResult result) => result.HasDirective(this);
        public bool ShowIfNeeded(CliParseResult result)
        {
            if (IsNeeded(result))
            {
                // Write out the completion responses
                return true;
            }

            return false;
        }
    }
}
