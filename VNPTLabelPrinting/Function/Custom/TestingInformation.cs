using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPTLabelPrinting.Function.Custom {
    public class TestingInformation : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public TestingInformation() {
            InitParam();
        }

        public void InitParam() {
            totalResult = "--";
            inputBarcode = "";
            fileProduct = "";
            fileSetting = "";
            logSystem = "";
            productName = "";
            Station = "";
            appTitle = "";
        }

        public void Clear() {
            totalResult = "--";
            inputBarcode = "";
            logSystem = "";
        }

        public void waitParam() {
            totalResult = "Waiting...";
            logSystem = "";
            inputBarcode = "";
        }

        public bool passParam() {
            totalResult = "Passed";
            return true;
        }

        public bool failParam() {
            totalResult = "Failed";
            return true;
        }

        string _file_product;
        public string fileProduct {
            get { return _file_product; }
            set {
                _file_product = value;
                OnPropertyChanged(nameof(fileProduct));
            }
        }
        string _file_setting;
        public string fileSetting {
            get { return _file_setting; }
            set {
                _file_setting = value;
                OnPropertyChanged(nameof(fileSetting));
            }
        }
        string _total_result;
        public string totalResult {
            get { return _total_result; }
            set {
                _total_result = value;
                OnPropertyChanged(nameof(totalResult));
            }
        }
        string _input_barcode;
        public string inputBarcode {
            get { return _input_barcode; }
            set {
                _input_barcode = value;
                OnPropertyChanged(nameof(inputBarcode));
            }
        }
        string _log_system;
        public string logSystem {
            get { return _log_system; }
            set {
                _log_system = value;
                OnPropertyChanged(nameof(logSystem));
            }
        }
        string _product_name;
        public string productName {
            get { return _product_name; }
            set {
                _product_name = value;
                OnPropertyChanged(nameof(productName));
            }
        }
        string _station;
        public string Station {
            get { return _station; }
            set {
                _station = value;
                OnPropertyChanged(nameof(Station));
            }
        }
        string _app_title;
        public string appTitle {
            get { return _app_title; }
            set {
                _app_title = value;
                OnPropertyChanged(nameof(appTitle));
            }
        }
        public int Interval {
            get { return Properties.Settings.Default.interval; }
            set {
                Properties.Settings.Default.interval = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(Interval));
            }
        }



    }
}
