namespace System.CommandLine
{
    public partial class CliParseException : Exception
    {
        public CliParseResult ParseResult { get; init; }
    }
}
