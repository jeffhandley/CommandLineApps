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

        // CS7002 Unexpected use of a generic name

        //public T this<T>[string name]
        //{
        //    get => GetOption<T>(name);
        //}

        private T GetOptionValue<T>(string name)
        {
            return name switch
            {
                "org" => (T)(object)"dotnet",
                "repo" => (T)(object)"runtime",
                "issue" => (T)(object)(int?)40074,
                "pr" => (T)(object)(int?)null!,
                "dry-run" => (T)(object)true,
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
