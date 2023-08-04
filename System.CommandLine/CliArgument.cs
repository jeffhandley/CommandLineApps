namespace System.CommandLineApp
{
    public class CliArgument : CliSymbol
    {
        public string? Description { get; set; }
        public ushort MinArgs { get; set; }
        public ushort MaxArgs { get; set; }

        public CliArgument(ushort minArgs = 0, ushort maxArgs = 0)
        {
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }
    }

    public partial class CliArgument<T> : CliArgument
    {
        public CliArgument(ushort minArgs = 0, ushort maxArgs = 0) : base(minArgs, maxArgs) { }
    }
}
