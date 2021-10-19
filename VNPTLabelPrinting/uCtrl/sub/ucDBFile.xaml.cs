using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for ucDBFile.xaml
    /// </summary>
    public partial class ucDBFile : UserControl {

        public ucDBFile() {
            InitializeComponent();
            List<string> list_db_file = new List<string>();
            DirectoryInfo di = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}DB");
            FileInfo[] fs = di.GetFiles("*.accdb");
            foreach (var f in fs) {
                list_db_file.Add(f.Name);
            }
            this.cbb_db_file.ItemsSource = list_db_file;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (this.cbb_db_file.SelectedValue != null) {
                string value = this.cbb_db_file.SelectedValue as string;
                string f = $"{AppDomain.CurrentDomain.BaseDirectory}DB\\{value}";
                Process.Start(f);
            }
        }

    }
}
