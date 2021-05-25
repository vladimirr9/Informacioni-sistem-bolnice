using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Validation
{
    public class JMBGValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string s = value.ToString();
                long result;
                if (long.TryParse(s, out result) && s.Length == 13)
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "JMBG se sastoji iz 13 cifara.");
                
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }


    }
}
