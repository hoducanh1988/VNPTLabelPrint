using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPTLabelPrinting.Function.Custom
{
    public class ItemInputBarcode : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public ItemInputBarcode() {
            Title = "";
            Content = "";
            isInputted = false;
        }

        string _title;
        public string Title {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
                Title = Name.Replace("_", " ") + ":";
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
        bool _is_inputted;
        public bool isInputted {
            get { return _is_inputted; }
            set {
                _is_inputted = value;
                OnPropertyChanged(nameof(isInputted));
            }
        }

    }
}
