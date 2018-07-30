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
    /// Interaction logic for LoadTemplateName.xaml
    /// </summary>
    public partial class LoadTemplateName : Window
    {

        Order order = null;
        bool bMerge;
        ObservableCollection<ListBoxItem> myCollection;
        private ListBoxItem m_SelectedOrder = null;
        public ListBoxItem SelectedOrder
        {
            get { return m_SelectedOrder; }
            set
            {
                // Some logic here
                //if (_mySelectedItem == null || _mySelectedItem.Tag != value.Tag)
                {
                    m_SelectedOrder = value;

                }
            }
        }

        public LoadTemplateName()
        {

        }
        public LoadTemplateName(int userID)
        {
            InitializeComponent();
            myCollection = new ObservableCollection<ListBoxItem>(GetAllTemlateByUserID(userID));
            CheckIfTemplatesExist(true);

            lstbxTemplatNames.ItemsSource = myCollection;
            DataContext = this;
        }

        private void CheckIfTemplatesExist(bool prompt = false)
        {
            if (myCollection.Count == 0)
            {
                if (prompt)
                {
                    OneButtonScreen.ShowMessage("אין רשימות להציג", "");
                }
                
                DialogResult = false;
                Close();
            }
        }

        private List<ListBoxItem> GetAllTemlateByUserID(int userID)
        {
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TheLearningAgent";
            dirPath += @"\Orders\" + userID + @"\Templates";

            DirectoryInfo d = new DirectoryInfo(dirPath);

            List<ListBoxItem> ans = new List<ListBoxItem>();

            try
            {
                //foreach (var file in d.GetFiles("*.xml"))
                foreach (var file in d.GetFiles("*.json"))
                {
                    string templateName = file.Name.Substring(17, file.Name.Length - 17 - 5);
                    var lbi = new ListBoxItem();
                    lbi.Tag = file.FullName;
                    lbi.Content = templateName;
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (m_SelectedOrder == null)
            {
                return;
            }
            try
            {
                System.IO.File.Delete((string)m_SelectedOrder.Tag);
                myCollection.Remove(m_SelectedOrder);
                CheckIfTemplatesExist();
            }
            catch (Exception exception)
            {
                Console.WriteLine("{0} Exception caught.", exception);
            }
        }
    }
}
