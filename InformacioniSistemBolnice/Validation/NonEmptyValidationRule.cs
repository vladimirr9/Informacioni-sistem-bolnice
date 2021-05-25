using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Validation
{
    class NonEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string strValue = value.ToString();
                if (strValue.Length == 0)
                {
                    return new ValidationResult(false, "Vrednost se mora popuniti");
                }
                return new ValidationResult(true, null);

            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }
}
