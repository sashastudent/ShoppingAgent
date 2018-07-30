using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TheLearningAgentClient.Models
{
    public class OrderSummaryModel
    {
        public ObservableCollection<OrderLineForSummaryModel> Lines { get; set; }

        public string CustomerNameLine { get; internal set; }
        public int OrderNumberLine { get; internal set; }
        public int ItemsCountLine { get; internal set; }
        public double TotalLine { get; internal set; }

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