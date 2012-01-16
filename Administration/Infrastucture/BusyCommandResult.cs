using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Caliburn.Micro;

namespace Administration.Infrastucture
{
    public class BusyCommandResult : IResult
    {
        private readonly ICommand _command;
        private readonly IBusyScope _busyScope;

        public BusyCommandResult(ICommand command, IBusyScope busyScope)
        {
            _command = command;
            _busyScope = busyScope;
        }

        public void Execute(ActionExecutionContext context)
        {
            CommandInvoker.InvokeBusy(_command, _busyScope);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}
