using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class EditPersonalDetails : Page
    {
        public EditPersonalDetails(MainWindow mainWindow,user i_user)
        {
            InitializeComponent();
            EditPersonalDetailsViewModel epdvm = new EditPersonalDetailsViewModel();
            epdvm.SetUser(i_user);
            epdvm.m_MainWindow = mainWindow;
            this.DataContext = epdvm;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((System.Windows.FrameworkElement)sender).Name == "txtPassword")
            {
                ((EditPersonalDetailsViewModel)this.DataContext).EditPersonalDetails.Password1 = ((PasswordBox)sender).Password;
            }
            if (((System.Windows.FrameworkElement)sender).Name == "txtPassword2")
            {
                ((EditPersonalDetailsViewModel)this.DataContext).EditPersonalDetails.Password2 = ((PasswordBox)sender).Password;
            }
        }
    }
}
