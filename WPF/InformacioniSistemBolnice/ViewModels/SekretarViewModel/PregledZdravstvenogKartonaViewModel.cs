using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class PregledZdravstvenogKartonaViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public Model.Pacijent Pacijent { get; set; }
        public ICommand Nazad { get; set; }


        public PregledZdravstvenogKartonaViewModel(PacijentiProzor pacijentiProzor, Model.Pacijent izabraniPacijent)
        {
            ZdravstveniKarton = izabraniPacijent.zdravstveniKarton;
            Pacijent = izabraniPacijent;
            this.pacijentiProzor = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

    }
}
