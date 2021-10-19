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
using VNPTLabelPrinting.Function.Custom;

namespace VNPTLabelPrinting.uCtrl
{
    /// <summary>
    /// Interaction logic for ucItemInputBarcode.xaml
    /// </summary>
    public partial class ucItemInputBarcode : UserControl
    {
        public ItemInputBarcode data_cxt = null;

        public ucItemInputBarcode(ItemInputBarcode _data_cxt) {
            InitializeComponent();
            this.data_cxt = _data_cxt;
            this.DataContext = this.data_cxt;
        }

        private void TxtContent_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                if (string.IsNullOrEmpty(txtContent.Text) == true || string.IsNullOrWhiteSpace(txtContent.Text) == true) return;
                this.data_cxt.isInputted = true;
                txtContent.IsEnabled = false;
            }
        }
    }
}
