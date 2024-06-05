using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorVP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement el in GroupButton.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += ButtonClick;
                }
            }
        }
        public double memoryValue = 0;

        public TextBox TextBox => text;

        private void ButtonClick(Object sender, RoutedEventArgs e)
        {
            try
            {
                string textButton = ((Button)e.OriginalSource).Content.ToString();
                if (textButton == "C")
                {
                    text.Clear();
                    memoryValue = 0;
                }
                else if (textButton == "x")
                {
                    if (text.Text.Length > 0)
                    {
                        text.Text = text.Text.Substring(0, text.Text.Length - 1);
                    }
                }
                else if (textButton == "=")
                {
                    string expression = text.Text.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                    object result = new DataTable().Compute(expression, null);
                    text.Text = result.ToString().Replace(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator, ",");
                }

                else if (textButton == "MR")
                {
                    text.Text = memoryValue.ToString();
                }
                else if (textButton == "M+")
                {
                    memoryValue += double.Parse(text.Text);
                    text.Clear();
                }
                else if (textButton == "MC")
                {
                    memoryValue = 0;
                }
                else if (textButton == "%")
                {
                    if (text.Text.Contains("%"))
                    {
                        string[] values = text.Text.Split('%');
                        double value1 = double.Parse(values[0]);
                        double value2 = double.Parse(values[1]);
                        text.Text = (value1 % value2).ToString();
                    }
                    else
                    {
                        text.Text += "%";
                    }
                }

                else
                {
                    text.Text += textButton;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
