using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPTLabelPrinting.Function.Custom {
    public class SettingItemInfo : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public SettingItemInfo() {
            Title = "";
            Content = "";
        }

        string _title;
        public string Title {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        string _content;
        public string Content {
            get { return _content; }
            set {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

    }
}
