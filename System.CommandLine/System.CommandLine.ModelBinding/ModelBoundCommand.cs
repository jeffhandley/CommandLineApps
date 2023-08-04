using System.CommandLineApp;

namespace System.CommandLine.ModelBinding
{
    public class ModelBoundCommand<T> : System.CommandLineApp.CliCommand
    {
        public ModelBoundCommand(string name) : base(name) { }
    }
}
