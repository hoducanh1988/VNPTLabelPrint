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
using System.IO;
using System.Diagnostics;

namespace VNPTLabelPrinting.uCtrl {
    /// <summary>
    /// Interaction logic for ucItemLayoutFileSetting.xaml
    /// </summary>
    public partial class ucItemLayoutFileSetting : UserControl {
        public ucItemLayoutFileSetting() {
            InitializeComponent();

            List<string> list_file_layout = new List<string>();
            DirectoryInfo di = new DirectoryInfo($"{AppDomain.CurrentDomain.BaseDirectory}Layout");
            FileInfo[] fs = di.GetFiles("*.btw");
            foreach (var f in fs) {
                list_file_layout.Add(f.Name);
            }
            this.cbb_layout_file.ItemsSource = list_file_layout;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (this.cbb_layout_file.SelectedValue != null) {
                string value = this.cbb_layout_file.SelectedValue as string;
                string f = $"{AppDomain.CurrentDomain.BaseDirectory}Layout\\{value}";
                Process.Start(f);
            }
        }
    }
}
