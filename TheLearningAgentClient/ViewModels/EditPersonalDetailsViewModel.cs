using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheLearningAgentClient.Models;
using TheLearningAgentClient.Views;

namespace TheLearningAgentClient.ViewModels
{
    public class EditPersonalDetailsViewModel
    {
        private user m_User;

        public EditPersonalDetailsModel EditPersonalDetails{ get; set; }

        public MainWindow m_MainWindow;
        public MyICommand OKCommand { get; set; }
        public MyICommand CancelCommand { get; set; }


        public EditPersonalDetailsViewModel()
        {
            EditPersonalDetails = new EditPersonalDetailsModel();
            
            OKCommand = new MyICommand(OnOk);
            CancelCommand = new MyICommand(OnCancel);
        }

        internal void SetUser(user i_user)
        {
            if (i_user.address != null)
            {
                EditPersonalDetails.Address = i_user.address.Trim();
            }
            if (i_user.last_name != null)
            {
                EditPersonalDetails.LastName = i_user.last_name.Trim();
            }
            if (i_user.name != null)
            {
                EditPersonalDetails.FirstName = i_user.name.Trim();
            }
            if (i_user.phone != null)
            {
                EditPersonalDetails.Phone = i_user.phone.Trim();
            }
            m_User = i_user;
            LoadLimitations();
        }

        private void OnOk(object parameter)
        {
            if (EditPersonalDetails.Password1 != EditPersonalDetails.Password2)
            {
                OneButtonScreen.ShowMessage("פרט שגוי", "הססמאות אינן זהות");
                return;
            }

            if (!(EditPersonalDetails.Address.Length > 0 &&
                EditPersonalDetails.Phone.Length > 0 &&
                EditPersonalDetails.FirstName.Length > 0 &&
                EditPersonalDetails.LastName.Length > 0))
            {
                OneButtonScreen.ShowMessage("פרט שגוי", "אחד או יותר מהפרטים שגויים");
                return;
            }

            m_User.address = EditPersonalDetails.Address;
            m_User.phone = EditPersonalDetails.Phone;
            m_User.name = EditPersonalDetails.FirstName;
            m_User.last_name = EditPersonalDetails.LastName;
            if (EditPersonalDetails.Password1.Length > 0)
            {
                m_User.password = SecurePasswordHasher.Hash(EditPersonalDetails.Password1);
            }
            m_User = DataBaseManager.UpdateUser(m_User);

            foreach (var item in EditPersonalDetails.LimitationsList)
            {
                try
                {

                    if (item.HasChanged())
                    {
                        user_ref_limit ul = new user_ref_limit();
                        ul.user_id = m_User.user_id;
                        ul.limit_id = item.LimitID;
                        ul.Partial = item.LimitValue == 1;

                        if (item.LimitValue > 0)
                        {
                            DataBaseManager.UpdateUserPreference(ul);
                        }
                        else
                        {
                            DataBaseManager.RemoveUserPreference(ul);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
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
            if (EditPersonalDetails.LimitationsList == null)
            {
                EditPersonalDetails.LimitationsList = new ObservableCollection<EditLimitationModel>();
            }
            else
            {
                EditPersonalDetails.LimitationsList.Clear();
            }

            List<limit> limitations = DataBaseManager.GetLimitations();
            List<user_ref_limit> userLimitations = DataBaseManager.GetUserLimitations((m_User.user_id));

            foreach (limit _limit in limitations)
            {
                EditLimitationModel elm = null;

                foreach (var _url in userLimitations)
                {
                    if (_limit.limit_id == _url.limit_id)
                    {
                        short indication = 0;
                        if ((bool)_url.Partial)
                        {
                            indication = 1;
                        }
                        else
                        {
                            indication = 2;
                        }

                        elm = new EditLimitationModel { LimitValue = indication, Title = _limit.limit_name, LimitID = _limit.limit_id };
                        break;
                    }
                }

                if (elm == null)
                {
                    elm = new EditLimitationModel { LimitValue = 0, Title = _limit.limit_name, LimitID = _limit.limit_id };
                }

                EditPersonalDetails.LimitationsList.Add(elm);
            }
        }
    }
}
