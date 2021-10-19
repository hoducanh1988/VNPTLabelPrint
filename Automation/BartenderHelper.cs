using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation {

    public class BartenderHelper : IDisposable {

        BarTender.Application btApp;
        BarTender.Format btFormat;
        BarTender.Messages btMsgs;

        public class ItemVariable {

            public ItemVariable() {
                Name = "";
                Value = "";
            }

            public string Name { get; set; }
            public string Value { get; set; }
        }

        public BartenderHelper() {
            btApp = new BarTender.Application();
        }

        public bool printLabel(string layout_file_full_name, string printer_name, string copies, params ItemVariable[] variables) {
            try {
                string f_name = layout_file_full_name;
                btFormat = btApp.Formats.Open(f_name, false, "");

                foreach (var v in variables) {
                    btFormat.SetNamedSubStringValue(v.Name, v.Value);
                }

                btFormat.Printer = printer_name;
                for ( int i=0;i< int.Parse(copies); i++) {
                    btFormat.Print("label", false, -1, out btMsgs);
                }
                
                btFormat.Close(BarTender.BtSaveOptions.btDoNotSaveChanges);

                return true;
            }
            catch (Exception ex) {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false; 
            }
        }

        public void Dispose() {
            try { btApp.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges); }
            catch { return; }
        }

    }
}
