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
    public class ValidacijaPrezime : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex regex = new Regex(@"^[A-Z]{1}[a-zA-Z\s]+$");
                if (string.IsNullOrWhiteSpace(s)) return new ValidationResult(false, "Morate uneti prezime");

                if (regex.IsMatch(s))
                {
                    return new ValidationResult(true, "");
                }
                return new ValidationResult(false, "Pogresan unos! \n Moguce je uneti samo slova");
            }
            catch
            {
                return new ValidationResult(false, "Greska!");
            }
        }
    }
}
