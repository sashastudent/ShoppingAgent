using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheLearningAgentClient.Models;
using TheLearningAgentClient.Views;

namespace TheLearningAgentClient.ViewModels
{
    public class MainScreenViewModel : INotifyPropertyChanged
    {
        public MainScreenModel MainScreen { get; set; }

        private MainWindow m_MainWindow;
        private ScrollViewer m_CartScroller;
        private int m_CurrentDep;
        private bool m_Searching;

        #region BindingLogic
        private string m_SearchString;
        public string SearchString
        {
            get { return m_SearchString; }
            set
            {
                if (m_SearchString != value)
                {
                    bool refresh = false;
                    if (m_Searching && m_SearchString != null && m_SearchString.Length > 2 && value.Length <= 2)
                    {
                        refresh = true;
                    }
                    m_SearchString = value;
                    RaisePropertyChanged("SearchString");

                    if (m_SearchString.Length > 2)
                    {
                        SearchForProducts();
                    }
                    else if (refresh)
                    {
                        m_Searching = false;
                        RefreshItemsForPreview();
                    }
                }
            }
        }

        private ComboBoxItem m_SelectedDepartment;
        public ComboBoxItem SelectedDepartment
        {
            get { return m_SelectedDepartment; }
            set
            {
                if (m_SelectedDepartment == null || m_SelectedDepartment.Tag != value.Tag)
                {
                    if ((int)value.Tag >= 0)
                    {
                        LoadItemsForPreviewByDepID((int)value.Tag);
                    }

                    m_SelectedDepartment = value;
                }
            }
        }
        #endregion

        #region CallBacksDefenition
        public MyICommand LogOffCommand { get; set; }
        public MyICommand EditPersonalInformationCommand { get; set; }
        public MyICommand PayAndCloseOrderCommand { get; set; }
        public MyICommand CancelOrderCommand { get; set; }
        public MyICommand SaveOrderCommand { get; set; }
        public MyICommand LoadOrderCommand { get; set; }
        public MyICommand LoadPrevOrderCommand { get; set; }
        public MyICommand DeleteItemFromCartCommand { get; set; }
        public MyICommand ChangeQtyRequestCommand { get; set; }
        public MyICommand IngredientCheckedChangedCommand { get; set; }
        public MyICommand ItemSelectedFromCatalogCommand { get; set; }
        public MyICommand AddingAllRecomendationsFromAgentCommand { get; set; }
        #endregion

        public MainScreenViewModel()
        {

        }
        public MainScreenViewModel(MainWindow mainWindow, user i_user, ScrollViewer scroller)
        {
            MainScreen = new MainScreenModel(i_user);

            m_MainWindow = mainWindow;
            m_CartScroller = scroller;
            m_CurrentDep = 0;
            m_Searching = false;

            RefreshItemsForPreview();
            LoadIngredientsToFilter();
            LoadDepartments();
            LoadOrder(MainScreen.Order);

            #region CallBacksInit
            LogOffCommand = new MyICommand(OnLogOff);
            EditPersonalInformationCommand = new MyICommand(OnEditPersonalInformation);
            PayAndCloseOrderCommand = new MyICommand(OnPayAndCloseOrder);
            CancelOrderCommand = new MyICommand(OnCancelOrder);
            SaveOrderCommand = new MyICommand(OnSaveOrder);
            LoadOrderCommand = new MyICommand(OnLoadOrder);
            LoadPrevOrderCommand = new MyICommand(OnLoadPrevOrder);
            DeleteItemFromCartCommand = new MyICommand(OnDeleteItemFromCart);
            ChangeQtyRequestCommand = new MyICommand(OnChangeQtyRequest);
            IngredientCheckedChangedCommand = new MyICommand(OnIngredientCheckedChanged);
            ItemSelectedFromCatalogCommand = new MyICommand(OnItemSelectedFromCatalog);
            AddingAllRecomendationsFromAgentCommand = new MyICommand(OnAddingAllRecomendationsFromAgent);
            #endregion
        }

