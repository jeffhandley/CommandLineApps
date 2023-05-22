using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    internal class Cli : IEnumerable<CliSymbol>
    {
        internal ParseResult Parse(string[] args)
        {
            throw new NotImplementedException();
        }

        public void Add(CliSymbol symbol) { }

        IEnumerator<CliSymbol> IEnumerable<CliSymbol>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
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
        public T? Value { get; internal set; }
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
        private string v1;
        private string[] strings;
        private string v2;

        public CliOption(string v1, string[] strings, string v2)
        {
            this.v1 = v1;
            this.strings = strings;
            this.v2 = v2;
        }
    }

    class CliArgument<T> : CliSymbol<T>
    {
        private string v;

        public CliArgument(string v)
        {
            this.v = v;
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
