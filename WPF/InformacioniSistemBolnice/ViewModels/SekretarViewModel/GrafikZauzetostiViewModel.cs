using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class GrafikZauzetostiViewModel
    {
        public PocetnaStranicaSekretara pocetnaStranica;
        public ObservableCollection<GrafZauzetostiDto> ParoviVremeBrojTermina { get; set; }

        public GrafikZauzetostiViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            pocetnaStranica = pocetnaStranicaSekretara;
            ParoviVremeBrojTermina = new();
        }



    }
}
