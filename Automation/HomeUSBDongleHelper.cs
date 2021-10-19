using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automation {
    public class HomeUSBDongleHelper : IDisposable {
        string port_name = "";
        int baud_rate = 115200;
        SerialPort dut = null;
        bool isConnected = false;

        public HomeUSBDongleHelper(string port, string baud) {
            this.port_name = port;
            this.baud_rate = int.Parse(baud);
            isConnected = Open();
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

        public string Query(string cmd, int delay) {
            if (!isConnected) return null;
            dut.WriteLine(cmd); 
            Thread.Sleep(delay);
            string data = dut.ReadExisting();
            return data;
        }

        public int? getCounter(out string message) {
            message = "";
            try {
                if (!isConnected) return null;
                int count = 0;
            RE:
                count++;
                string data = this.Query("CHECK,COUNTER!", 250);
                data = data.Replace("\0", "").Trim();
                bool r = data.ToLower().Contains("resp,") && data.ToLower().Contains("!");
                if (!r) {
                    if (count < 3) goto RE;
                    else return null;
                }
                data = data.ToLower().Split(new string[] { "resp," }, StringSplitOptions.None)[1].Replace("!", "").Replace("\n", "").Replace("\r", "").Trim();
                data = data.ToUpper();
                return int.Parse(data);
            }
            catch (Exception ex) {
                message = ex.ToString();
                return null;
            }
        }

        public bool setLQI(string value) {
            try {
                if (!isConnected) return false;
                int count = 0;
            RE:
                count++;
                string data = this.Query($"CHECK,LQI,{value}!", 250);
                bool r = data.ToLower().Contains($"writed lqi = {value}");
                if (!r) {
                    if (count < 3) goto RE;
                    else return false;
                }
                return true;
            }
            catch { return false; }
        }

        public bool openNetwork() {
            try {
                if (isConnected == false) return false;
                int count = 0;
            RE:
                count++;
                string data = this.Query($"CHECK,OpenNW,1!", 250);
                bool r = data.ToLower().Contains($"opennw-ok");
                if (!r) {
                    if (count < 3) goto RE;
                    else return false;
                }
                return true;
            }
            catch { return false; }
        }

        public bool closeNetwork() {
            try {
                if (isConnected == false) return false;
                int count = 0;
            RE:
                count++;
                string data = this.Query($"CHECK,CloseNW,1!", 250);
                bool r = data.ToLower().Contains($"closenw-ok");
                if (!r) {
                    if (count < 3) goto RE;
                    else return false;
                }
                return true;
            }
            catch { return false; }
        }

        public string getEUI(out string message) {
            message = "";
            try {
                if (!isConnected) return null;
                int count = 0;
                string data = "";
            RE:
                count++;
                data += dut.ReadExisting();
                message = data;
                data = data.Replace("\0", "").Trim();
                bool r = data.ToLower().Contains("eui") && data.ToLower().Contains("lqi");
                if (!r) {
                    if (count < 60) {
                        Thread.Sleep(1000);
                        goto RE;
                    }
                    else return null;
                }

                string[] buffer = data.Split('\n');
                foreach (var b in buffer) {
                    if (b.ToLower().Contains("eui")) {
                        data = b;
                        break;
                    }
                }

                data = data.ToLower().Split(new string[] { "eui =" }, StringSplitOptions.None)[1].Replace("!", "").Replace("\n", "").Replace("\r", "").Trim();
                data = data.ToUpper();
                return data;
            }
            catch (Exception ex) {
                message = ex.ToString();
                return null;
            }
        }

        public bool Reset() {
            if (!isConnected) return false;
            dut.WriteLine("CHECK,RESTARTNOW!");
            this.Dispose();
            Thread.Sleep(1000);
            this.Open();

            int count = 0;
            int c = 0;
            int d = 0;
            string data = "";
        RE:
            count++;
            Thread.Sleep(1000);
            data += dut.ReadExisting().ToUpper();
            string[] buffer = data.Split(new string[] { "STARTUP!" }, StringSplitOptions.None);
            c = buffer.Length;
            if (c > d) {
                d = c;
                goto RE;
            }

            return true;
        }

        public void Dispose() {
            try {
                if (dut != null) dut.Close();
            }
            catch { }
        }

    }
}
