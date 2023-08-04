namespace System.CommandLineApp
{
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
