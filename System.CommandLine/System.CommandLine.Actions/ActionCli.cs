using System.CommandLineApp;

namespace System.CommandLine.Actions
{
    public class ActionCli : Cli
    {
        Dictionary<System.CommandLineApp.CliDirective, Func<CliParseResult, int>> _directiveActions = new();
        Dictionary<System.CommandLineApp.CliCommand, Func<CliParseResult, int>> _commandActions = new();
        Dictionary<System.CommandLineApp.CliOption, Func<CliParseResult, int>> _optionActions = new();

        CliCompletionDirective _completion;
        CliHelpOption _help;

        public ActionCli()
        {
            _completion = new CliCompletionDirective();
            _help = new CliHelpOption();

            this.SetAction(_completion, cmd =>
            {
                _completion.ShowIfNeeded(cmd, out int exitCode);
                return exitCode;
            });

            this.SetAction(_help, cmd =>
            {
                _help.ShowIfNeeded(cmd, out int exitCode);
                return exitCode;
            });
        }

        public void SetAction(System.CommandLineApp.CliDirective directive, Func<CliParseResult, int> action) =>
            _directiveActions.Add(directive, action);

        public void SetAction(System.CommandLineApp.CliCommand command, Func<CliParseResult, int> action) =>
            _commandActions.Add(command, action);

        public void SetAction<T>(System.CommandLineApp.CliOption<T> option, Func<CliParseResult, int> action) =>
            _optionActions.Add(option, action);

        public CliActionResult Invoke(IEnumerable<string> args)
        {
            this.AddDirective(_completion);
            this.AddOption(_help);

            var result = CliParser.Parse(this, args);
            System.CommandLineApp.CliSymbol? invokedSymbol = null;
            int? exitCode = null;

            foreach (var directive in _directiveActions.Keys)
            {
                if (result.HasDirective(directive))
                {
                    exitCode = _directiveActions[directive](result);
                    invokedSymbol = directive;
                }
            }

            if (invokedSymbol is null)
            {
                foreach (var option in _optionActions.Keys)
                {
                    if (result.GetOption<bool>((System.CommandLineApp.CliOption<bool>)option))
                    {
                        exitCode = _optionActions[option](result);
                        invokedSymbol = option;
                    }
                }
            }

            if (invokedSymbol is null)
            {
                foreach (var command in _commandActions.Keys)
                {
                    if (result.HasCommand(command))
                    {
                        exitCode = _commandActions[command](result);
                        invokedSymbol = command;
                    }
                }
            }

            return new CliActionResult(result, invokedSymbol, exitCode ?? 1);
        }
    }
}
