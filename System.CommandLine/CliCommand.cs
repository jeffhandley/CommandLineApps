namespace System.CommandLineApp
{
    public partial class CliCommand : CliSymbol
    {
        public CliCommand(string name) => Name = name;
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<CliCommand> Commands { get; } = new();
        public List<CliOption> Options { get; } = new();
        public List<CliArgument> Arguments { get; } = new();

        public CliCommand AddCommand(CliCommand command)
        {
            command.Parent = this;
            Commands.Add(command);

            return command;
        }

        public CliCommand AddCommand(string name) => AddCommand(new CliCommand(name));

        public CliArgument<T> AddArgument<T>(CliArgument<T> argument)
        {
            argument.Parent = this;
            Arguments.Add(argument);

            return argument;
        }

        public CliArgument<T> AddArguments<T>(ushort minArgs = 0, ushort maxArgs = 0) => AddArgument(new CliArgument<T>(minArgs, maxArgs));

        public CliOption<T> AddOption<T>(CliOption<T> option)
        {
            option.Parent = this;
            Options.Add(option);

            return option;
        }

        public CliOption<T> AddOption<T>(string name) => AddOption(new CliOption<T>(name, name[0]));
        public CliOption<T> AddOption<T>(string name, char abbr) => AddOption(new CliOption<T>(name, abbr));
    }
}
