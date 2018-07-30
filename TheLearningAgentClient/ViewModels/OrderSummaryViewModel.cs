using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheLearningAgentClient.Models;

namespace TheLearningAgentClient.ViewModels
{
    internal class OrderSummaryViewModel
    {
        public OrderSummaryModel OrderSummary { get; set; }
        public OrderSummaryViewModel()
        {
            OrderSummary = new OrderSummaryModel();
            OrderSummary.Lines = new ObservableCollection<OrderLineForSummaryModel>();
        }

        public void SetUser(user i_user)
        {
            OrderSummary.CustomerNameLine = i_user.ToString();
        }

        public void SetOrder(Order i_order)
        {
            OrderSummary.OrderNumberLine = i_order.GetOrderId();
            OrderSummary.ItemsCountLine = i_order.GetItemsCount();
            OrderSummary.TotalLine = i_order.Total;

            foreach (var item in i_order.Cart)
            {
                OrderLineForSummaryModel olfsm = new OrderLineForSummaryModel
                {
                    Amount = (item.m_qty * (double)item.m_product.price),
                    Price = ((double)(item.m_product.price)),
                    ProductID = ((item.m_product.product_id)).ToString(),
                    ProductName = (item.m_product.product_name),
                    Weighable = (bool)(item.m_product.weighable),
                    Quantity = item.m_qty
                };

                OrderSummary.Lines.Add(olfsm);
            }
        }    
    }
}