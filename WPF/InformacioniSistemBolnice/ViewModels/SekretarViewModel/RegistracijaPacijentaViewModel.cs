using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class RegistracijaPacijentaViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        public ICommand Nazad { get; set; }
        public ICommand RegistrujPacijenta { get; set; }
        public Model.Pacijent NoviPacijent { get; set; }
        public Osoba NovaOsoba { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public Korisnik NoviKorisnik { get; set; }
        public Adresa NovaAdresa { get; set; }
       
        public RegistracijaPacijentaViewModel(PacijentiProzor pacijentiProzor)
        {
            this.pacijentiProzor = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
            NoviKorisnik = new Korisnik( " ", " ", UlogaKorisnika.pacijent);
            NovaAdresa = new Adresa();
            NovaOsoba = new Osoba(NovaAdresa, NoviKorisnik);
            NoviPacijent = new (NovaOsoba);
            RegistrujPacijenta = new Command(o => RegistracijaPacijenta());
            Nazad = new Command(o => PovratakNazad());
        }

        private void RegistracijaPacijenta()
        {
            PacijentDto registracijaPacijentaDto = new(NoviPacijent.Ime, NoviPacijent.Prezime, NoviPacijent.Jmbg,
                                                NoviPacijent.DatumRodjenja, NoviPacijent.Telefon, NoviPacijent.Email,
                                                NoviPacijent.Korisnik.KorisnickoIme, NoviPacijent.Korisnik.Lozinka,
                                                NoviPacijent.AdresaStanovanja.Drzava, NoviPacijent.AdresaStanovanja.Grad,
                                                NoviPacijent.AdresaStanovanja.Ulica, NoviPacijent.AdresaStanovanja.Broj);
            SekretarKontroler.Instance.KreiranjeNaloga(registracijaPacijentaDto);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);

        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = pacijentiProzor.Content;
        }
    }
}
