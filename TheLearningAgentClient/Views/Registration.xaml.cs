using System.Windows;
using System.Windows.Controls;

using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            RegistrationViewModel rvm = new RegistrationViewModel();
            rvm.m_MainWindow = mainWindow;
            this.DataContext = rvm;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.FrameworkElement)sender).Name == "txtPassword")
            {
                ((RegistrationViewModel)this.DataContext).Registration.Password1 = ((PasswordBox)sender).Password;
            }
            if (((System.Windows.FrameworkElement)sender).Name == "txtPassword2")
            {
                ((RegistrationViewModel)this.DataContext).Registration.Password2 = ((PasswordBox)sender).Password;
            }
        }
    }
}
