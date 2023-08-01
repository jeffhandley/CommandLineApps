namespace System.CommandLine.ModelBinding
{
    public static class CliExtensions
    {
        public static ModelBoundCommand<T> AddCommand<T>(this Cli cli, string name)
        {
            var command = new ModelBoundCommand<T>(name);
            cli.AddCommand(command);

            // Add the options and arguments discovered for the command based on TOptions

            return command;
        }

        public static bool TryGetCommand<T>(this CliParseResult result, ModelBoundCommand<T> command, out T commandArgs) where T : new()
        {
            commandArgs = new T();
            return result.HasCommand(command);
        }
    }
}
