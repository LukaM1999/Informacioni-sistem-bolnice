using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Utilities
{
    public class DrzavaUtility
    {
        public static List<string> DobaviSveDrzave()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var rez = cultures.Select(cult => new RegionInfo(cult.LCID).NativeName)
                .Distinct().OrderBy(q => q).ToList();
            return rez;
        }
    }
}