        private void SearchForProducts()
        {
            List<product> list = DataBaseManager.GetProductsLike(m_SearchString, MainScreen.IngregientsToFilterOut);
            LoadItemsForPreview(list);
            m_Searching = true;

        }

        private void RefreshItemsForPreview()
        {
            if (m_Searching)
            {
                SearchForProducts();
            }
            else if (m_CurrentDep >= 0)
            {
                LoadItemsForPreviewByDepID(m_CurrentDep);
            }
        }

        public void LoadItemsForPreviewByDepID(int dep_id)
        {
            m_Searching = false;
            SearchString = "";
            if (MainScreen.ItemsForPreview == null)
            {
                MainScreen.ItemsForPreview = new ObservableCollection<ItemPreviewModel>();
            }
            else
            {
                MainScreen.ItemsForPreview.Clear();
            }

            List<product> list;

            if (dep_id == 0 )
            {
                GetAgentRecommendations(null);
                list = MainScreen.RecomedationsFromAgent;
            }
            else
            {
                list = DataBaseManager.GetAllProductsByCategoryAndComponents(dep_id, MainScreen.IngregientsToFilterOut);
            }

            foreach (var item in list)
            {
                string PhotoPath = AppDomain.CurrentDomain.BaseDirectory + @"\Photos\Pruducts\" + item.product_id + ".jpg";
                if (!File.Exists(PhotoPath))
                {
                    PhotoPath = AppDomain.CurrentDomain.BaseDirectory + @"\Photos\Pruducts\0.jpg";
                }
                MainScreen.ItemsForPreview.Add(new ItemPreviewModel { Recommended = IsRecommended(item), Weighable = (bool)item.weighable, ProductId = item.product_id, ProductName = item.product_name, ProductPhotoPath = PhotoPath, ProductPrice = ((double)item.price) });
            }

            m_CurrentDep = dep_id;

        }

        public void LoadIngredientsToFilter()
        {
            if (MainScreen.IngredientsToFilter == null)
            {
                MainScreen.IngredientsToFilter = new ObservableCollection<IngredientToFilter>();
            }
            else
            {
                MainScreen.IngredientsToFilter.Clear();
            }

            List<product_component> list = DataBaseManager.GetComponentsList();

            foreach (var item in list)
            {
                MainScreen.IngredientsToFilter.Add(new IngredientToFilter { Description = item.component_name, IsChecked = false, IngredientID = item.component_id });
            }
        }

        public void LoadDepartments()
        {
            

            MainScreen.Departments = GetListOfDepartments();

 
        }

        public void LoadItemsForPreview(List<product> list)
        {
            if (MainScreen.ItemsForPreview == null)
            {
                MainScreen.ItemsForPreview = new ObservableCollection<ItemPreviewModel>();
            }
            else
            {
                MainScreen.ItemsForPreview.Clear();
            }

            foreach (var item in list)
            {
                string PhotoPath = AppDomain.CurrentDomain.BaseDirectory + @"\Photos\Pruducts\" + item.product_id + ".jpg";
                if (!File.Exists(PhotoPath))
                {
                    PhotoPath = AppDomain.CurrentDomain.BaseDirectory + @"\Photos\Pruducts\0.jpg";
                }
                MainScreen.ItemsForPreview.Add(new ItemPreviewModel {Recommended = IsRecommended(item),  Weighable = (bool)item.weighable, ProductId = item.product_id, ProductName = item.product_name, ProductPhotoPath = PhotoPath, ProductPrice = ((double)item.price) });
            }
        }
         private bool IsRecommended(product p)
        {
            return (null!=MainScreen.RecomedationsFromAgent.Find(x => x.product_id == p.product_id));
        }

