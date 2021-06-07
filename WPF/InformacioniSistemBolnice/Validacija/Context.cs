using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Validacija
{
    public class Context
    {
        private ValidationRule _validacija;

        public Context() { }
       
        public Context(ValidationRule validacija)
        {
            _validacija = validacija;
        }

        public void PostaviValidaciju(ValidationRule validacija)
        {
             _validacija = validacija;
        }

        public ValidationResult Validiraj(object value, CultureInfo cultureInfo)
        {
            return _validacija.Validate(value, cultureInfo);
        }

    }
}
