using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public class CliParser
    {
        public static CliParseResult Parse(string[] args)
        {
            return default(CliParseResult);
        }
    }

    public struct CliParseResult
    {
        public T GetOption<T>(string name)
        {
            return default(T)!;
        }

        public bool HasOption(string name)
        {
            return false;
        }

        public IEnumerable<T> GetArguments<T>()
        {
            return Enumerable.Empty<T>();
        }
    }
}
