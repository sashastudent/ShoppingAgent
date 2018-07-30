using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLearningAgentClient.Models
{
    public class RegistrationModel : INotifyPropertyChanged
    {
        private string userName = "";
        private string firstName = "";
        private string lastName = "";
        private string address = "";
        private string password1 = "";
        private string password2 = "";

        public ObservableCollection<EditLimitationModel> LimitationsList { get; set; }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                if (userName != value)
                {
                    userName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }

        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        public string Password1
        {
            get
            {
                return password1;
            }

            set
            {
                if (password1 != value)
                {
                    password1 = value;
                    RaisePropertyChanged("Password1");
                }
            }
        }

        public string Password2
        {
            get
            {
                return password2;
            }

            set
            {
                if (password2 != value)
                {
                    password2 = value;
                    RaisePropertyChanged("Password2");
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
    }
}
