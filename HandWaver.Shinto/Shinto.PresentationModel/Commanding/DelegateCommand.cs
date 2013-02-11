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

        public void CheckCanExecute()
        {
            OnCanExecuteChanged();
        }

        public bool CanExecute(object parameter)
        {
            bool can = _canExecute();            
            return can;
        }

        public event EventHandler CanExecuteChanged;

        protected void OnCanExecuteChanged()
        {
            if (null != CanExecuteChanged)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
