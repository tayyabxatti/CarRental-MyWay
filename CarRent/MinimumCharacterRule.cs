using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarRent.ViewModels
{
    public class MinimumCharacterRule : ValidationRule
    {
        public int MinimumCharacter { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if(charString.Length< MinimumCharacter)
            {
                return new ValidationResult(false, $"User atleast {MinimumCharacter} characters.");
                
            }
            return new ValidationResult(true, null);
            
        }
    }


}
