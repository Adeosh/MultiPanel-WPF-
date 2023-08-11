using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MultiPanel_WPF_.MVVM.View
{
    public partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();

            foreach (UIElement elem in MainRoot.Children)
            {
                if (elem is Button)
                {
                    ((Button)elem).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)((Button)e.OriginalSource).Content;

            if (str == "AC")
            {
                textLabel.Text = "";
            }
            else if (str == "=")
            {
                string value = new DataTable().Compute(textLabel.Text, null).ToString();
                textLabel.Text = value;
            }
            else
            {
                textLabel.Text += str;
            }

        }
    }
}
