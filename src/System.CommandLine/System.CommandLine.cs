namespace System.CommandLine
{
    public static partial class CliParser
    {
        public static CliParseResult Parse(string[] args) => new CliParseResult { args = args };
        public static CliParseResult Parse(this Cli cli, string[] args) => new CliParseResult { args = args };
    }

    public partial struct CliParseResult
    {
        internal string[] args { get; init; } = new string[0];
        public IEnumerable<T> GetArguments<T>(ushort minArgs, ushort maxArgs = 0) => Enumerable.Empty<T>();
        public T GetOption<T>(string name, char? abbr = null) => default!;

        public bool HasFlag(string name, char? abbr = null) => false;
        public CliParseResult() { }
        public bool HasErrors { get; } = false;

        public bool HasCommand(string name) => args?.Contains(name) ?? false;
        public bool HasDirective(CliDirective directive) => false;
        public T GetOption<T>(CliOption<T> option) => default(T)!;
        public bool HasCommand(CliCommand command) => args?.Contains(command.Name) ?? false;
        public IEnumerable<T> GetArguments<T>(CliArgument<T> argument) => Enumerable.Empty<T>();
    }

    public abstract partial class CliSymbol
    {
    }

    public abstract partial class CliSymbolGroup
    {
        public CliCommand AddCommand(string name) => new CliCommand(name);
        public CliOption<T> Add<T>(CliOption<T> option) => option;
        public CliOption<T> AddOption<T>(string name, char abbr) => Add(new CliOption<T>(name, abbr));
        public CliGroup AddGroup(CliGroupType type) => new CliGroup(type);
        public CliArgument<T> Add<T>(CliArgument<T> argument) => argument;
        public CliArgument<T> AddArguments<T>(ushort minArgs = 0, ushort maxArgs = 0) => Add(new CliArgument<T>(minArgs, maxArgs));
        public CliDirective Add(CliDirective directive) => directive;
        public CliCommand Add(CliCommand command) => command;
    }

    public partial class Cli : CliSymbolGroup
    {
    }

    public partial class CliCommand : CliSymbolGroup
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public CliCommand(string name) => Name = name;
    }

    public partial class CliOption<T> : CliSymbol
    {
        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public IEnumerable<char> ShortNames { get; set; }
        public CliOption(string name)
        {
            Name = name;
            Aliases = Enumerable.Empty<string>();
            ShortNames = Enumerable.Empty<char>();
        }
        public CliOption(string name, char shortName)
        {
            Name = name;
            Aliases = Enumerable.Empty<string>();
            ShortNames = new char[] { shortName };
        }
        public CliOption(string name, IEnumerable<string> aliases) : this(name)
        {
            Aliases = aliases;
        }

        public string? Description { get; set; }
    }

    public partial class CliDirective : CliSymbol
    {
        public string Name { get; set; }
        public CliDirective(string name) => Name = name;
    }

    public partial class CliParseException : Exception { }

    public partial class CliArgument<T> : CliSymbol
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

    public enum CliGroupType
    {
        Any,
        All,
        MutuallyExclusive
    }

    public partial class CliGroup : CliSymbolGroup
    {
        public CliGroupType Type { get; set; }
        public CliGroup(CliGroupType type) => Type = type;
    }
}
