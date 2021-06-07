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
    public class ValidacijaTelefon : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex regex = new Regex(@"^\d{9,}$");
                if (string.IsNullOrWhiteSpace(s)) return new ValidationResult(false, "Morate uneti broj telefona");
                if (regex.IsMatch(s))
                {
                    return new ValidationResult(true, "");
                }

                return new ValidationResult(false, "Pogresan unos!\nBroj telefona sadrzi najmanje 9 cifara");
            }
            catch
            {
                return new ValidationResult(false, "Doslo je do nepoznate greske");
            }
        }
    }
}
