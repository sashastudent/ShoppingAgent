using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLearningAgentClient.Models
{
    public class ItemPreviewModel : INotifyPropertyChanged
    {
        private int productId;
        private string productName;
        private double productPrice;
        private string productPhotoPath;
        private bool weighable;
        private bool recommended;

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

        public bool Recommended
        {
            get
            {
                return recommended;
            }

            set
            {
                if (recommended != value)
                {
                    recommended = value;
                    RaisePropertyChanged("Recommended");
                }
            }
        }

        public string ProductPhotoPath
        {
            get
            {
                return productPhotoPath;
            }

            set
            {
                if (productPhotoPath != value)
                {
                    productPhotoPath = value;
                    RaisePropertyChanged("ProductPhotoPath");
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
