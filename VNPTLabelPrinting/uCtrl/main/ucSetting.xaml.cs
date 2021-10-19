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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VNPTLabelPrinting.Function.Custom;
using VNPTLabelPrinting.Function.Global;

namespace VNPTLabelPrinting.uCtrl
{
    /// <summary>
    /// Interaction logic for ucSetting.xaml
    /// </summary>
    public partial class ucSetting : UserControl
    {

        public ucSetting() {
            InitializeComponent();
            loadSettingItem();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_content = (string)b.Content;

            switch (b_content) {
                case "Save Setting": {
                        using (var sw = new StreamWriter(myGlobal.myTesting.fileSetting)) {
                            foreach (var item in myGlobal.mySetting) {
                                string s = $"{item.Title.Replace(" ", "_")}={item.Content}";
                                sw.WriteLine(s);
                            }
                        }
                        MessageBox.Show("Success.", "Save Setting", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
            }
        }

        private bool loadSettingItem() {
            myGlobal.mySetting.Clear();
            string[] buffer = File.ReadAllLines(myGlobal.myTesting.fileSetting);
            foreach (var b in buffer) {
                if (string.IsNullOrEmpty(b) == false) {
                    string title = b.Split('=')[0].Replace("_", " ").Trim();
                    string content = b.Replace($"{title.Replace(" ", "_")}=", "");
                    SettingItemInfo item_info = new SettingItemInfo() { Title = title, Content = content };
                    myGlobal.mySetting.Add(item_info);

                    string sri = item_info.Title.ToLower();

                    switch (sri) {
                        case var s when sri.Contains("printer"): {
                                ucItemPrinterSetting ucItem = new ucItemPrinterSetting();
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("layout"): {
                                ucItemLayoutFileSetting ucItem = new ucItemLayoutFileSetting();
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("home switch model"): {
                                List<string> list_str = new List<string>() { "OHSSZL1Y01", "OHSSZL2Y01", "OHSSZL3Y01" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("home gateway model"): {
                                List<string> list_str = new List<string>() { "OHGW01H", "OHGW01L", "OHGW02H", "OHGW02L" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("home switch color"): {
                                List<string> list_str = new List<string>() { "Đen", "Trắng" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("home switch border"): {
                                List<string> list_str = new List<string>() { "Không", "Bạc", "Vàng" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("network interface"): {
                                List<string> list_str = new List<string>() { "LAN", "WIFI" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("serial port name"): {
                                List<string> list_str = new List<string>();
                                for (int i = 1; i < 100; i++) list_str.Add($"COM{i}");
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("color"): {
                                List<string> list_str = new List<string>() { "Black", "White", "" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("metal frame"): {
                                List<string> list_str = new List<string>() { "Gold", "Silver", "No", "" };
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("db file"): {
                                ucDBFile ucItem = new ucDBFile();
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        case var s when sri.Contains("production year"): {
                                List<string> list_str = new List<string>();
                                for (int i = 21; i < 100; i++) list_str.Add($"{i}");
                                ucItemComboboxSetting ucItem = new ucItemComboboxSetting(list_str);
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                        default: {
                                ucItemSetting ucItem = new ucItemSetting();
                                ucItem.DataContext = item_info;
                                this.sp_setting.Children.Add(ucItem);
                                break;
                            }
                    }

                }
            }

            return true;
        }

    }
}
