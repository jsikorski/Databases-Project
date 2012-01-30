using System;
using System.Threading;
using Common.Utils;

namespace Common.Infrastucture
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

        public static void Invoke(ICommand command, Action onComplete = null)
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

        public static void InvokeBusy(ICommand command, IBusyScope busyScope)
        {
            busyScope.IsBusy = true;
            Invoke(command, () => busyScope.IsBusy = false);
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

            if (exception.Message.StartsWith("ORA-02292"))
            {
                return "Constraint violated. Cannot complete this operation.";
            }

            if (exception.Message.StartsWith("ORA-01031") || exception.Message.StartsWith("ORA-00942"))
            {
                return "You don't have sufficient privileges to complete this operation.";
            }

            if (exception.Message.StartsWith("ORA-02290"))
            {
                return "Cannot execute this operation. Refresh Your view and try again.";
            }

            return "Unknown database error occured.";
        }
    }
}
