using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLogin
{
    public class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> _execute {  get; set; }
        public Func<bool> _canExecute {  get; set; }

        public DelegateCommand(Action<object> action, Func<bool> canExecute=null)
        {
            this._execute = action;
            this._canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
