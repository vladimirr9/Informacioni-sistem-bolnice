using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InformacioniSistemBolnice.Converter
{
    public class AppointmentTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                AppointmentType appType = (AppointmentType)value;
                if (appType == AppointmentType.generalPractitionerCheckup)
                    return "Pregled - opšta praksa";
                else if (appType == AppointmentType.specialistCheckup)
                    return "Pregled - specijalista";
                else return "Operacija";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
