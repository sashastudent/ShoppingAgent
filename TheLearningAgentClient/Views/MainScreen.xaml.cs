using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Page
    {
        public MainScreen(MainWindow mainWindow, user i_user)
        {
            InitializeComponent();

            ScrollViewer scroller = ItemInCartViewControl.FindName("Scroller") as ScrollViewer;
            MainScreenViewModel msvm = new MainScreenViewModel(mainWindow, i_user, scroller);
            this.DataContext = msvm;
        }
    }
}
