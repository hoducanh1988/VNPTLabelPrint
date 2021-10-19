using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automation {
    public class IO {

        public static bool ToFile(string file_name, List<string> listData, string data) {
            try {
                if (listData == null) listData = new List<string>();
                listData.Add(data);

                RE:
                if (listData.Count > 1) {
                    listData.RemoveAt(0);
                    goto RE;
                }
                
                File.WriteAllLines(file_name, listData.ToArray());
                return true;
            } catch { return false; }
        }

        public static List<string> FromFile(string file_name) {
            try {
                if (File.Exists(file_name) == false) return null;
                string[] buffer = File.ReadAllLines(file_name);
                return buffer.ToList();
            } catch { return null; }
        }


    }
}
