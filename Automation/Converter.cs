using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Automation {
    public class Converter {

        public static string charToYear(char[] chars_year) {
            try {
                int start_year = 2013;
                int num = (int)chars_year[0];
                int yyyy = num < 60 ? (start_year + (num - 48)) : (start_year + (num - 55));
                return yyyy.ToString();
            } catch { return null; }
        }

        public static string weekToMonth (string week, string year) {
            try {
                GregorianCalendar gc = new GregorianCalendar();
                for (DateTime dt = new DateTime(int.Parse(year), 1, 1); dt.Year == int.Parse(year); dt = dt.AddDays(1)) {
                    if (gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday) == int.Parse(week)) {
                        return dt.Month.ToString();
                    }
                }
                return (-1).ToString();
            } catch { return (-1).ToString(); }
        }

        public static string serialToDate(string serial) {
            try {
                serial = serial.ToUpper();
                string y = serial.Substring(4, 1);
                string ww = serial.Substring(5, 2);

                string yyyy = charToYear(y.ToCharArray());
                string mm = weekToMonth(ww, yyyy);
                return $"{mm}/{yyyy}";
            } catch { return null; }
        }

        public static string uidToFactorySerialNumber(string uid, string product_number, string factory, string hardwave_version, string product_color) {
            try {
                string year = "", week = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                string sn = string.Format("{0}{1}{2}{3}{4}{5}{6}", product_number, factory, year, week, hardwave_version, product_color, uid.Split('-')[1]);
                return sn;
            }
            catch { return null; }
        }

        public static string oneHomeUidToFactorySerialNumber(string uid, string product_number, string factory, string hardwave_version, string product_color) {
            try {
                string year = "", week = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                string sn = string.Format("{0}{1}{2}{3}{4}{5}{6}", product_number, factory, year, week, hardwave_version, product_color, uid.Substring(uid.Length - 6, 6));
                return sn;
            }
            catch { return null; }
        }

        public static string oneFarmUidToFactorySerialNumber(string uid, string product_number, string factory, string hardwave_version, string product_color) {
            try {
                string year = "", week = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                string sn = string.Format("{0}{1}{2}{3}{4}{5}{6}", product_number, factory, year, week, hardwave_version, product_color, uid.Substring(uid.Length - 6, 6));
                return sn;
            }
            catch { return null; }
        }

        public static string oneHomeMacToFactorySerialNumber(string mac, string product_number, string factory, string hardwave_version, string product_color) {
            try {
                string year = "", week = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                string sn = string.Format("{0}{1}{2}{3}{4}{5}{6}", product_number, factory, year, week, hardwave_version, product_color, mac.Substring(mac.Length - 6, 6));
                return sn;
            }
            catch { return null; }
        }

        public static string oneFarmMacToFactorySerialNumber(string mac, string product_number, string factory, string hardwave_version, string product_color) {
            try {
                string year = "", week = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                string sn = string.Format("{0}{1}{2}{3}{4}{5}{6}", product_number, factory, year, week, hardwave_version, product_color, mac.Substring(mac.Length - 6, 6));
                return sn;
            }
            catch { return null; }
        }

        public static string macToFactorySerialNumber(string mac, string product_number, string factory, string hardwave_version, string product_mac_code) {
            try {
                //SN = ProductNumber[0-2] + Factory[2-3] + Year[3-4] + Week[4-6] + ProductVersion[6-7] + ProductColor[7-8] + MAC[8-15]
                string year = "", week = "", id = "", mac_header = "", mac_code = "";
                int t = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2)) - 13;
                if (t < 0) return null;
                year = t < 10 ? t.ToString() : Convert.ToChar(55 + t).ToString().Trim();
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                week = weekNum < 10 ? string.Format("0{0}", weekNum) : weekNum.ToString().Trim();
                id = mac.Substring(6, 6).ToUpper().Trim();
                mac_header = mac.Substring(0, 6).ToUpper().Trim();

                product_mac_code = product_mac_code.Replace("\r", "").Replace("\n", "").Trim();
                string[] buffer = product_mac_code.Split(',');
                foreach (var b in buffer) {
                    if (b.ToUpper().Contains(mac_header)) {
                        if (b.Contains("=")) {
                            mac_code = b.Split('=')[1].ToUpper();
                        }
                        else if (b.Contains(":")) {
                            mac_code = b.Split(':')[1].ToUpper();
                        }
                        
                        break;
                    }
                }
                if (string.IsNullOrEmpty(mac_code) || string.IsNullOrWhiteSpace(mac_code)) return null;

                //
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                                     product_number.ToUpper().Trim(),
                                     factory.ToUpper().Trim(),
                                     year,
                                     week,
                                     hardwave_version.ToUpper().Trim(),
                                     mac_code.ToUpper().Trim(),
                                     id);
            }
            catch { return null; }
        }

        public static string MacToGponSerial(string mac) {
            try {
                string mac_Header = mac.Substring(0, 6);
                string low_MAC = mac.Substring(6, 6);
                string origalByteString = Convert.ToString(HexToBin(low_MAC)[0], 2).PadLeft(8, '0');
                string VNPT_SERIAL_ONT = null;

                origalByteString = origalByteString + "" + Convert.ToString(HexToBin(low_MAC)[1], 2).PadLeft(8, '0');
                origalByteString = origalByteString + "" + Convert.ToString(HexToBin(low_MAC)[2], 2).PadLeft(8, '0');
                //----HEX to BIN Cach 2-------
                string value = low_MAC;
                var s = String.Join("", low_MAC.Select(x => Convert.ToString(Convert.ToInt32(x + "", 16), 2).PadLeft(4, '0')));
                //----HEX to BIN Cach 2-------
                string shiftByteString = "";
                shiftByteString = origalByteString.Substring(1, origalByteString.Length - 1) + origalByteString[0];

                string[] lines = System.IO.File.ReadAllLines(string.Format("{0}GponFormat.dll", AppDomain.CurrentDomain.BaseDirectory));
                foreach (var line in lines) {
                    if (line.ToUpper().Contains(mac_Header.ToUpper())) {
                        VNPT_SERIAL_ONT = line.Split('=')[1].ToUpper() + BinToHex(shiftByteString);
                        break;
                    }
                }

                return VNPT_SERIAL_ONT;
            }
            catch {
                return null;
            }
        }

        public static string MacToWpsPin_Broadcom(string mac) {
            try {
                long tempPIN;
                long PinCodeDevice;
                long accum = 0;
                long digit;

                string StrInput = EncodeString(mac);

                tempPIN = long.Parse(StrInput, System.Globalization.NumberStyles.HexNumber);
                PinCodeDevice = tempPIN % 9999999;

                PinCodeDevice *= 10;
                accum += 3 * ((PinCodeDevice / 10000000) % 10);
                accum += 1 * ((PinCodeDevice / 1000000) % 10);
                accum += 3 * ((PinCodeDevice / 100000) % 10);
                accum += 1 * ((PinCodeDevice / 10000) % 10);
                accum += 3 * ((PinCodeDevice / 1000) % 10);
                accum += 1 * ((PinCodeDevice / 100) % 10);
                accum += 3 * ((PinCodeDevice / 10) % 10);

                digit = (accum % 10);
                accum = (10 - digit) % 10;
                PinCodeDevice += accum;
                string str = string.Empty;
                str = PinCodeDevice.ToString("D8");

                return str;
            }
            catch {
                return null;
            }
        }

        public static string MacToWpsPin_Econet(string mac) {
            try {
                int[] macAddr = StringArrayToIntArray(MacToSixArray(mac));

                int iPin, checksum;
                iPin = macAddr[0] * 256 * 256 + macAddr[4] * 256 + macAddr[5];
                iPin = iPin % 10000000;
                checksum = ComputeChecksum(iPin);
                iPin = iPin * 10 + checksum;

                string result = iPin.ToString();
                for (int i = 0; i < 8; i++) {
                    if (result.Length < 8) {
                        result = "0" + result;
                    }
                    else break;
                }

                return result;
            }
            catch {
                return null;
            }
        }

        public static string MacToPLDTWiFiPassword(string mac_address) {
            try {
                string upper_characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string lower_characters = "abcdefghijklmnopqrstuvwxyz";
                string specical_characters = "@$&-_=?";
                string digit_characters = "1234567890";

                mac_address = mac_address.ToUpper();

                if (mac_address.Length != 12) return null;

                string pattern = "^[0-9,A-F]{12}$";
                if (Regex.IsMatch(mac_address, pattern, RegexOptions.IgnoreCase) == false) return null;

                string last_8_digit = mac_address.ToUpper().Substring(4, 8);
                int sum_uppper = 0, sum_lower1 = 0, sum_lower2 = 0, sum_digit = 0, sum_special = 0;
                int idx = 0;
                foreach (var c in last_8_digit) {
                    int c_int = (int)c;

                    sum_uppper += c_int;
                    if (idx == 0 || idx == 3 || idx == 4 || idx == 6 || idx == 7) sum_special += c_int;
                    if (idx == 1 || idx == 2 || idx == 5 || idx == 6 || idx == 7) sum_digit += c_int;
                    if (idx == 0 || idx == 2 || idx == 4 || idx == 6 || idx == 7) sum_lower1 += c_int;
                    if (idx == 1 || idx == 3 || idx == 5 || idx == 6 || idx == 7) sum_lower2 += c_int;

                    idx++;
                }

                int idx_upper = -1, idx_lower1 = -1, idx_lower2 = -1, idx_digit = -1, idx_special = -1;
                idx_upper = sum_uppper % 26;
                idx_special = sum_special % 7;
                idx_digit = sum_digit % 10;
                idx_lower1 = sum_lower1 % 26;
                idx_lower2 = sum_lower2 % 26;

                string pldt_wifi_pass = string.Format("PLDTWIFI{0}{1}{2}{3}{4}", lower_characters.ToCharArray()[idx_lower1],
                                                                                 digit_characters.ToCharArray()[idx_digit],
                                                                                 upper_characters.ToCharArray()[idx_upper],
                                                                                 specical_characters.ToCharArray()[idx_special],
                                                                                 lower_characters.ToCharArray()[idx_lower2]);
                return pldt_wifi_pass;
            }
            catch { return null; }
        }

        public static bool QRCodeToUID_Serial(string qr_code, out string uid, out string serial) {
            uid = null; serial = null;
            if (string.IsNullOrEmpty(qr_code) || string.IsNullOrWhiteSpace(qr_code)) return false;
            qr_code = qr_code.ToLower();
            if (qr_code.Contains("eui") == false || qr_code.Contains("sn") == false) return false;
            string data = qr_code.Split(new string[] { "eui" }, StringSplitOptions.None)[1];
            serial = data.Split(new string[] { "sn" }, StringSplitOptions.None)[1].Replace(":", "").Replace("\n", "").Replace("\r", "").Trim().ToUpper();
            uid = data.Split(new string[] { "sn" }, StringSplitOptions.None)[0].Replace(":", "").Replace("\n", "").Replace("\r", "").Trim().ToUpper();
            return true;
        }

        public static string addStringInt(string value, int number) {
            int data = int.Parse(value) + number;
            return data.ToString().PadLeft(6, '0');
        }

        #region support

        private static string EncodeString(string mac) {
            string result = string.Empty;
            string temp = string.Empty;
            StringBuilder str = new StringBuilder();

            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] myPass = System.Text.Encoding.UTF8.GetBytes(mac);
            myPass = myMD5.ComputeHash(myPass);
            StringBuilder s = new StringBuilder();
            foreach (byte p in myPass) {
                s.Append(p.ToString("x").ToLower());
            }
            temp = s.ToString();
            for (int i = 0; i < 6; i++) {
                str.Append(temp[i * 3 + 3]);
            }
            return str.ToString().Trim();
        }

        private static int ComputeChecksum(int PIN) {
            int digit_s;
            int accum = 0;

            PIN *= 10;
            accum += 3 * ((PIN / 10000000) % 10);
            accum += 1 * ((PIN / 1000000) % 10);
            accum += 3 * ((PIN / 100000) % 10);
            accum += 1 * ((PIN / 10000) % 10);
            accum += 3 * ((PIN / 1000) % 10);
            accum += 1 * ((PIN / 100) % 10);
            accum += 3 * ((PIN / 10) % 10);

            digit_s = (accum % 10);
            return ((10 - digit_s) % 10);
        }

        public static string[] MacToSixArray(string mac) {
            string[] buffer = new string[6];
            for (int i = 0; i < mac.Length; i += 2) {
                buffer[i / 2] = mac.Substring(i, 2);
            }

            return buffer;
        }

        public static int[] StringArrayToIntArray(string[] arr) {
            int[] buffer = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                buffer[i] = HexToDec(arr[i]);
            }

            return buffer;
        }

        public static string DecToHex(int dec) {
            string hex = "";
            hex = dec.ToString("X4");
            if (hex.Length > 4) {
                hex = hex.Substring(hex.Length - 4, 4);
            }
            return hex;
        }

        public static int HexToDec(string Hex) {
            int decValue = int.Parse(Hex, System.Globalization.NumberStyles.HexNumber);
            return decValue;
        }

        public static string BinToHex(string bin) {
            string output = "";
            try {
                int rest = bin.Length % 4;
                bin = bin.PadLeft(rest, '0'); //pad the length out to by divideable by 4

                for (int i = 0; i <= bin.Length - 4; i += 4) {
                    output += string.Format("{0:X}", Convert.ToByte(bin.Substring(i, 4), 2));
                }

                return output;
            }
            catch {
                return null;
            }
        }

        public static Byte[] HexToBin(string pHexString) {
            if (String.IsNullOrEmpty(pHexString))
                return new Byte[0];

            if (pHexString.Length % 2 != 0)
                throw new Exception("Hexstring must have an even length");

            Byte[] bin = new Byte[pHexString.Length / 2];
            int o = 0;
            int i = 0;
            for (; i < pHexString.Length; i += 2, o++) {
                switch (pHexString[i]) {
                    case '0': bin[o] = 0x00; break;
                    case '1': bin[o] = 0x10; break;
                    case '2': bin[o] = 0x20; break;
                    case '3': bin[o] = 0x30; break;
                    case '4': bin[o] = 0x40; break;
                    case '5': bin[o] = 0x50; break;
                    case '6': bin[o] = 0x60; break;
                    case '7': bin[o] = 0x70; break;
                    case '8': bin[o] = 0x80; break;
                    case '9': bin[o] = 0x90; break;
                    case 'A': bin[o] = 0xa0; break;
                    case 'a': bin[o] = 0xa0; break;
                    case 'B': bin[o] = 0xb0; break;
                    case 'b': bin[o] = 0xb0; break;
                    case 'C': bin[o] = 0xc0; break;
                    case 'c': bin[o] = 0xc0; break;
                    case 'D': bin[o] = 0xd0; break;
                    case 'd': bin[o] = 0xd0; break;
                    case 'E': bin[o] = 0xe0; break;
                    case 'e': bin[o] = 0xe0; break;
                    case 'F': bin[o] = 0xf0; break;
                    case 'f': bin[o] = 0xf0; break;
                    default: throw new Exception("Invalid character found during hex decode");
                }

                switch (pHexString[i + 1]) {
                    case '0': bin[o] |= 0x00; break;
                    case '1': bin[o] |= 0x01; break;
                    case '2': bin[o] |= 0x02; break;
                    case '3': bin[o] |= 0x03; break;
                    case '4': bin[o] |= 0x04; break;
                    case '5': bin[o] |= 0x05; break;
                    case '6': bin[o] |= 0x06; break;
                    case '7': bin[o] |= 0x07; break;
                    case '8': bin[o] |= 0x08; break;
                    case '9': bin[o] |= 0x09; break;
                    case 'A': bin[o] |= 0x0a; break;
                    case 'a': bin[o] |= 0x0a; break;
                    case 'B': bin[o] |= 0x0b; break;
                    case 'b': bin[o] |= 0x0b; break;
                    case 'C': bin[o] |= 0x0c; break;
                    case 'c': bin[o] |= 0x0c; break;
                    case 'D': bin[o] |= 0x0d; break;
                    case 'd': bin[o] |= 0x0d; break;
                    case 'E': bin[o] |= 0x0e; break;
                    case 'e': bin[o] |= 0x0e; break;
                    case 'F': bin[o] |= 0x0f; break;
                    case 'f': bin[o] |= 0x0f; break;
                    default: throw new Exception("Invalid character found during hex decode");
                }
            }
            return bin;
        }

        public static string StringToHex(string hexstring) {
            StringBuilder sb = new StringBuilder();
            foreach (char t in hexstring) {
                //Note: X for upper, x for lower case letters
                sb.Append(Convert.ToInt32(t).ToString("x"));
            }
            return sb.ToString();
        }

        public static string HexToString(string hexString) {
            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2) {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }

        public static string uniCodeToBinary(string unicode_text) {
            return String.Join(String.Empty, Encoding.Unicode.GetBytes(unicode_text).Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0'))); // must ensure 8 digits.
        }

        public static string binaryToUnicode(string binary_text) {
            return Encoding.Unicode.GetString(System.Text.RegularExpressions.Regex.Split(binary_text, "(.{8})").Where(binary => !String.IsNullOrEmpty(binary)).Select(binary => Convert.ToByte(binary, 2)).ToArray());
        }

        public static string stringToMD5(string input_str) {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input_str));

            for (int i = 0; i < bytes.Length; i++) {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        public static string intToTimeSpan(int time_ms) {
            TimeSpan result = TimeSpan.FromMilliseconds(time_ms);
            return result.ToString("hh':'mm':'ss");
        }

        #endregion
        


    }
}
