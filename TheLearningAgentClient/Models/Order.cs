
using System.Collections.Generic;

using System.Xml.Serialization;

using System.Xml;
using System.IO;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TheLearningAgentClient.Models
{
    public class OrderRecord : INotifyPropertyChanged
    {
        public product m_product;

        public double m_qty;

        public product Product
        {
            get { return m_product; }

            set
            {
                if (m_product != value)
                {
                    m_product = value;
                    RaisePropertyChanged("Product");
                }
            }
        }

        public double QTY
        {
            get { return m_qty; }

            set
            {
                if (m_qty != value)
                {
                    m_qty = value;
                    RaisePropertyChanged("QTY");
                }
            }
        }


        public OrderRecord(product i_product, double i_qty)
        {
            m_product = i_product;
            m_qty = i_qty;
        }
        public OrderRecord()
        {

        }

        public product GetProduct()
        {
            return m_product;
        }

        public override string ToString()
        {
            return
                this.m_product.product_name + "\n" +
                "    "+this.m_qty+" X "+ ((double)(this.m_product.price)).ToString("0.00") + " = " + ((double)(this.m_qty * (double)this.m_product.price)).ToString("0.00");
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


    public class Order : INotifyPropertyChanged
    {
        public enum OrderType { Template, Regular };
        public enum OrderStatus { New, Open, Close };

        private OrderType m_OrderType;
        private OrderStatus m_OrderStatus;
        private double m_Total;
        private int m_userId;
        private int m_OrderId;
        public ObservableCollection<OrderRecord> Cart { get; set; }

        public OrderType OrderType1 { get => m_OrderType; set => m_OrderType = value; }
        public OrderStatus OrderStatus1 { get => m_OrderStatus; set => m_OrderStatus = value; }
        public double Total { get => m_Total; set => m_Total = value; }
        public int UserId { get => m_userId; set => m_userId = value; }



        
        public Order(int i_userId)
        {
            Cart = new ObservableCollection<OrderRecord>();
            Total = 0;
            OrderStatus1 = OrderStatus.New;
            OrderType1 = OrderType.Regular;
            UserId = i_userId;
        }

        public Order()
        {
            Cart = new ObservableCollection<OrderRecord>();
            Total = 0;
            OrderStatus1 = OrderStatus.New;
            OrderType1 = OrderType.Regular;
        }

        public int IndexOfProduct(product p)
        {
            foreach (var item in Cart)
            {
                if (item.m_product.product_id == p.product_id)
                {
                    return Cart.IndexOf(item);
                }
            }
            return -1;
        }


        public void AddToCart(product p, double count, bool overrideQty)
        {
            bool found = false;

            double Diff = 0;

            foreach (var item in Cart)
            {
                if (item.GetProduct().product_id == p.product_id)
                {
                    found = true;
                    if (overrideQty)
                    {
                        Diff = (count - item.m_qty);
                        item.m_qty = count;
                        if (count == 0)
                        {
                            Cart.Remove(item);
                        }
                    }
                    else
                    {
                        Diff = count;
                        item.m_qty += count;
                        if (item.m_qty == 0)
                        {
                            Cart.Remove(item);
                        }
                    }
                    
                    break;
                }
            }
            if (!found)
            {
                Cart.Add(new OrderRecord(p, count));
                Diff = count;
            }

            Total += (double)p.price * Diff;
        }

        internal void AddToCart(Order order)
        {
            foreach (OrderRecord item in order.Cart)
            {
                AddToCart(item.GetProduct(), item.m_qty,false);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Order FromJsonFile(string xmlfile)
        {
            return (Order)JsonConvert.DeserializeObject<Order>(File.ReadAllText(xmlfile));
        }
        public string ToXml()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Order));
            var xml = "";

            using (var sww = new System.IO.StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, this);
                    xml = sww.ToString(); // Your XML
                }
            }

            return xml;
        }

        public static Order FromXmlFile(string xmlfile)
        {
            XmlSerializer s = new XmlSerializer(typeof(Order));

            TextReader r = new StreamReader(xmlfile);

            Order o = (Order)s.Deserialize(r);
            r.Close();
            return o;
        }

        public void SubmitOrder()
        {
            m_OrderId =  DataBaseManager.SubmitOrder(this);

            //string xml = ToXml();
            string json = ToJson();
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TheLearningAgent";
            filePath += @"\Orders\" + UserId + "\\Submitted\\" + DateTime.Now.ToString("dd-MM-yy-hh-mm-ss").Replace("/","_").Replace(" ", "_").Replace(":", "") + ".json";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            System.IO.File.WriteAllText(filePath, json);
        }

        public void SaveOrderAsTemplate(string name)
        {
            m_OrderType = OrderType.Template;
            
            //string xml = ToXml();
            string json = ToJson();
            m_OrderType = OrderType.Regular;
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TheLearningAgent";
            filePath += @"\Orders\" + UserId + "\\Templates\\" + DateTime.Now.ToString("dd-MM-yy-hh-mm-ss").Replace("/", "_").Replace(" ", "_").Replace(":", "") + name+".json";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            System.IO.File.WriteAllText(filePath, json);
        }


        

        public List<string> ToPlainText()
        {
            List<string> ans =new List<string>();

            user u = DataBaseManager.GetUser(this.m_userId);

            if(u==null)
            {
                return ans;
            }

            ans.Add("Customer Name: "+u.ToString());
            ans.Add("Order Number: " + this.m_OrderId);
            ans.Add("Items Count: "+ GetItemsCount());
            ans.Add("");
            ans.Add("");
            foreach (var item in this.Cart)
            {
                ans.Add(item.ToString());
                ans.Add("");
            }
            ans.Add("");
            ans.Add("Total: " + this.m_Total);

            return ans;
        }

        internal int GetItemsCount()
        {
            int ans = 0;
            foreach (var item in Cart)
            {
                ans++;
            }
            return ans;
        }

        internal int GetOrderId()
        {
            return m_OrderId;
        }

        internal double GetQtyByProduct(product p)
        {
            foreach (var item in Cart)
            {
                if (item.GetProduct().product_id == p.product_id)
                {
                    return item.m_qty;
                }
            }
            return 0;
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
