using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InformacioniSistemBolnice
{
    public class SchedulerViewTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string) switch
            {
                "Dnevni pogled" => "Day",
                "Nedeljni pogled" => "Week",
                "Mesečni pogled" => "Month",
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string) switch
            {
                "Day" => "Dnevni pogled",
                "Week" => "Nedeljni pogled",
                "Month" => "Mesečni pogled",
                _ => null
            };
        }
    }
}
