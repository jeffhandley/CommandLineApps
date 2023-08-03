namespace System.CommandLine
{
    public struct CliParseResult
    {
        internal IEnumerable<string> args { get; init; } = Enumerable.Empty<string>();

        public CliParseResult() { }

        public T GetOption<T>(string name, char? abbr = null) => GetOptionValue<T>(name)!;
        public T GetOption<T>(CliOption<T> option) => GetOption<T>(option.Name);
        public IEnumerable<T> GetArguments<T>(ushort minArgs, ushort maxArgs = 0) => GetArgumentValues<T>();
        public IEnumerable<T> GetArguments<T>(CliArgument<T> argument) => GetArguments<T>(argument.MinArgs, argument.MaxArgs);
        public bool HasDirective(string name) => args.Contains($"[{name}]");
        public bool HasDirective(CliDirective directive) => HasDirective(directive.Name);
        public bool HasCommand(string name) => args.Contains(name);
        public bool HasCommand(CliCommand command) => HasCommand(command.Name);
        public bool HasOption(string name) => args.Contains($"--{name}");
        public bool HasOption(CliOption option) => HasOption(option.Name);
        public bool HasErrors { get; } = false;

        private T GetOptionValue<T>(string name)
        {
            return name switch
            {
                "org" => (T)(object)(string)(args.Contains("--org") || args.Contains("-o") ? "dotnet" : (string)null!),
                "repo" => (T)(object)(string)(args.Contains("--org") || args.Contains("-o") ? "runtime" : (string)null!),
                "issue" => (T)(object)(args.Contains("--issue") || args.Contains("-i") ? (int?)40074 : (int?)null)!,
                "pr" => (T)(object)(args.Contains("--pr") || args.Contains("-p") ? (int?)40074 : (int?)null)!,
                "dry-run" => (T)(object)(bool)(args.Contains("--dry-run") || args.Contains("-d")),
                "help" => (T)(object)(bool)(args.Contains("--help") || args.Contains("-h") || args.Contains("-?")),
                _ => default(T)!,
            };
        }

        private IEnumerable<T> GetArgumentValues<T>()
        {
            if (this.HasCommand("add"))
            {
                return new[] { (T)(object)"area-System.Security" };
            }
            else if (this.HasCommand("remove"))
            {
                return new[] { (T)(object)"untriaged" };
            }

            return new[] { (T)(object)"area-System.Security", (T)(object)"untriaged" };
        }
    }
}
