using System.CommandLineApp;

namespace System.CommandLine.ModelBinding
{
    public class ModelBoundCli<TCommands> : Cli where TCommands : new()
    {
        public ModelBoundCli()
        {
            foreach (var command in typeof(TCommands).GetProperties(Reflection.BindingFlags.Public | Reflection.BindingFlags.Instance))
            {
                AddCommand(command.Name.ToLower());
            }
        }

        public ModelBoundResult<TCommands> Parse(IEnumerable<string> args)
        {
            var result = CliParser.Parse(args);

            // Operate over the result and bind to the TCommands
            var commands = new TCommands();

            return new(result, commands);
        }
    }
}