        public void LoadOrder(Order order, bool ScrollToEnd = false, int ScrollToIndex = -1)
        {
            if (MainScreen.ItemsInCart == null)
            {
                MainScreen.ItemsInCart = new ObservableCollection<ItemInCartModel>();
            }
            else
            {
                MainScreen.ItemsInCart.Clear();
            }

            foreach (var item in order.Cart)
            {
                AddItemToGuiCart(item);
            }

            MainScreen.Total = order.Total;
            MainScreen.Count = order.Cart.Count;

            //var point = item.TranslatePoint(new Point() - (Vector)e.GetPosition(sv), ip);
            //sv.ScrollToVerticalOffset(point.Y + (item.ActualHeight / 2));


            if (null != m_CartScroller)
            {
                if (ScrollToEnd)
                {
                    m_CartScroller.ScrollToEnd();
                }
                else if (ScrollToIndex != -1)
                {
                    ContentPresenter itemcontainer = ((ItemsControl)m_CartScroller.Content).ItemContainerGenerator.ContainerFromIndex(ScrollToIndex) as ContentPresenter;
                    if (itemcontainer != null)
                    {
                        itemcontainer.BringIntoView();
                    }
                }
            }
        }

        private void AddItemToGuiCart(OrderRecord item)
        {
            MainScreen.ItemsInCart.Add(new ItemInCartModel { Weighable = (bool)item.m_product.weighable, ProductId = item.m_product.product_id, ProductName = item.m_product.product_name, ProductPrice = (double)item.m_product.price , Quantity = item.m_qty });
        }

        private bool AreThereItemInOrder()
        {
            return MainScreen.Count > 0;
        }

