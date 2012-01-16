using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Administration.Commands;
using Administration.Features;
using Administration.Utils;
using Caliburn.Micro;
using Connection.Exceptions;
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
                ShowErrorMessage(exception);
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
                                                     ShowErrorMessage(exception);
                                                 }

                                                 if (onComplete != null)
                                                 {
                                                     onComplete();                                                     
                                                 }
                                             });
        }

        private static void ShowErrorMessage(Exception exception)
        {
            if (exception.InnerException == null)
            {
                MessageBoxService.ShowError(exception);
                return;
            }
            
            if (exception.InnerException.Message.StartsWith("ORA"))
            {
                MessageBoxService.ShowError(GetOracleErrorTranslation(exception.InnerException));
            }
            else
            {
                MessageBoxService.ShowError(exception.InnerException);                           
            }
        }

        private static string GetOracleErrorTranslation(Exception exception)
        {
            if (exception.Message.StartsWith("ORA-00001"))
            {
                return "Constraint violated. Check inserted data.";
            }

            if (exception.Message.StartsWith("ORA-01031"))
            {
                return "You don't have sufficient privileges to complete this operation.";
            }

            return "Unknown database error occured.";
        }
    }
}
