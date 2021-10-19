using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation {
    public class Parser {

        public static bool isMatchingUidAndSerialOneHome(string uid, string serial) {
            try {
                if (uid.Length != 16) return false;
                if (serial.Length != 15) return false;
                bool r = uid.Substring(uid.Length - 6, 6).Equals(serial.Substring(serial.Length - 6, 6));
                return r;
            } catch { return false; }
        }

        public static bool isMatchingMacAndSerialOneHome(string mac, string serial) {
            try {
                if (mac.Length != 12) return false;
                if (serial.Length != 15) return false;
                bool r = mac.Substring(mac.Length - 6, 6).Equals(serial.Substring(serial.Length - 6, 6));
                return r;
            }
            catch { return false; }
        }

        public static bool isMatchingMacAndSerialVNPT(string mac, string serial, string setting_product_number, string setting_factory, string setting_hardware_version, string setting_code_serial_number) {
            try {
                bool r = false;
                if (mac.Length != 12) return r;
                if (serial.Length != 15) return r;

                string product_number = serial.Substring(0, 3);
                r = product_number.Equals(setting_product_number);
                if (r == false) return r;

                string factory = serial.Substring(3, 1);
                r = factory.Equals(setting_factory);
                if (r == false) return r;

                string hardware_version = serial.Substring(7, 1);
                r = hardware_version.Equals(setting_hardware_version);
                if (r == false) return r;

                string mac_code = $"{mac.Substring(0, 6)}={serial.Substring(8, 1)}";
                r = setting_code_serial_number.Contains(mac_code);
                if (r == false) return r;

                r = mac.Substring(mac.Length - 6, 6).Equals(serial.Substring(serial.Length - 6, 6));
                return r;
            } catch { return false; }
        }

        public static bool isMatchingSerialNumber (string old_serial, string new_serial) {
            try {
                if (old_serial.Length != 15) return false;
                if (new_serial.Length != 15) return false;
                string osl = old_serial.Substring(0, 3) + old_serial.Substring(7, 8);
                string nsl = new_serial.Substring(0, 3) + new_serial.Substring(7, 8);
                return osl.ToUpper().Equals(nsl.ToUpper());
            } catch { return false; }
        }


        public static bool isStringExistInList(string str, List<string> list) {
            if (list == null || list.Count == 0) return false;
            return list.Contains(str);
        }

        public static int stringToInt(string value) {
            return int.Parse(value);
        }

        public static bool isIMEINumber(string imei) {
            char[] array_input = imei.ToCharArray();
            string s = "";

            int idx = 0;
            foreach (var c in array_input) {
                int value = int.Parse(c.ToString());
                if (idx < 14) s += idx % 2 == 0 ? value.ToString() : (value * 2).ToString();
                else continue;
                idx++;
            }

            int number = 0;
            char[] array_output = s.ToCharArray();
            foreach (var c in array_output) {
                number += int.Parse(c.ToString());
            }

            int digit_number = int.Parse(imei.Substring(imei.Length - 1, 1));
            int sum = number + digit_number;

            return sum % 10 == 0;
        }

    }
}