        internal ObservableCollection<ComboBoxItem> GetListOfDepartments()
        {
            var Departments = DataBaseManager.GetAllDepartments();
            ObservableCollection<ComboBoxItem> ans = new ObservableCollection<ComboBoxItem>();

            ComboBoxItem agentoxItem = new ComboBoxItem();
            agentoxItem.Tag = 0;
            agentoxItem.Content = "מומלצים עבורך";
            agentoxItem.Background = Brushes.Yellow;
            agentoxItem.FontWeight = FontWeights.Bold;
            ans.Add(agentoxItem);

            foreach (var item in Departments)
            {
                var cbi = new ComboBoxItem();
                cbi.Tag = item.category_id;
                cbi.Content = item.category_name;
                ans.Add(cbi);
            }
            return ans;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #region CallbacksImplementation

        private void OnLogOff(object parameter)
        {
            if (CaptureYesNo.AskYesNoQuestion("ניתוק", "האם אתה בטוח שברצונך להתנתק?"))
                m_MainWindow.GoToPrevPage();
        }

        private void OnEditPersonalInformation(object parameter)
        {
            m_MainWindow.SetPage(new EditPersonalDetails(m_MainWindow, MainScreen.User));
        }

        private void OnPayAndCloseOrder(object parameter)
        {
            try
            {
                MainScreen.Order.SubmitOrder();
                List<string> lines = MainScreen.Order.ToPlainText();
                OrderSummary orderSummary = new OrderSummary(MainScreen.Order, MainScreen.User);
                orderSummary.Show();
                MainScreen.Order = new Order(MainScreen.User.user_id);
                LoadOrder(MainScreen.Order);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private void OnCancelOrder(object parameter)
        {
            try
            {
                if (CaptureYesNo.AskYesNoQuestion("ביטול עסקה", "האם אתה בטוח שברצונך לבטל את העסקה?"))
                {
                    MainScreen.Order = new Order(MainScreen.User.user_id);
                    LoadOrder(MainScreen.Order);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private void OnSaveOrder(object parameter)
        {
            try
            {
                SaveTemplateName ctn = new SaveTemplateName();
                if (true == ctn.ShowDialog())
                {
                    MainScreen.Order.SaveOrderAsTemplate(ctn.TemplateName());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private void OnLoadOrder(object parameter)
        {
            try
            {
                LoadTemplateName ltn = new LoadTemplateName(MainScreen.User.user_id);
                if (true == ltn.ShowDialog())
                {
                    if (ltn.Merge())
                    {
                        MainScreen.Order.AddToCart(ltn.GetSelectedOrder());
                    }
                    else
                    {
                        MainScreen.Order = ltn.GetSelectedOrder();
                    }
                    LoadOrder(MainScreen.Order, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private void OnLoadPrevOrder(object parameter)
        {
            try
            {
                LoadOrderFromHistory lofh = new LoadOrderFromHistory(MainScreen.User);
                if (true == lofh.ShowDialog())
                {
                    if (lofh.Merge())
                    {
                        MainScreen.Order.AddToCart(lofh.GetSelectedOrder());
                    }
                    else
                    {
                        MainScreen.Order = lofh.GetSelectedOrder();
                    }
                    LoadOrder(MainScreen.Order, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

        }

        private void GetAgentRecommendations(object parameter)
        {
            if (MainScreen.RecomedationsFromAgent == null)
            {
                int NumberOfReciepts = DataBaseManager.GetNumberOfReciepts(MainScreen.User.user_id);
                if (NumberOfReciepts >= 3)
                {
                    MainScreen.RecomedationsFromAgent = DataBaseManager.GetRecomendationsFromPersonalSmartAgent(MainScreen.User.user_id);
                }
                else
                {
                    MainScreen.RecomedationsFromAgent = DataBaseManager.GetRecomendationsFromGeneralSmartAgent(MainScreen.User.user_id);
                }
            }

            if (MainScreen.RecomedationsFromAgent == null)
            {
                MainScreen.RecomedationsFromAgent = new List<product>();
            }
        }

        private void OnDeleteItemFromCart(object parameter)
        {
            int productIDAdded = (int)parameter;
            product p = DataBaseManager.GetProductById(productIDAdded);

            MainScreen.Order.AddToCart(p, 0, true);
            LoadOrder(MainScreen.Order);
        }

        private void OnChangeQtyRequest(object parameter)
        {
            int productIDAdded = (int)parameter;
            product p = DataBaseManager.GetProductById(productIDAdded);

            double qty = 1;

            CaptureQTY cqty = new CaptureQTY((bool)p.weighable, MainScreen.Order.GetQtyByProduct(p));
            if (true == cqty.ShowDialog())
                qty = cqty.GetValue();
            else
                return;

            MainScreen.Order.AddToCart(p, qty, true);
            LoadOrder(MainScreen.Order, false, MainScreen.Order.IndexOfProduct(p));
        }

        private void OnIngredientCheckedChanged(object parameter)
        {
            int ingredientID = (int)(((CheckBox)parameter).Tag);
            bool isChecked = (bool)((CheckBox)parameter).IsChecked;
            if (isChecked)
            {
                MainScreen.IngregientsToFilterOut.Add(ingredientID);
            }
            else
            {
                MainScreen.IngregientsToFilterOut.Remove(ingredientID);
            }

            if (m_Searching || m_CurrentDep > 0)
            {
                RefreshItemsForPreview();
            }
        }

        private void OnItemSelectedFromCatalog(object parameter)
        {
            int productIDAdded = (int)parameter;
            product p = DataBaseManager.GetProductById(productIDAdded);

            double qty = 1;
            if ((bool)p.weighable)
            {
                CaptureQTY cqty = new CaptureQTY((bool)p.weighable, 0);
                if (true == cqty.ShowDialog())
                    qty = cqty.GetValue();
                else
                    return;
            }

            MainScreen.Order.AddToCart(p, qty, false);
            LoadOrder(MainScreen.Order, false, MainScreen.Order.IndexOfProduct(p));
        }

        private void OnAddingAllRecomendationsFromAgent(object parameter)
        {
            bool found = false;
            
            foreach (var item in MainScreen.ItemsForPreview)
            {
                if (item.Recommended)
                {
                    if (!found && CaptureYesNo.AskYesNoQuestion("המלצות הסוכן", "האם אתה בטוח שברצונך להוסיף את כל הפריטים המוצעים?") == false)
                    {
                        return;
                    }
                    found = true;
                    product p = DataBaseManager.GetProductById(item.ProductId);
                    MainScreen.Order.AddToCart(p, 1, false);
                    LoadOrder(MainScreen.Order, false, MainScreen.Order.IndexOfProduct(p));
                }   
            }
            if (found)
            {
                OneButtonScreen.ShowMessage("הפריטים נוספו", "אנא ערוך את כמות/משקל הפריטים המוצעים שנוספו לרשימת הקניות");
            }
            else
            {
                OneButtonScreen.ShowMessage("לא נמצאו פריטים מומלצים", "");
            }
        }
        #endregion
    }
}
