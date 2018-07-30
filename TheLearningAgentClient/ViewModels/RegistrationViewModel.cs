using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheLearningAgentClient.Models;
using TheLearningAgentClient.Views;

namespace TheLearningAgentClient.ViewModels
{
    public class RegistrationViewModel
    {
        public RegistrationModel Registration { get; set; }

        public MainWindow m_MainWindow;

        public MyICommand OKCommand { get; set; }
        public MyICommand CancelCommand { get; set; }

        public RegistrationViewModel()
        {
            Registration = new RegistrationModel();
            LoadLimitations();
            OKCommand = new MyICommand(OnOk);
            CancelCommand = new MyICommand(OnCancel);
        }

        private void OnOk(object parameter)
        {
            if (DataBaseManager.DoesUserNameExistsAlready(Registration.UserName))
            {
                OneButtonScreen.ShowMessage("בחר שם משתמש אחר", "שם המשתמש שבחרת כבר תפוס");
                return;
            }
            if (Registration.Password1 != Registration.Password2)
            {
                OneButtonScreen.ShowMessage("פרט שגוי", "הססמאות אינן זהות");
                return;

            }
            if (!(Registration.FirstName.Length > 0 &&
                Registration.LastName.Length > 0 &&
                Registration.Address.Length > 0 &&
                Registration.Password1.Length > 0 &&
                Registration.Password2.Length > 0 &&
                Registration.UserName.Length > 0 &&
                Registration.Password1.Length > 0))
            {
                OneButtonScreen.ShowMessage("פרט שגוי", "אחד או יותר מהפרטים שגויים");
                return;
            }

            string SaltedAndHashedPassword = SecurePasswordHasher.Hash(Registration.Password1);

            user u = new user();
            u.user_name = Registration.UserName;
            u.password = SaltedAndHashedPassword;//textbox_pswd.Text;
            u.address = Registration.Address;
            u.name = Registration.FirstName;
            u.last_name = Registration.LastName;
            DataBaseManager.AddUser(u);

            if (u.user_id != 0)
            {
                bool changed = false;

                List<user_ref_limit> list = new List<user_ref_limit>();

                foreach (var item in Registration.LimitationsList)
                {
                    try
                    {
                        if (item.LimitValue > 0)
                        {
                            user_ref_limit ul = new user_ref_limit();
                            ul.user_id = u.user_id;
                            ul.limit_id = item.LimitID;
                            ul.Partial = item.LimitValue == 1;
                            list.Add(ul);

                            changed = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                }

                if (changed)
                {
                    DataBaseManager.AddUserLimitations(list);
                }
            }


            m_MainWindow.GoToPrevPage();
        }

        private void OnCancel(object parameter)
        {
            m_MainWindow.GoToPrevPage();
        }

        private void LoadLimitations()
        {
            if (Registration.LimitationsList == null)
            {
                Registration.LimitationsList = new ObservableCollection<EditLimitationModel>();
            }
            else
            {
                Registration.LimitationsList.Clear();
            }
            
            var list = DataBaseManager.GetLimitations();
            foreach (var item in list)
            {
                Registration.LimitationsList.Add(new EditLimitationModel { LimitValue = 0, Title = item.limit_name, LimitID = item.limit_id });
            }
        }
    }
}
