using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TheLearningAgentClient
{
    public class MyICommand : ICommand
    {
        Func<bool> _TargetCanExecuteMethod;

        public object parameter;
        private Action<object> _TargetExecuteMethod;

        public MyICommand(Action<object> executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
            parameter = null;
        }

        public MyICommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
            parameter = null;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }


        // Prism commands solve this in their implementation 
        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod(parameter);
            }
        }
    }
}