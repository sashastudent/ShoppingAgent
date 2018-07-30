using System.Windows.Input;
using TheLearningAgentClient.Models;
using TheLearningAgentClient.Views;

namespace TheLearningAgentClient.ViewModels
{
    public class LoginViewModel
    {
        public LoginModel Login { get; set; }

        public MainWindow m_MainWindow;
        public MyICommand ConnectCommand { get; set; }
        public MyICommand RegisterCommand { get; set; }
        public MyICommand ExitCommand { get; set; }

        public LoginViewModel()
        {
            Login = new LoginModel();
            ConnectCommand = new MyICommand(OnConnect);
            RegisterCommand = new MyICommand(OnRegisterClicked);
            ExitCommand = new MyICommand(OnExit);
        }

        private void OnConnect(object parameter)
        {
            Connect();
        }

        private void OnRegisterClicked(object parameter)
        {
            Register();
        }

        private void OnExit(object parameter)
        {
            m_MainWindow.SetPage(null);
            m_MainWindow.Close();
        }

        private void Register()
        {
            m_MainWindow.SetPage(new Registration(m_MainWindow));
        }

        public void Connect()
        {
            user u = DataBaseManager.GetValidUser(Login.UserName, Login.Password);

            /*if (u == null)
            {
                u = DataBaseManager.GetValidUser("Naama", "123");
            }*/

            if (u != null)
            {
                m_MainWindow.SetPage(new MainScreen(m_MainWindow, u));
            }
            else
            {
                OneButtonScreen.ShowMessage("פרט שגוי", "שם משתמש או סיסמה לא נכונים");
            }
        }

        
    }
}
