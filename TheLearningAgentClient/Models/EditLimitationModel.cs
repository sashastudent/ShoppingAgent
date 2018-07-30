using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLearningAgentClient.Models
{
    public class EditLimitationModel : INotifyPropertyChanged
    {
        private string title;
        private int limitValue;
        private int limitID;

        private bool FirstTimeFlag = false;
        private int initValue;

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public int LimitValue
        {
            get
            {
                return limitValue;
            }

            set
            {
                if (!FirstTimeFlag)
                {
                    initValue = value;
                    FirstTimeFlag = true;
                }
                if (limitValue != value)
                {
                    limitValue = value;
                    RaisePropertyChanged("LimitValue");
                }
            }
        }
        public int LimitID
        {
            get
            {
                return limitID;
            }

            set
            {
                if (limitID != value)
                {
                    limitID = value;
                    RaisePropertyChanged("LimitID");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        internal bool HasChanged()
        {
            return initValue != limitValue;
        }
    }
}
