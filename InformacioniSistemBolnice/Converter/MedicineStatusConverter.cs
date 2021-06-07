using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InformacioniSistemBolnice.Converter
{
    class MedicineStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                MedicineStatus medStatus = (MedicineStatus)value;
                if (medStatus == MedicineStatus.validated)
                    return "Validiran";
                else if (medStatus == MedicineStatus.rejected)
                    return "Odbijen";
                else if (medStatus == MedicineStatus.waitingForValidation)
                    return "Čeka na validaciju";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
