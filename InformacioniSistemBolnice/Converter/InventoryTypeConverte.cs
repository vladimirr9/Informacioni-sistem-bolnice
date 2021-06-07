using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InformacioniSistemBolnice.Converter
{
    class InventoryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                InventoryType invType = (InventoryType)value;
                if (invType == InventoryType.staticInv)
                    return "Statička";
                else if (invType == InventoryType.dinamicInv)
                    return "Dinamička";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
