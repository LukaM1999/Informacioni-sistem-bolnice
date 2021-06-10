using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Servis;
using Model;

namespace InformacioniSistemBolnice.ViewModels.Pacijent
{
    public class IzborTerminaPacijentaViewModel
    {
        public ObservableCollection<Termin> PredlozeniTermini { get; set; }

        public IzborTerminaPacijentaViewModel()
        {
        }
    }
}
