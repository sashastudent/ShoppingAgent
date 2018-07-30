using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TheLearningAgentClient.Views
{
    /// <summary>
    /// Interaction logic for SaveTemplateName.xaml
    /// </summary>
    public partial class CaptureQTY : Window
    {
        bool m_Weighable;
        double m_Value;
        double m_InitValue;
        string m_PrevString;
        int m_PrevCaretIndex;

        bool ignoreCaret;
        public CaptureQTY(bool weighable, double qty)
        {
            InitializeComponent();
            m_Weighable = weighable;
            m_Value = 0;
            m_InitValue = qty;
            
            if (m_Weighable)
            {
                Title.Content = "הכנס משקל";
            }
            else
            {
                Title.Content = "הכנס כמות";
            }

            ResetTextBox(qty);
        }

        private void ResetTextBox(double qty)
        {
            if (m_InitValue != 0)
            {
                if (m_Weighable)
                {
                    txtQTY.Text = qty.ToString("0.000");
                }
                else
                {
                    txtQTY.Text = qty.ToString("0");
                }
                txtQTY.Focus();
                txtQTY.SelectAll();
            }
            else
            {
                txtQTY.Focus();
                m_PrevString = "";
            }
                

        }

        internal double GetValue()
        {
            return m_Value;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void txtTemplatName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Submit();
            }
        }
        private void Submit()
        {
            if (m_Value == 0)
            {
                return;
            }
            DialogResult = true;
            Close();
        }

        private bool IsTextAllowed(string text)
        {
            if (text == "")
            {
                m_Value = 0;
                return false;
            }
            if (m_Weighable)
            {
                try
                {
                    //Double.Parse(text+"0");
                    m_Value = Double.Parse(Double.Parse(text).ToString("0.000"));
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                return false;
                }
            }
            else
            {
                try
                {
                    //Int32.Parse(text+"0");
                    m_Value = Int32.Parse(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                
                return false;
                }
            }
            return true;
        }
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
          /*  string StrToCheck = (sender as TextBox).Text + e.Text;
              e.Handled = !IsTextAllowed(StrToCheck); */
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            /*if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }*/
        }

        private void txtQTY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsTextAllowed((sender as TextBox).Text) && (sender as TextBox).Text !="")
            {
                ignoreCaret = true;
                (sender as TextBox).Text = m_PrevString;
                ignoreCaret = false;
                (sender as TextBox).CaretIndex = m_PrevCaretIndex;
            }
            else
            {
                m_PrevString = (sender as TextBox).Text;
                if (!ignoreCaret)
                {
                    m_PrevCaretIndex = (sender as TextBox).CaretIndex;
                }
                
            }
        }
    }
}
