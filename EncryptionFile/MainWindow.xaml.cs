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
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace EncryptionFile {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        string safe_file_name = "";

        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_tag = b.Tag as string;

            switch (b_tag) {
                case "start_en": {
                        if (File.Exists(txt_efile.Text) == false) return;
                        string file = $"{AppDomain.CurrentDomain.BaseDirectory}{safe_file_name}";
                        Automation.Encryption encryption = new Automation.Encryption(file);
                        string data = File.ReadAllText(txt_efile.Text);
                        encryption.saveSource(data);
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case "browser_file_en": {
                        OpenFileDialog dlg = new OpenFileDialog();
                        if (dlg.ShowDialog() == true) {
                            txt_efile.Text = dlg.FileName;
                            safe_file_name = dlg.SafeFileName;
                        }
                        break;
                    }
                case "start_de": {
                        if (File.Exists(txt_dfile.Text) == false) return;
                        string file = $"{AppDomain.CurrentDomain.BaseDirectory}{safe_file_name}";
                        Automation.Encryption encryption = new Automation.Encryption(txt_dfile.Text);
                        string data = encryption.readSource();
                        File.WriteAllText(file, data);
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case "browser_file_de": {
                        OpenFileDialog dlg = new OpenFileDialog();
                        if (dlg.ShowDialog() == true) {
                            txt_dfile.Text = dlg.FileName;
                            safe_file_name = dlg.SafeFileName;
                        }
                        break;
                    }
            }


        }
    }
}
