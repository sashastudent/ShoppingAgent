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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheLearningAgentClient.Views;

namespace TheLearningAgentClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //ComboBoxFix.Initialize();
            InitializeComponent();
            //SetPage(new Login(this));
            SetPage(new Login(this));
        }

        private void PageLoaded(object sender, EventArgs e)
        {

            Page page = ((System.Windows.Controls.Page)((System.Windows.Controls.ContentControl)sender).Content);

            if (page != null)
            {
                CenterWindowOnScreen(page);
            }

            if (page.Title == "Main Screen")
            {
                Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                Application.Current.MainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                this.MinWidth += 13.6;
                this.MinHeight += 37.6;



                //TODO
                this.Height = 700;
                this.Width = 900;
            }
            else
            {
                Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize;
                Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        public void SetPage(Page page)
        {
            Main.Navigate(page);
        }

        public void GoToPrevPage()
        {
            Main.GoBack();
        }

        private void CenterWindowOnScreen(Page page)
        {
            this.MinHeight = page.MinHeight;
            this.MaxHeight = page.MaxHeight;
            this.MinWidth = page.MinWidth;
            this.MaxWidth = page.MaxWidth;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            

        }
    }
}
