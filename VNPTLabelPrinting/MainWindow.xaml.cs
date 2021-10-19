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
using VNPTLabelPrinting.Function.Global;
using VNPTLabelPrinting.uCtrl;

namespace VNPTLabelPrinting {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        ucRunAll uc_runall = new ucRunAll();
        ucSetting uc_setting = new ucSetting();
        ucLog uc_log = new ucLog();
        ucHelp uc_help = new ucHelp();
        ucAbout uc_about = new ucAbout();

        public MainWindow() {
            InitializeComponent();
            this.DataContext = myGlobal.myTesting;
            addControl(0);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            Label l = sender as Label;
            switch (l.Content.ToString()) {
                case "X": {
                        this.Close();
                        break;
                    }
                default: {
                        Dictionary<string, int> dictionary = new Dictionary<string, int>() { { "RUN ALL", 0 }, { "SETTING", 1 }, { "LOG", 2 }, { "HELP", 3 }, { "ABOUT", 4 } };
                        int t = 0;
                        bool ret = dictionary.TryGetValue(l.Content.ToString(), out t);
                        this.lblMinus.Margin = new Thickness(t * 100, 0, 0, 0);
                        //add control
                        addControl(t);
                        break;
                    }
            }
        }

        private bool addControl(int idx) {
            this.grid_main.Children.Clear();
            switch (idx) {
                case 0: { this.grid_main.Children.Add(uc_runall); break; }
                case 1: { this.grid_main.Children.Add(uc_setting); break; }
                case 2: { this.grid_main.Children.Add(uc_log); break; }
                case 3: { this.grid_main.Children.Add(uc_help); break; }
                case 4: { this.grid_main.Children.Add(uc_about); break; }
            }
            return true;
        }

        private void WrapPanel_MouseDown(object sender, MouseButtonEventArgs e) {
            if (MessageBox.Show("Bạn muốn quay trở lại form login phải không?\nChọn 'YES' = đồng ý, 'NO' = thoát.", "Back to login form", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                LoginWindow window = new LoginWindow();
                window.Show();
                this.Close();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            myGlobal.isPause = true;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
            myGlobal.isPause = false;
        }
    }
}
