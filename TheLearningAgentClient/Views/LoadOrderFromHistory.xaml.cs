using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using TheLearningAgentClient.Models;

namespace TheLearningAgentClient.Views
{



    /// <summary>
    /// Interaction logic for LoadOrderFromHistory.xaml
    /// </summary>
    public partial class LoadOrderFromHistory : Window
    {

        Order order = null;
        user m_User;
        bool bMerge;
        ObservableCollection<ListBoxItem> myCollection;
        private ListBoxItem m_SelectedOrder = null;
        public ListBoxItem SelectedOrder
        {
            get { return m_SelectedOrder; }
            set
            {
                m_SelectedOrder = value;
            }
        }

        public LoadOrderFromHistory()
        {

        }
        public LoadOrderFromHistory(user User)
        {
            InitializeComponent();
            m_User = User;
            myCollection = new ObservableCollection<ListBoxItem>(GetAllTemlateByUserID(User.user_id));

            if (myCollection.Count == 0)
            {
                OneButtonScreen.ShowMessage("אין הסטורית קניות להציג", "");
                DialogResult = false;
                Close();
            }

            lstbxTemplatNames.ItemsSource = myCollection;
            DataContext = this;
        }

        private List<ListBoxItem> GetAllTemlateByUserID(int userID)
        {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TheLearningAgent";
            dirPath += @"\Orders\" + userID + @"\Submitted";

            DirectoryInfo d = new DirectoryInfo(dirPath);

            List<ListBoxItem> ans = new List<ListBoxItem>();

            try
            {
                //foreach (var file in d.GetFiles("*.xml"))
                foreach (var file in d.GetFiles("*.json"))
                {
                    var lbi = new ListBoxItem();
                    lbi.Tag = file.FullName;
                    lbi.Content = file.CreationTime.ToString("dd/MM/yy hh:mm:ss");
                    ans.Add(lbi);
                }
            }
            catch (Exception)
            {
            }
            

            return ans;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnMerge_Click(object sender, RoutedEventArgs e)
        {
            if (m_SelectedOrder == null)
            {
                return;
            }

            bMerge = true;

            //order = Order.FromXmlFile((string)_mySelectedItem.Tag);
            order = Order.FromJsonFile((string)m_SelectedOrder.Tag);
            DialogResult = true;
            Close();
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            if (m_SelectedOrder == null)
            {
                return;
            }

            bMerge = false;

            //order = Order.FromXmlFile((string)_mySelectedItem.Tag);
            order = Order.FromJsonFile((string)m_SelectedOrder.Tag);
            DialogResult = true;
            Close();
        }

        internal Order GetSelectedOrder()
        {
            return order;
        }

        internal bool Merge()
        {
            return bMerge;
        }

        private void btnReceipt_Click(object sender, RoutedEventArgs e)
        {
            if (m_SelectedOrder == null)
            {
                return;
            }

            //OrderSummary os = new OrderSummary(Order.FromXmlFile((string)_mySelectedItem.Tag), m_User);
            OrderSummary os = new OrderSummary(Order.FromJsonFile((string)m_SelectedOrder.Tag), m_User);

            os.ShowDialog();

        }
    }
}
