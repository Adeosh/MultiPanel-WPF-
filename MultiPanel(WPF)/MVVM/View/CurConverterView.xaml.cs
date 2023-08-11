using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultiPanel_WPF_.MVVM.View
{
    public partial class CurConverterView : UserControl
    {
        public CurConverterView()
        {
            InitializeComponent();
            BindCurrency();
        }

        private void BindCurrency() 
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Text");
            dataTable.Columns.Add("Value");

            dataTable.Rows.Add("Выбор", 0);
            dataTable.Rows.Add("RUB", 1);
            dataTable.Rows.Add("USD", 92);
            dataTable.Rows.Add("EUR", 101);
            dataTable.Rows.Add("GBR", 118);
            dataTable.Rows.Add("BYN", 30);
            dataTable.Rows.Add("AMD", 0.23);
            dataTable.Rows.Add("HUF", 0.26);
            dataTable.Rows.Add("GEL", 35);
            dataTable.Rows.Add("KZT", 0.21);
            dataTable.Rows.Add("TRY", 3.4);
            dataTable.Rows.Add("CZK", 4.2);
            dataTable.Rows.Add("SEK", 8.7);
            dataTable.Rows.Add("CHF", 105);
            dataTable.Rows.Add("RSD", 0.87);
            dataTable.Rows.Add("JPY", 0.64);

            cbFrom.ItemsSource = dataTable.DefaultView;
            cbFrom.DisplayMemberPath = "Text";
            cbFrom.SelectedValuePath = "Value";
            cbFrom.SelectedIndex = 0;

            cbTo.ItemsSource = dataTable.DefaultView;
            cbTo.DisplayMemberPath = "Text";
            cbTo.SelectedValuePath = "Value";
            cbTo.SelectedIndex = 0;
        }

        private void ClearMaster()
        {
            tbAmount.Text = string.Empty;

            if(cbFrom.Items.Count > 0)
                cbFrom.SelectedIndex = 0;
            if(cbTo.Items.Count > 0)
                cbTo.SelectedIndex = 0;

            lblTotal.Content = "";
            tbAmount.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertedValue;

            if (tbAmount.Text == null || tbAmount.Text.Trim() == "")
            {
                MessageBox.Show("Пожалуйста, введите валюту", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                tbAmount.Focus();
                return;
            }
            else if (cbFrom.SelectedValue == null || cbFrom.SelectedIndex == 0)
            {
                MessageBox.Show("Пожалуйста, введите валюту из которой производить ковертацию", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                cbFrom.Focus();
                return;
            }
            else if (cbTo.SelectedValue == null || cbTo.SelectedIndex == 0)
            {
                MessageBox.Show("Пожалуйста, введите валюту в которую произвести конвертацию", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                cbTo.Focus();
                return;
            }

            if (cbFrom.Text == cbTo.Text)
            {
                ConvertedValue = double.Parse(tbAmount.Text);
                lblTotal.Content = cbFrom.Text + " " + ConvertedValue.ToString("N3");
            }
            else
            {
                ConvertedValue = (double.Parse(cbFrom.SelectedValue.ToString()) *
                    double.Parse(tbAmount.Text)) /
                    double.Parse(cbTo.SelectedValue.ToString());
                lblTotal.Content = cbTo.Text + " " + ConvertedValue.ToString("N3");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearMaster();
        }
    }
}
