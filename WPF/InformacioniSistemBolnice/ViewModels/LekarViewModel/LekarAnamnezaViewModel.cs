using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice.ViewModels.LekarViewModel
{
    class LekarAnamnezaViewModel
    {
        public GlavniProzorLekara pocetna;
        public Model.Pacijent Pacijent { get; set; }
        public Anamneza Anamneza { get; set; }
        public ICommand Potvrdi { get; set; }
        public ICommand Nazad { get; set; }


        public LekarAnamnezaViewModel(Model.Pacijent prosledjen, GlavniProzorLekara glavni)
        {
            pocetna = glavni;
            Pacijent = prosledjen;
            if (Pacijent.zdravstveniKarton.Anamneza == null) return;
            Anamneza = Pacijent.zdravstveniKarton.Anamneza;
            Potvrdi = new Command(o => Potvrda());
            Nazad = new Command(o => Odustani());
        }

        private void Potvrda()
        {
            AnamnezaDto anamneza =
                new(Pacijent.Jmbg, Anamneza.SadasnjaBolest, Anamneza.RanijeBolesti, Anamneza.PorodicneAnamneze, Anamneza.Zakljucak);
            ZdravstveniKartonKontroler.Instance.DodavanjeAnamneze(anamneza);
            Odustani();
        }

        private void Odustani()
        {
            Karton karton = new Karton() { DataContext = new KartonViewModel(pocetna, Pacijent.Jmbg) };
            pocetna.contentControl.Content = karton;
        }
    }
}
