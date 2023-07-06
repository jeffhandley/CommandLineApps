namespace System.CommandLine
{
    public partial class Cli
    {
        public CliCommand Add(CliCommand command) => command;
    }

    public partial class CliCommand
    {
        public string? Description { get; set; }
    }

    public partial class CliOption<T>
    {
        public CliOption(string name, IEnumerable<string> aliases) : this(name)
        {
            Aliases = aliases;
        }

        public string? Description { get; set; }
    }

    public partial class CliArgument<T>
    {
        public string? Description { get; set; }
    }
}
