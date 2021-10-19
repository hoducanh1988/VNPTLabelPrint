using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Automation {
    public class HomeGatewayHelper : IDisposable {

        string port_name = "", login_user = "", login_pass = "";
        int baud_rate = 115200;
        SerialPort dut = null;

        public HomeGatewayHelper(string port, string baud, string user, string password) {
            this.port_name = port;
            this.baud_rate = int.Parse(baud);
            this.login_pass = password;
            this.login_user = user;
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
                if (!r) return false;

                dut.WriteLine("");
                Thread.Sleep(100);
                string data = dut.ReadExisting();
                msg += data;
                if (data.Contains("~#")) return true;

                if (data.Contains("login:")) {
                    dut.WriteLine(this.login_user);
                    Thread.Sleep(300);
                }

                data = dut.ReadExisting();
                msg += data;
                if (data.Contains("Password:")) {
                    dut.WriteLine(this.login_pass);
                    Thread.Sleep(1000);
                }
                else return false;

                data = dut.ReadExisting();
                msg += data;
                return data.Contains("~#");

            }
            catch (Exception ex) {
                msg += ex.ToString();
                return false;
            }
        }


        public string getUid(string interf, out string message) {
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


                string cmd = interf == "LAN" ? "cat /sys/class/net/eth0/address" : "cat /sys/class/net/wlan0/address";
                count = 0;
            RE_UID:
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
                    if (count < 3) goto RE_UID;
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
