using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Validacija
{
    public class ValidacijaJMBG : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex regex = new Regex(@"\d{13}");
                if (string.IsNullOrWhiteSpace(s)) return new ValidationResult(false, "Morate uneti JMBG");

                if (regex.IsMatch(s))
                {
                    return new ValidationResult(true, "");
                }
                return new ValidationResult(false, "Pogresan unos!\n JMBG sadrzi 13 cifara");
            }
            catch
            {
                return new ValidationResult(false, "Doslo je do nepoznate greske");
            }
        }
    }
}
