using System;
using Caliburn.Micro;
using Action = System.Action;

namespace Common.Infrastucture
{
    public class CommandResult : IResult
    {
        private readonly ICommand _command;
        private readonly Action _onComplete;

        public CommandResult(ICommand command, Action onComplete = null)
        {
            _command = command;
            _onComplete = onComplete;
        }

        public void Execute(ActionExecutionContext context)
        {
            CommandInvoker.Invoke(_command, _onComplete);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = (sender, args) => { };
    }
}
