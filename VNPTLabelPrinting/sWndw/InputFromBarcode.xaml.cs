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
using System.Windows.Shapes;
using System.Windows.Threading;
using VNPTLabelPrinting.uCtrl;

namespace VNPTLabelPrinting.sWndw
{
    /// <summary>
    /// Interaction logic for InputFromBarcode.xaml
    /// </summary>
    public partial class InputFromBarcode : Window
    {

        public InputFromBarcode() {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, e) => {

                bool closeWindow = true;
                foreach (UIElement child in mainViewer.Children) {
                    if (child is ucItemInputBarcode) {
                        //set focus
                        if ((child as ucItemInputBarcode).data_cxt.isInputted == false) {
                            (child as ucItemInputBarcode).txtContent.Focus();
                            closeWindow = false;
                            break;
                        }
                    }
                }

                //close form
                if (closeWindow) this.Close();
            };
            timer.Start();
        }
    }
}
