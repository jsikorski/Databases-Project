using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Administration.Commands;
using Administration.Features;
using Caliburn.Micro;

namespace Administration.Infrastucture
{
    public class MainViewCommandResult : IResult
    {
        private MainViewModel MainViewModel { get; set; }

        private readonly ICommand _command;

        public MainViewCommandResult(ICommand command)
        {
            _command = command;
        }

        public void Execute(ActionExecutionContext context)
        {
            CommandInvoker.Invoke(_command, () => MainViewModel.IsBusy = false);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}
