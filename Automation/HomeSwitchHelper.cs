using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation {
    public class HomeSwitchHelper : IDisposable {

        string port_name = "";
        int baud_rate = 115200;
        SerialPort dut = null;

        public HomeSwitchHelper(string port, string baud) {
            this.port_name = port;
            this.baud_rate = int.Parse(baud);
        }

        public bool Open() {
            try {
                dut = new SerialPort();
                dut.PortName = port_name;
                dut.BaudRate = baud_rate;
                dut.DataBits = 8;
                dut.StopBits = StopBits.One;
                dut.Parity = Parity.None;
                dut.Open();
                return dut.IsOpen;
            }
            catch { return false; }
        }

        public string getUid(out string message) {
            message = "";
            try {
                bool r = Open();
                if (!r) return "";

                int count = 0;
                string data = "";
                string eui = "";
            RE:
                count++;
                dut.WriteLine("Custom switch_turn_off_log_off 0xAAAA");
                Thread.Sleep(1000);
                dut.WriteLine("Custom who_are_you");
                Thread.Sleep(1000);
                data = dut.ReadExisting();
                message += data;
                r = data.ToLower().Contains("eui:") && data.ToLower().Contains("0x");
                if (!r) {
                    if (count < 3) goto RE;
                    else goto END;
                }

                string[] buffer = data.Split('\n');
                for (int i = 0; i < buffer.Length; i++) {
                    if (buffer[i].ToLower().Contains("eui:")) {
                        string s = buffer[i].Replace("\r", "").Replace("\n", "").Trim();
                        eui = s.Split(new string[] { "0x" }, StringSplitOptions.None)[1];
                        goto END;
                    }
                }

                END:
                dut.WriteLine("custom switch_turn_off_log");
                Thread.Sleep(1000);
                data = dut.ReadExisting();
                message += data;

                return eui;

            }
            catch (Exception ex) {
                message += ex.ToString();
                return "";
            }
        }

        public void Dispose() {
            try {
                if (dut != null) dut.Close();
            }
            catch { }
        }

    }
}
