using System;
using System.Windows.Input;


namespace KeyDash.MVVM
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public event EventHandler? CanExecuteChanged
        {
            add {CommandManager.RequerySuggested += value;}
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
          
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }

        public RelayCommand(Action<object> execute ,Func<object, bool> canEx = null)
        {
            this.execute = execute;
            this.canExecute = canEx;
        }
    }
}
