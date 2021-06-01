using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InformacioniSistemBolnice.Converter
{
    class AppointmentStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                AppointmentStatus appStatus = (AppointmentStatus)value;
                if (appStatus == AppointmentStatus.scheduled)
                    return "Zakazan";
                else if (appStatus == AppointmentStatus.missed)
                    return "Propušten";
                else if (appStatus == AppointmentStatus.finished)
                    return "Završen";
                else return "Otkazan";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
