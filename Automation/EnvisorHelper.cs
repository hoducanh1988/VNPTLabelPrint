using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Automation {
    public class EnvisorHelper {

        string port_name = "";
        int baud_rate = 115200;
        SerialPort dut = null;

        public EnvisorHelper(string port, string baud) {
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

        public bool Login(out string msg) {
            msg = "";
            try {
                bool r = Open();
                return r;
            }
            catch (Exception ex) {
                msg += ex.ToString();
                return false;
            }
        }

        public string getMacWlan(out string message) {
            message = "";
            try {
                int count = 0;
            RE_LOGIN:
                count++;
                bool r = Login(out message);
                if (!r) {
                    if (count < 3) goto RE_LOGIN;
                    else return "";
                }


                string cmd = "Read_MAC";
                count = 0;
            RE_MAC:
                count++;
                string data = "";
                dut.WriteLine("");
                Thread.Sleep(100);
                dut.WriteLine(cmd);
                Thread.Sleep(300);
                data = dut.ReadExisting();
                message += data;

                string[] buffer = data.Split('\n');
                r = false;
                for (int i = 0; i < buffer.Length; i++) {
                    string s = buffer[i].Replace(":", "").Replace("\r", "").Trim().ToUpper();
                    string p = "^[0-9,A-F]{12}$";
                    if (Regex.IsMatch(s, p, RegexOptions.None) == true) {
                        data = s;
                        r = true;
                        break;
                    }
                }

                if (!r) {
                    if (count < 3) goto RE_MAC;
                    else return "";
                }

                return data;
            }
            catch (Exception ex) {
                message = ex.ToString();
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
