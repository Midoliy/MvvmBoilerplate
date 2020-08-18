using System;
using System.Windows.Input;

namespace MvvmLight
{
    public class DelegateCommand : ICommand
    {
        public bool CanExecute(object parameter)
            => _canExecute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
            => _execute();

        public DelegateCommand(Action execute) : this(execute, () => true) { }
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

    }

    public class DelegateCommand<T> : ICommand
    {
        public bool CanExecute(object parameter)
            => _canExecute(Cast(parameter));

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
            => _execute(Cast(parameter));

        private T Cast(object parameter)
            => (parameter == null) ? default : (T)parameter;

        public DelegateCommand(Action<T> execute) : this(execute, _ => true) { }
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

    }
}
