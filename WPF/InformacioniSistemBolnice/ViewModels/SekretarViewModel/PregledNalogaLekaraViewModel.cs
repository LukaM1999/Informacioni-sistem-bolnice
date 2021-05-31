using Model;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledNalogaLekaraViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public Lekari lekari;
        public Lekar Lekar { get; set; }
         public ICommand Nazad { get; set; }

        public PregledNalogaLekaraViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara, Lekari uCLekari, Lekar izabraniLekar)
        {
            Lekar = izabraniLekar;
            pocetna = pocetnaStranicaSekretara;
            lekari = uCLekari;
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new Lekari(pocetna);
        }
    }
}
