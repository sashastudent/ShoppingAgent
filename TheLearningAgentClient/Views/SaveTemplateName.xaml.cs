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
using System.Windows.Shapes;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for SaveTemplateName.xaml
    /// </summary>
    public partial class SaveTemplateName : Window
    {
        string name;
        public SaveTemplateName()
        {
            InitializeComponent();
            txtTemplatName.Focus();
        }

        internal string TemplateName()
        {
            return name;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void txtTemplatName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
        private void Submit()
        {
            if (txtTemplatName.Text.Length==0)
            {
                return;
            }
            name = txtTemplatName.Text;
            DialogResult = true;
            Close();
        }

    }
}
