using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Utilities
{
    public class PronadjiProzorUtility
    {
        public Window PronadjiProzor(object viewModel)
        {
            foreach (Window prozor in Application.Current.Windows) 
                if (prozor.DataContext == viewModel) return prozor;
            return null;
        }
    }
}
