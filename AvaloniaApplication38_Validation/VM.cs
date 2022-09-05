using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AvaloniaApplication38_Validation
{
    public class VM : ReactiveObject
    {
        public VM()
        {
            _customText = "00:00:00.000";
        }

        private const string regex = @"^(\d){2}:(\d){2}:(\d){2}\.(\d){3}$";
        private string _customText;
        //[RegularExpression(@"(\d){2}:(\d){2}:(\d){2}\.(\d){3}")]
        [RegularExpression(regex, ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.CustomError))]
        public string CustomText
        {
            get => _customText;
            set
            {
                var r = new Regex(regex);

                //if (r.IsMatch(value))
                {
                    this.RaiseAndSetIfChanged(ref _customText, value);
                }
            }
        }
    }
}
