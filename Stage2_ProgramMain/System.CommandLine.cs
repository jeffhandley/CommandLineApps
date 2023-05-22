using System.Collections;
using System.Collections.Generic;

namespace System.CommandLine
{
    internal class Cli : IEnumerable<CliSymbol>
    {
        public Cli(string description)
        {
            Description = description;
        }

        public string? Description { get; set; }

        internal ParseResult Parse(string[] args)
        {
            throw new NotImplementedException();
        }

        public void Add(CliSymbol symbol) { }

        public CliOption<T> AddOption<T>(CliOption<T> option)
        {
            return option;
        }

        public CliArgument<T> AddArgument<T>(CliArgument<T> argument)
        {
            return argument;
        }

        IEnumerator<CliSymbol> IEnumerable<CliSymbol>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void ProvideHelp(params string[] options) { }
        public void ProvideCompletion(string directive = "[complete]") { }
        public void ProvideErrorHandling(int exitCode = 100) { }
    }

    internal class ParseResult
    {
        internal IEnumerable<ParseError> Errors = new List<ParseError>();

        internal T GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }
    }

    internal class ParseError
    {
        public ParseError(string message) => Message = message;

        public string Message { get; internal set; }
    }

    abstract class CliSymbol
    {
        public string? Key { get; init; }
    }

    abstract class CliSymbol<T> : CliSymbol
    {
#nullable disable
        internal CliSymbol() { }
#nullable restore

        public T Value { get; internal set; }
        public T? DefaultValue { get; init; }

        public IList<CliSymbolValidator<T>> Validators { get; } = new List<CliSymbolValidator<T>>();
    }

    internal class CliSymbolValidator<T>
    {
        public CliSymbolValidator(Func<T, bool> value, string errorMessage)
        {
        }
    }

    class CliOption<T> : CliSymbol<T>
    {
        private string Name;
        private string[] Aliases;
        private string Description;

        public CliOption(string name, string[] aliases, string description)
        {
            this.Name = name;
            this.Aliases = aliases;
            this.Description = description;
        }

        public CliOption(string name, string alias, string description) : this(name, new[] { alias }, description) { }
    }

    class CliArgument<T> : CliSymbol<T>
    {
        private string Description;

        public CliArgument(string description)
        {
            this.Description = description;
        }
    }
}

namespace System.CommandLine.Validation
{
    internal class ExistingFilesOnly : CliSymbolValidator<FileInfo>
    {
        public ExistingFilesOnly() : base(f => f.Exists, "The specified file does not exist")
        {

        }
    }
}
