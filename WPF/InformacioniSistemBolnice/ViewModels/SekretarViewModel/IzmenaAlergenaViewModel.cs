using System.Windows.Controls;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class IzmenaAlergenaViewModel
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public Alergen Alergen { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand IzmeniAlergen { get; set; }

        public IzmenaAlergenaViewModel(AlergeniProzor alergeniProzor, Alergen izabraniAlergen)
        {
            Alergen = izabraniAlergen;
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            listaAlergena = alergeniProzor.ListaAlergena;
            Nazad = new Command(o => PovratakNazad());
            IzmeniAlergen = new Command(o => IzmenaAlergena());
        }

        private void IzmenaAlergena()
        {
            AlergenKontroler.Instance.IzmenaAlergena(new AlergenDto(Alergen.Naziv), (Alergen)listaAlergena.SelectedItem);
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }
    }
}
