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
using System.Windows;


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
            RegistrujPacijenta = new Command(o => RegistracijaPacijenta(), o => ValidanUnos());
            Nazad = new Command(o => PovratakNazad());
        }

        private bool RegistracijaPacijenta()
        {
            PacijentDto registracijaPacijentaDto = new(NoviPacijent.Ime, NoviPacijent.Prezime, NoviPacijent.Jmbg,
                                                NoviPacijent.DatumRodjenja, NoviPacijent.Telefon, NoviPacijent.Email,
                                                NoviPacijent.Korisnik.KorisnickoIme, NoviPacijent.Korisnik.Lozinka,
                                                NoviPacijent.AdresaStanovanja.Drzava, NoviPacijent.AdresaStanovanja.Grad,
                                                NoviPacijent.AdresaStanovanja.Ulica, NoviPacijent.AdresaStanovanja.Broj);
            if (NalogPacijentaKontroler.Instance.KreiranjeNaloga(registracijaPacijentaDto))
            {
                pocetna.contentControl.Content = new PacijentiProzor(pocetna);
                return true;
            }
            MessageBox.Show("Korisnik sa unetim korisnickim imenom vec postoji!");
            return false;
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = pacijentiProzor.Content;
        }

        private bool ValidanUnos()
        {
            if (NoviPacijent.Ime is null || NoviPacijent.Prezime is null || NoviPacijent.Jmbg is null ||
                NoviPacijent.DatumRodjenja.Equals(default(DateTime)) || NoviPacijent.AdresaStanovanja.Drzava is null ||
                NoviPacijent.AdresaStanovanja.Grad is null || NoviPacijent.AdresaStanovanja.Ulica is null ||
                NoviPacijent.AdresaStanovanja.Broj is null || NoviPacijent.Telefon is null ||
                NoviPacijent.Email is null || NoviPacijent.Korisnik.KorisnickoIme is null ||
                NoviPacijent.Korisnik.Lozinka is null) return false;
            return true;
        }

    }
}
