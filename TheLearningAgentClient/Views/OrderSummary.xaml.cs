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
using TheLearningAgentClient.Models;
using TheLearningAgentClient.ViewModels;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for OrderSummaryxaml.xaml
    /// </summary>
    public partial class OrderSummary : Window
    {
        public OrderSummary(Order order, user user)
        {
            InitializeComponent();
            OrderSummaryViewModel osvm = new OrderSummaryViewModel();
            osvm.SetUser(user);
            osvm.SetOrder(order);
            this.DataContext = osvm;
        }
    }
}
