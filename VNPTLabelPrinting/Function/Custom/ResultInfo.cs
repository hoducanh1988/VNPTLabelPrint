
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPTLabelPrinting.Function.Custom
{
    public class ResultInfo : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public ResultInfo() {
            itemName = "";
            itemResult = "";
            itemInput = "";
            itemOutput = "";
            isCheck = "";
        }

        string _name;
        public string itemName {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(itemName));
            }
        }
        string _is_check;
        public string isCheck {
            get { return _is_check; }
            set {
                _is_check = value;
                OnPropertyChanged(nameof(isCheck));
            }
        }
        string _input;
        public string itemInput {
            get { return _input; }
            set {
                _input = value;
                OnPropertyChanged(nameof(itemInput));
            }
        }
        string _output;
        public string itemOutput {
            get { return _output; }
            set {
                _output = value;
                OnPropertyChanged(nameof(itemOutput));
            }
        }
        string _result;
        public string itemResult {
            get { return _result; }
            set {
                _result = value;
                OnPropertyChanged(nameof(itemResult));
            }
        }
    }
}
