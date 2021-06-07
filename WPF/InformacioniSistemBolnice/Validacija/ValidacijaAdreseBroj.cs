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
    public class ValidacijaAdreseBroj : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                
                var s = value as string;
                Regex regex = new Regex(@"^\d{1,5}$");
                if (string.IsNullOrWhiteSpace(s)) return new ValidationResult(false, "Nepotpuna adresa!\n Unesite broj");

                if (regex.IsMatch(s))
                {
                    return new ValidationResult(true, "");
                }
                return new ValidationResult(false, "Pogresan unos!\n Moguce je uneti samo cifre!");
            }
            catch
            {
                return new ValidationResult(false, "Greska!");
            }
        }
    }
}
