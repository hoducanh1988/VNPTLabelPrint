using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VNPTLabelPrinting.Function.Custom;
using VNPTLabelPrinting.Function.Global;

namespace VNPTLabelPrinting {
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window {
        string backup_file = $"{AppDomain.CurrentDomain.BaseDirectory}Backup.ini";

        public LoginWindow() {
            InitializeComponent();
            showObjectToTreeNode();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(label_selected.Content.ToString()) == false) {
                myGlobal.myTesting.fileProduct = $"Script\\{label_selected.Content.ToString()}";
                myGlobal.myTesting.fileSetting = $"Setting\\{label_selected.Content.ToString().Replace(".txt", ".ini")}";
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
        }

        private void CheckBox_Checked_UnChecked(object sender, RoutedEventArgs e) {
            string selected_value = "";
            clearSelectedValue(label_selected.Content as string);
            foreach (var pr in myGlobal.families) {
                foreach (var pd in pr.Products) {
                    var its = pd.Members.Where(x => x.isChecked == true).FirstOrDefault();
                    if (its != null) {
                        selected_value = $"{pr.Name}\\{pd.Name}\\{its.Name}";
                    }
                }
            }

            label_selected.Content = selected_value;
        }

        private void clearSelectedValue(string value) {
            if (value == "") return;

            string[] buffer = value.Split('\\');
            string prName = buffer[0];
            string pdName = buffer[1];
            string mbName = buffer[2];
            
            foreach (var pr in myGlobal.families) {
                if (pr.Name.Equals(prName)) {
                    foreach (var pd in pr.Products) {
                        if (pd.Name.Equals(pdName)) {
                            foreach (var mb in pd.Members) {
                                if (mb.Name.Equals(mbName)) {
                                    mb.isChecked = false;
                                    break;
                                }
                            }
                        }
                        
                    }
                }
            }
        }
        
        private bool showObjectToTreeNode() {
            try {
                myGlobal.families.Clear();
                //add family
                string root_dir = $"{AppDomain.CurrentDomain.BaseDirectory}Script";
                var subdirs = new DirectoryInfo(root_dir).GetDirectories();

                foreach (var d0 in subdirs) {
                    Project prj = new Project() { Name = d0.Name };
                    var ds = d0.GetDirectories();
                    foreach (var d1 in ds) {
                        prj.Products.Add(new Product() { Name = d1.Name });
                        var fs = d1.GetFiles("*.txt");
                        foreach (var f in fs) {
                            prj.Products[prj.Products.Count - 1].Members.Add(new ProductMember() { Name = f.Name });
                        }
                    }
                    myGlobal.families.Add(prj);
                }

                this.trvFamilies.ItemsSource = myGlobal.families;
                return true;
            }
            catch { return false; }
        }
    }


    public class StringToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (string.IsNullOrEmpty(value as string) == true) return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new Exception();
        }
    }

}
