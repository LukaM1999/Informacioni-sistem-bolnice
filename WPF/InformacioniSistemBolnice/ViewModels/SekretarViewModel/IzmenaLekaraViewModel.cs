using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Repozitorijum;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
    public class IzmenaLekaraViewModel
    {
        public PocetnaStranicaSekretara pocetna;
        public Lekari lekari;
        public ListView listaLekara;
        public Lekar Lekar { get; set; }

        public ObservableCollection<Specijalizacija> Specijalizacije { get; set; }
        public ICommand Nazad { get; set; }
        public ICommand IzmeniLekara { get; set; }
        public IzmenaLekaraViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara,
                                        Lekari uCLekari, Lekar izabraniLekar)
        {
            Lekar = izabraniLekar;
            pocetna = pocetnaStranicaSekretara;
            lekari = uCLekari;
            listaLekara = lekari.ListaLekara;
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            Specijalizacije = SpecijalizacijaRepo.Instance.Specijalizacije;
            IzmeniLekara = new Command(o => IzmenaLekara());
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private void IzmenaLekara() 
        {
            Adresa adresa = Lekar.AdresaStanovanja;
            Korisnik korisnik = Lekar.Korisnik;
            LekarDto lekarDto = new(Lekar.Ime, Lekar.Prezime, Lekar.Jmbg, Lekar.DatumRodjenja, adresa.Drzava, adresa.Grad,
                                    adresa.Ulica, adresa.Broj, Lekar.Telefon, Lekar.Email, korisnik.KorisnickoIme,
                                    korisnik.Lozinka, Lekar.Specijalizacija.Naziv);
            NalogLekaraKontroler.Instance.IzmenaNalogaLekara(lekarDto, (Lekar)listaLekara.SelectedValue);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }
    }
}
