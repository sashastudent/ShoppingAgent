using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            LoginViewModel lvm = new LoginViewModel();
            lvm.m_MainWindow = mainWindow;
            this.DataContext = lvm;

            txtUserName.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginViewModel)this.DataContext).Login.Password = ((PasswordBox)sender).Password;
        }

        private void txtTemplatName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((LoginViewModel)this.DataContext).Connect();
            }
        }

      
    }
}
