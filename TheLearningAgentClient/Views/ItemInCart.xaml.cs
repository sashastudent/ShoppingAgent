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
    /// Interaction logic for ItemPreview.xaml
    /// </summary>
    public partial class ItemInCart : UserControl
    {
        #region DelItem
        public static readonly DependencyProperty DelItemProperty =
            DependencyProperty.Register(
                "DelItem",
                typeof(ICommand),
                typeof(ItemInCart),
                new UIPropertyMetadata(null));
        public ICommand DelItem
        {
            get { return (ICommand)GetValue(DelItemProperty); }
            set { SetValue(DelItemProperty, value); }
        }

        #endregion

        #region ChangeQTY
        public static readonly DependencyProperty ChangeQTYProperty =
            DependencyProperty.Register(
                "ChangeQTY",
                typeof(ICommand),
                typeof(ItemInCart),
                new UIPropertyMetadata(null));
        public ICommand ChangeQTY
        {
            get { return (ICommand)GetValue(ChangeQTYProperty); }
            set { SetValue(ChangeQTYProperty, value); }
        }

        #endregion

        public ItemInCart()
        {
            InitializeComponent();
        }
    }
}
