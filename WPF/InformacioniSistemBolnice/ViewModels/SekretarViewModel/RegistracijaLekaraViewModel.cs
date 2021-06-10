using Model;
using Repozitorijum;
using System.Collections.ObjectModel;
using Kontroler;
using InformacioniSistemBolnice.DTO;
using System.Windows.Input;
using System;
using System.Windows;
using InformacioniSistemBolnice.Validacija;
using System.Windows.Controls;
using PropertyChanged;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class RegistracijaLekaraViewModel 
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Specijalizacija> Specijalizacije { get; set; }
        public Lekar NoviLekar { get; set; }
        public Korisnik NoviKorisnik { get; set; }
        public Adresa AdresaLekara { get; set; }
        public Specijalizacija SpecijalizacijaLekara { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand RegistrujLekara { get; set; }
        public ICommand ValidirajIme { get; set; }
        public ICommand ValidirajPrezime { get; set; }
        public ICommand ValidirajJMBG { get; set; }
        public ICommand ValidirajTelefon{ get; set; }
        public ICommand ValidirajEmailAdresu { get; set; }
        public ValidationResult IspravnostImena { get; set; }
        public ValidationResult IspravnostPrezimena { get; set; }
        public ValidationResult IspravnostJMBG { get; set; }
        public ValidationResult IspravnostTelefona { get; set; }
        public ValidationResult IspravnostEmailAdrese { get; set; }

        private Context _context;

        public RegistracijaLekaraViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            pocetna = pocetnaStranicaSekretara;
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            Specijalizacije = SpecijalizacijaRepo.Instance.Specijalizacije;
            NoviKorisnik = new Korisnik(null, null, UlogaKorisnika.pacijent);
            AdresaLekara = new Adresa();
            SpecijalizacijaLekara = new Specijalizacija();
            NoviLekar = new Lekar(AdresaLekara, NoviKorisnik);
            NoviLekar.DatumRodjenja = DateTime.Parse("6/1/2021");
            RegistrujLekara = new Command(o => RegistracijaLekara(), o => ValidanUnos());
            Nazad = new Command(o => PovratakNazad());
            ValidirajIme = new Command(o => ValidacijaImena());
            ValidirajPrezime = new Command(o => ValidacijaPrezimena());
            ValidirajJMBG = new Command(o => ValidacijaJMBG());
            ValidirajTelefon = new Command(o => ValidacijaTelefona());
            ValidirajEmailAdresu = new Command(o => ValidacijaEmailAdrese());
           _context = new Context();
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private bool RegistracijaLekara()
        {
            LekarRepo.Instance.Deserijalizacija();
            LekarDto lekarDto = PokupiPodatkeSaForme();
            if (NalogLekaraKontroler.Instance.KreiranjeNalogaLekara(lekarDto)) {
                pocetna.contentControl.Content = new Lekari(pocetna);
                return true;
            }
            MessageBox.Show("Korisnik sa unetim korisnickim imenom vec postoji!");
            return false;
        }


        private LekarDto PokupiPodatkeSaForme()
        {
            return new LekarDto(NoviLekar.Ime, NoviLekar.Prezime, NoviLekar.Jmbg, NoviLekar.DatumRodjenja,
                                                         NoviLekar.AdresaStanovanja.Drzava, NoviLekar.AdresaStanovanja.Grad,
                                                         NoviLekar.AdresaStanovanja.Ulica, NoviLekar.AdresaStanovanja.Broj,
                                                         NoviLekar.Telefon, NoviLekar.Email, NoviLekar.Korisnik.KorisnickoIme,
                                                         NoviLekar.Korisnik.Lozinka, SpecijalizacijaLekara.Naziv);
        }

        private bool ValidanUnos()
        {
            if (NoviLekar.Ime is null || NoviLekar.Prezime is null || NoviLekar.Jmbg is null ||
                NoviLekar.DatumRodjenja.Equals(default(DateTime)) || NoviLekar.AdresaStanovanja.Drzava is null ||
                NoviLekar.AdresaStanovanja.Grad is null || NoviLekar.AdresaStanovanja.Ulica is null ||
                NoviLekar.AdresaStanovanja.Broj is null || NoviLekar.Telefon is null ||
                NoviLekar.Email is null || NoviLekar.Korisnik.KorisnickoIme is null ||
                NoviLekar.Korisnik.Lozinka is null || SpecijalizacijaLekara.Naziv is null) return false;
            return true;
        }

        private void ValidacijaImena()
        {
            _context.PostaviValidaciju(new ValidacijaIme());
            IspravnostImena = _context.Validiraj(NoviLekar.Ime, null);
        }

        private void ValidacijaPrezimena()
        {
            _context.PostaviValidaciju(new ValidacijaPrezime());
            IspravnostPrezimena = _context.Validiraj(NoviLekar.Prezime, null);
        }

        private void ValidacijaJMBG()
        {
            _context.PostaviValidaciju(new ValidacijaJMBG());
            IspravnostJMBG = _context.Validiraj(NoviLekar.Jmbg, null);
        }

        private void ValidacijaTelefona()
        {
            _context.PostaviValidaciju(new ValidacijaTelefon());
            IspravnostTelefona = _context.Validiraj(NoviLekar.Telefon, null);
        }

        private void ValidacijaEmailAdrese()
        {
            _context.PostaviValidaciju(new ValidacijaEmail());
            IspravnostEmailAdrese = _context.Validiraj(NoviLekar.Email, null);
        }
    }
}
