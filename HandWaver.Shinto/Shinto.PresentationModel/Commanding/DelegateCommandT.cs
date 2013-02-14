using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Shinto.PresentationModel.Commanding
{
    public class DelegateCommand<T> : ICommand
    {
        public DelegateCommand(Action<T> execute) : this(execute, (arg) => true)
        {

        }

        public DelegateCommand(Action<T> execute, Func<T,bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        Action<T> _execute;
        Func<T, bool> _canExecute;

        public bool CanExecute(object parameter)
        {
            T arg = (T)parameter;
            return _canExecute(arg);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            T arg = (T)parameter;
            _execute(arg);
        }
    }
}
