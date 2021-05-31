using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class DefinisanjeAlergenaViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public ICommand Nazad { get; set; }
        public ICommand DefinisiAlergen { get; set; }
        public Alergen NoviAlergen { get; set; }


        public DefinisanjeAlergenaViewModel(AlergeniProzor alergeniProzor)
        {
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
            NoviAlergen = new Alergen(" ");
            DefinisiAlergen = new Command(o => DefinisanjeAlergena());
            Nazad = new Command(o => PovratakNazad());
        }

        private void DefinisanjeAlergena()
        {
            AlergenDto alergenDto = new(NoviAlergen.Naziv);
            SekretarKontroler.Instance.DefinisanjeAlergena(alergenDto);
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }
    }
}
