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
using TheLearningAgentClient.Models;
using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for ItemPreview.xaml
    /// </summary>
    public partial class ItemPreview : UserControl
    {
        #region AddItem
        public static readonly DependencyProperty AddItemProperty =
            DependencyProperty.Register(
                "AddItem",
                typeof(ICommand),
                typeof(ItemPreview),
                new UIPropertyMetadata(null));
        public ICommand AddItem
        {
            get { return (ICommand)GetValue(AddItemProperty); }
            set { SetValue(AddItemProperty, value); }
        }

        #endregion

        public ItemPreview()
        {
            InitializeComponent();
        }
    }
}
