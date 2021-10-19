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
using System.Windows.Xps.Packaging;
using System.IO;

namespace VNPTLabelPrinting.uCtrl
{
    /// <summary>
    /// Interaction logic for ucHelp.xaml
    /// </summary>
    public partial class ucHelp : UserControl
    {
        public ucHelp()
        {
            InitializeComponent();

            if (File.Exists(string.Format("{0}Help.xps", System.AppDomain.CurrentDomain.BaseDirectory))) {
                XpsDocument xpsDocument = new XpsDocument(string.Format("{0}Help.xps", System.AppDomain.CurrentDomain.BaseDirectory), System.IO.FileAccess.Read);
                FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
                _docViewer.Document = fds;
            }
        }
    }
}
