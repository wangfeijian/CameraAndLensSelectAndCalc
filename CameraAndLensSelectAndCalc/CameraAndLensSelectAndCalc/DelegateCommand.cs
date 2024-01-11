using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CameraAndLensSelectAndCalc
{
    public class DelegateCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return FuncCanExecute == null || FuncCanExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            ActionExecute?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public Action<object> ActionExecute { get; set; }
        public Func<object, bool> FuncCanExecute { get; set; }
    }
}
