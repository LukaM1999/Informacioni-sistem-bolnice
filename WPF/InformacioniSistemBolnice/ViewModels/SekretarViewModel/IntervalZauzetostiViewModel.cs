using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.Views.SekretarView;
using InformacioniSistemBolnice.DTO;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class IntervalZauzetostiViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<GrafZauzetostiDto> ParoviVremeBrojTermina { get; set; }
        public DateTime PocetakIntervala { get; set; }
        public DateTime KrajIntervala { get; set; }
        public ICommand GenerisiGrafZauzetosti { get; set; }

        public IntervalZauzetostiViewModel(PocetnaStranicaSekretara pocetna) 
        {
            GenerisiGrafZauzetosti = new Command(o => GenerisanjeGrafaZauzetosti());
            PocetakIntervala = DateTime.Parse("6/1/2021");
            KrajIntervala = DateTime.Parse("7/1/2021");
            ParoviVremeBrojTermina = new();
            this.pocetna = pocetna;
        }

        private void GenerisanjeGrafaZauzetosti() 
        {
            ParoviVremeBrojTermina = GrafZauzetostiUtility.PrebrojTermine(PocetakIntervala, KrajIntervala);
            pocetna.contentControl.Content = new GrafikZauzetostiLekara() 
            { DataContext = new GrafikZauzetostiViewModel(pocetna) { ParoviVremeBrojTermina = ParoviVremeBrojTermina} };
        }
    }
}
