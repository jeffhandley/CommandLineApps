namespace System.CommandLine
{
    public abstract class CliOption : CliSymbol
    {
        public CliOption(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames)
        {
            Name = name;
            Aliases = aliases;
            ShortNames = shortNames;
        }

        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public IEnumerable<char> ShortNames { get; set; }
        public string? Description { get; set; }
    }

    public partial class CliOption<T> : CliOption
    {
        public CliOption(string name) : base(name, Enumerable.Empty<string>(), Enumerable.Empty<char>()) { }
        public CliOption(string name, char shortName) : base(name, Enumerable.Empty<string>(), new[] { shortName }) { }
        public CliOption(string name, IEnumerable<string> aliases) : base(name, aliases, Enumerable.Empty<char>()) { }
        public CliOption(string name, IEnumerable<string> aliases, char shortName) : base(name, aliases, new[] { shortName }) { }
        public CliOption(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames) : base(name, aliases, shortNames) { }
    }
}
