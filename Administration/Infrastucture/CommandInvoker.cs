using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Commands;
using Administration.Utils;
using Caliburn.Micro;
using Action = System.Action;

namespace Administration.Infrastucture
{
    public class CommandInvoker
    {
        public static void Execute(ICommand command)
        {
            try
            {
                command.Execute();
            }
            catch (Exception exception)
            {
                MessageBoxService.ShowError(exception);
            }
        }

        public static void Invoke(ICommand command, Action onComplete)
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 try
                                                 {
                                                     command.Execute();
                                                 }
                                                 catch (Exception exception)
                                                 {
                                                     MessageBoxService.ShowError(exception);
                                                 }

                                                 if (onComplete != null)
                                                 {
                                                     onComplete();                                                     
                                                 }
                                             });
        }
    }
}
