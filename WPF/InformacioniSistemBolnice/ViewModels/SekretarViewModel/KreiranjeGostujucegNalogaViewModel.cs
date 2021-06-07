using InformacioniSistemBolnice.DTO;
using Kontroler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class KreiranjeGostujucegNalogaViewModel
    {
        public GostujuciNaloziProzor gostujuciNaloziProzor;
        public PocetnaStranicaSekretara pocetna;
        public Model.Pacijent GostujuciPacijent { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand KreirajGostujuciNalog { get; set; }
        public KreiranjeGostujucegNalogaViewModel(GostujuciNaloziProzor gostujuciNalozi)
        {
            gostujuciNaloziProzor = gostujuciNalozi;
            pocetna = gostujuciNaloziProzor.pocetna;
            GostujuciPacijent = new Model.Pacijent();
            KreirajGostujuciNalog = new Command(o => KreiranjeGostujucegNaloga(), o => ValidanUnos());
            Nazad = new Command(o => PovratakNazad());
        }

        private void KreiranjeGostujucegNaloga()
        {

            GostujuciNalogDto gostujuciNalogDto = new GostujuciNalogDto(GostujuciPacijent.Ime, GostujuciPacijent.Prezime,
                                                    GostujuciPacijent.Jmbg, GostujuciPacijent.DatumRodjenja,
                                                    GostujuciPacijent.Telefon, GostujuciPacijent.Email);
            NalogPacijentaKontroler.Instance.KreiranjeGostujucegPacijenta(gostujuciNalogDto);
            pocetna.contentControl.Content = new GostujuciNaloziProzor(gostujuciNaloziProzor.pocetna);

        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = gostujuciNaloziProzor.Content;
        }

        private bool ValidanUnos()
        {
            if (GostujuciPacijent.Ime is null || GostujuciPacijent.Prezime is null || GostujuciPacijent.Jmbg is null ||
                GostujuciPacijent.DatumRodjenja.Equals(default(DateTime)) || GostujuciPacijent.Telefon is null ||
                GostujuciPacijent.Email is null ) return false;
            return true;
        }
    }
}
