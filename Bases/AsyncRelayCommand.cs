using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace DataIntegration.Bases
{
    public class AsyncRelayCommand : ICommand
    {
        private bool isExecuting;
        private readonly Func<object, Task> execute;
        private readonly Predicate<object> canExecute;
        private readonly Action<Exception, object> onException;

        private Dispatcher Dispatcher { get; }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null, Action<Exception, object> onException = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            this.onException = onException;
            Dispatcher = Application.Current.Dispatcher;
        }

        private void InvalidateRequerySuggested()
        {
            if (Dispatcher.CheckAccess())
                CommandManager.InvalidateRequerySuggested();
            else
                Dispatcher.Invoke(CommandManager.InvalidateRequerySuggested);
        }

        public bool CanExecute(object parameter) => !isExecuting && (canExecute == null || canExecute(parameter));

        private async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    isExecuting = true;
                    InvalidateRequerySuggested();
                    await execute(parameter);
                }
                catch (Exception e)
                {
                    onException?.Invoke(e, parameter);
                }
                finally
                {
                    isExecuting = false;
                    InvalidateRequerySuggested();
                }
            }
        }

        public void Execute(object parameter) => _ = ExecuteAsync(parameter);
    }
}
