using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public static partial class CliParser
    {
        public static CliParseResult ParseOptions(string[] args) => default;
    }

    public partial struct CliParseResult
    {
        public T GetOption<T>(string name, char? abbr = null) => default!;
        public bool HasOption(string name, char? abbr = null) => false;
    }
}
