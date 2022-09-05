using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7_Validation
{
    public class VM : INotifyPropertyChanged
    {
        public VM()
        {
            _customText = "00:00:00.000";
        }

        private string _customText;

        public event PropertyChangedEventHandler PropertyChanged;

        //[RegularExpression(@"(\d){2}:(\d){2}:(\d){2}\.(\d){3}")]
        [RegularExpression(@"(\d){2}:(\d){2}:(\d){2}\.(\d){3}", ErrorMessage = "Incorrect format")]
        public string CustomText
        {
            get => _customText;
            set
            {
                if (_customText != value)
                {
                    _customText = value;
                    RaisePropertyChanged(nameof(CustomText));
                }
            }
        }

        private void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
