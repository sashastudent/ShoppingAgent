using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TheLearningAgentClient
{
    public class ButtonCommandBinding : ICommand
    {
        //delegate command to register method to be executed
        private readonly Action handler;
        private bool isEnabled;

        /// <summary>
        /// Bind method to be executed to the handler
        /// So that it can direct on event execution
        /// </summary>
        /// <param name="handler"></param>
        public ButtonCommandBinding(Action handler)
        {
            // Assign the method name to the handler
            this.handler = handler;
        }

        //Property that helps to
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// method to specify if the event will execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        // Helps to execute the respective method using the handler
        public void Execute(object parameter)
        {
            //calls the respective method that has been registered with the handler
            handler();
        }
    }
}
