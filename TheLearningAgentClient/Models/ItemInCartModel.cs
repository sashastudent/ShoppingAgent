using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLearningAgentClient.Models
{
    public class ItemInCartModel : INotifyPropertyChanged
    {
        private int productId;
        private string productName;
        private double productPrice;
        private double quantity;
        private double amount;
        private bool weighable;

        public int ProductId
        {
            get
            {
                return productId;
            }

            set
            {
                if (productId != value)
                {
                    productId = value;
                    RaisePropertyChanged("ProductId");
                }
            }
        }
        
        public string ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                if (productName != value)
                {
                    productName = value;
                    RaisePropertyChanged("ProductName");
                }
            }
        }

        public bool Weighable
        {
            get
            {
                return weighable;
            }

            set
            {
                if (weighable != value)
                {
                    weighable = value;
                    RaisePropertyChanged("Weighable");
                }
            }
        }

        public double ProductPrice
        {
            get
            {
                return productPrice;
            }

            set
            {
                if (productPrice != value)
                {
                    productPrice = value;
                    RaisePropertyChanged("ProductPrice");
                }
            }
        }

        public double Quantity
        {  
            get
            {
                return quantity;
            }

            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged("Quantity");

                    amount = quantity * productPrice;
                    RaisePropertyChanged("Amount");
                }
            }
        }

        public double Amount
        {
            get
            {
                return amount;
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
