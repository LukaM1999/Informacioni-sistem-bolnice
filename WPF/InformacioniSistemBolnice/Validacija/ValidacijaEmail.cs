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
    public class ValidacijaEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {

                var s = value as string;
                Regex regex = new Regex(@"\b[\w.-]+@[\w.-]+(\.[\w.-]+)*\.[A-Za-z]{2,4}\b");
                if (string.IsNullOrWhiteSpace(s)) return new ValidationResult(false, "Morate uneti email adresu");

                if (regex.IsMatch(s))
                {
                    return new ValidationResult(true, "");
                }
                return new ValidationResult(false, "Pogresan format email adrese!");
            }
            catch
            {
                return new ValidationResult(false, "Greska!");
            }
        }
    }
}
