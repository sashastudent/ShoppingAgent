using System.ComponentModel;

namespace TheLearningAgentClient.Models
{
    public class OrderLineForSummaryModel
    {
        private string productName;
        private string productID;
        private double price;
        private double quantity;
        private double amount;
        private bool weighable;

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

        public string ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                if (productID != value)
                {
                    productID = value;
                    RaisePropertyChanged("ProductID");
                }
            }
        }
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (price != value)
                {
                    price = value;
                    RaisePropertyChanged("Price");
                }
            }
        }
        
        public double Amount
        {
            get
            {
                return amount;
            }

            set
            {
                if (amount != value)
                {
                    amount = value;
                    RaisePropertyChanged("Amount");
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