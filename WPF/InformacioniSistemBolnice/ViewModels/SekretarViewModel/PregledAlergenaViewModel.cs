using Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledAlergenaViewModel
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public Alergen Alergen { get; set; }
        public ICommand Nazad { get; set; }

        public PregledAlergenaViewModel(AlergeniProzor alergeniProzor, Alergen izabraniAlergen)
        {
            Alergen = izabraniAlergen;
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }
    }
}
