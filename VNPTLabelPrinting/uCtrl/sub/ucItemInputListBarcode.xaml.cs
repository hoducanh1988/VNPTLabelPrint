using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using VNPTLabelPrinting.Function.Custom;
using Automation;

namespace VNPTLabelPrinting.uCtrl {
    /// <summary>
    /// Interaction logic for ucItemInputListBarcode.xaml
    /// </summary>
    public partial class ucItemInputListBarcode : UserControl {

        public class logItem {
            public string ID { get; set; }
            public string Serial { get; set; }
            public string TimeScan { get; set; }
            public string Lot { get; set; }
            public string Color { get; set; }
            public string ProductCode { get; set; }
            public string Line { get; set; }
            public string WorkOrder { get; set; }
        }

        public ItemInputBarcode data_cxt = null;
        public string max_serial = "";
        public string value_format = "";
        public string db_file = "";

        public ucItemInputListBarcode(ItemInputBarcode _data_cxt, string max_value, string value_format, string db_file) {
            InitializeComponent();
            this.data_cxt = _data_cxt;
            this.DataContext = this.data_cxt;
            this.max_serial = max_value;
            this.value_format = value_format;
            this.db_file = db_file;
        }

        private void TxtContent_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                if (string.IsNullOrEmpty(txtContent.Text) == true || string.IsNullOrWhiteSpace(txtContent.Text) == true) return;
                string value = (sender as TextBox).Text;
                //check format
                if (Regex.IsMatch(value, value_format, RegexOptions.IgnoreCase) == false) {
                    MessageBox.Show($"SN '{value}' sai định dạng.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    (sender as TextBox).Clear();
                    return;
                }
                //check dupplicate in list
                if (this.data_cxt.Content.Contains(value) == true) {
                    MessageBox.Show($"SN '{value}' bị trùng lặp.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    (sender as TextBox).Clear();
                    return;
                }
                //check dupplication in database
                MicrosoftAccessHelper masc = new MicrosoftAccessHelper(this.db_file);
                var result = masc.selectDataRow<logItem>("SN_Print_Log", "Serial", value);
                masc.Close();
                if (result != null) {
                    MessageBox.Show($"SN '{value}' đã in trong LOT {result.Lot}, {result.TimeScan}, {result.WorkOrder}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    (sender as TextBox).Clear();
                    return;
                }

                this.data_cxt.Content = value + "\n" + this.data_cxt.Content;
                label_counter.Content = string.Format("{0}/{1}", this.data_cxt.Content.Split('\n').Length - 1, max_serial);
                if (this.data_cxt.Content.Split('\n').Length - 1 < int.Parse(max_serial)) {
                    (sender as TextBox).Clear();
                }
                else {
                    this.data_cxt.isInputted = true;
                    txtContent.IsEnabled = false;
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.SaveFileDialog fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.Filter = "*.xls|*.xls";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                MicrosoftAccessHelper masc = new MicrosoftAccessHelper(this.db_file);
                masc.ExportQuery("SN_Print_Log", fileDialog.FileName);
                masc.Close();
                MessageBox.Show("Sucess.", "Export File", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
