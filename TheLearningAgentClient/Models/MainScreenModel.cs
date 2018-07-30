using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheLearningAgentClient.Models
{
    public class IngredientToFilter : INotifyPropertyChanged
    {
        private bool isChecked;
        private string description;
        private int ingredientID;

        public int IngredientID
        {
            get
            {
                return ingredientID;
            }

            set
            {
                if (ingredientID != value)
                {
                    ingredientID = value;
                    RaisePropertyChanged("IngredientID");
                }
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }

            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    RaisePropertyChanged("IsChecked");
                }
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class MainScreenModel : INotifyPropertyChanged
    {
        private user m_User;
        public Order m_Order;
        private double m_Total;
        private int m_Count;
        private string m_GreetingName;

        public ObservableCollection<IngredientToFilter> IngredientsToFilter { get; set; }
        public ObservableCollection<ComboBoxItem> Departments { get; set; }
        public ObservableCollection<ItemPreviewModel> ItemsForPreview { get; set; }
        public ObservableCollection<ItemInCartModel> ItemsInCart { get; set; }
        public List<product> RecomedationsFromAgent { get; set; }
        public List<int> IngregientsToFilterOut { get; set; }

        public MainScreenModel(user i_ueser)
        {
            User = i_ueser;
            m_Order = new Order(m_User.user_id);
            IngregientsToFilterOut = new List<int>();

            m_Total = 0;
            m_Count = 0;
        }

        public user User
        {
            get { return m_User; }
            set
            {
                m_User = value;
                GreetingName = m_User.GetFullName();
            }
        }

        public Order Order
        {
            get { return m_Order; }
            set
            {
                m_Order = value;
            }
        }

        public string GreetingName
        {
            get
            {
                return "שלום " + m_GreetingName;
            }

            set
            {
                if (m_GreetingName != value)
                {
                    m_GreetingName = value;
                    RaisePropertyChanged("GreetingName");
                }
            }
        }

        public double Total
        {
            get
            {
                return m_Total;
            }

            set
            {
                if (m_Total != value)
                {
                    m_Total = value;
                    RaisePropertyChanged("Total");
                }
            }
        }

        public int Count
        {
            get
            {
                return m_Count;
            }

            set
            {
                if (m_Count != value)
                {
                    m_Count = + value;
                    RaisePropertyChanged("Count");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
