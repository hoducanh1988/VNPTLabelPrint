using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace VNPTLabelPrinting.uCtrl {
    /// <summary>
    /// Interaction logic for ucInstruction.xaml
    /// </summary>
    public partial class ucInstruction : UserControl {
        public ucInstruction() {
            InitializeComponent();
            string text = "";
            string p = myGlobal.myTesting.productName;
            switch (p) {
                case object a when p.ToLower().Contains("smoke"): {
                        text = "Vui lòng nhấn và giữ nút trên sản phẩm cho đến khi đèn led chuyển sang trạng thái sáng màu xanh thì nhả ra.";
                        break;
                    }
                case object b when p.ToLower().Contains("temp"): {
                        text = "Vui lòng nhấn và giữ nút trên sản phẩm cho đến khi vạch sóng trên màn hình sản phẩm nhấp nháy thì nhả ra.";
                        break;
                    }
                case object c when p.ToLower().Contains("motion"):
                case object d when p.ToLower().Contains("door"): {
                        text = "Vui lòng nhấn và giữ nút trên sản phẩm cho đến khi đèn led chuyển sang trạng thái sáng nhấp nháy thì nhả ra.";
                        break;
                    }
            }
            Thread t = new Thread(new ThreadStart(() => {
                int count = 0;
            RE:
                count++;
                Thread.Sleep(500);
                Dispatcher.Invoke(new Action(() => {
                    lbl_Instruction.Content = $"{text} ({count / 2}/60)";
                    this.Background = count % 2 == 0 ? Brushes.Yellow : Brushes.White;
                }));
                goto RE;
            }));
            t.IsBackground = true;
            t.Start();
        }
    }
}
