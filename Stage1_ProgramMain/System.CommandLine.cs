using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    internal class Cli : IEnumerable<CliSymbol>
    {
        internal static ParseResult Parse(string[] args)
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
        internal IEnumerable<ParseError> Errors;

        internal T GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }
    }

    internal class ParseError
    {
        public string Message { get; internal set; }
    }

    abstract class CliSymbol
    {
        public string Key { get; set; }
    }

    abstract class CliSymbol<T> : CliSymbol
    {
        public T? Value { get; internal set; }
        public T? DefaultValue { get; init; }
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