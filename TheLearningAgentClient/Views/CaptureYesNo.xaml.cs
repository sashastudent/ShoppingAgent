using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class CaptureYesNo : Window
    {
        public CaptureYesNo(string title, string subtitle)
        {
            InitializeComponent();
            Title.Content = title;
            SubTitle.Text = subtitle;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public static bool AskYesNoQuestion(string title, string subtitle)
        {
            CaptureYesNo cyn = new CaptureYesNo(title, subtitle);
            return ((bool)cyn.ShowDialog());
        }
    }

    
}
