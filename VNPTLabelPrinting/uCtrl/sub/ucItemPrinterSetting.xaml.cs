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

namespace VNPTLabelPrinting.uCtrl {
    /// <summary>
    /// Interaction logic for ucItemPrinterSetting.xaml
    /// </summary>
    public partial class ucItemPrinterSetting : UserControl {
        public ucItemPrinterSetting() {
            InitializeComponent();
            this.cbb_printer.ItemsSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
        }
    }
}
