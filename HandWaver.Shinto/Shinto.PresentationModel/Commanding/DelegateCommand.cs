using System;
using System.Windows.Input;

namespace Shinto.PresentationModel.Commanding
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action execute) : this (execute, () => true)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        Action _execute;
        Func<bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
