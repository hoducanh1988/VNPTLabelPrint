using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VNPTLabelPrinting.Function.Custom
{
    public class ProductMember : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ProductMember() {
            Name = "";
            isChecked = false;
        }

        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        bool _is_checked;
        public bool isChecked {
            get { return _is_checked; }
            set {
                _is_checked = value;
                OnPropertyChanged(nameof(isChecked));
            }
        }
    }
}
