using Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledVestiViewModel
    {
        public VestiProzor vesti;

        public PocetnaStranicaSekretara pocetna;

        public ICommand Nazad { get; set; }

        public Vest Vest { get; set; }

        public PregledVestiViewModel(VestiProzor vesti, Vest izabranaVest)
        {
            Vest = izabranaVest;
            this.vesti = vesti;
            pocetna = vesti.menu.pocetna;
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad() 
        {
            pocetna.contentControl.Content = vesti.Content;
        }

    }
}
