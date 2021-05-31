using Model;
using Repozitorijum;
using System.Collections.ObjectModel;
using Kontroler;
using InformacioniSistemBolnice.DTO;
using System.Windows.Input;

namespace InformacioniSistemBolnice.ViewModels.SekretarViewModel
{
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
        public RegistracijaLekaraViewModel(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            pocetna = pocetnaStranicaSekretara;
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            Specijalizacije = SpecijalizacijaRepo.Instance.Specijalizacije;
            NoviKorisnik = new Korisnik(" ", " ", UlogaKorisnika.pacijent);
            AdresaLekara = new Adresa();
            SpecijalizacijaLekara = new Specijalizacija();
            NoviLekar = new Lekar(AdresaLekara, NoviKorisnik);
            RegistrujLekara = new Command(o => RegistracijaLekara());
            Nazad = new Command(o => PovratakNazad());
        }

        private void PovratakNazad()
        {
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private void RegistracijaLekara()
        {
            LekarDto lekarDto = PokupiPodatkeSaForme();
            SekretarKontroler.Instance.KreiranjeNalogaLekara(lekarDto);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private LekarDto PokupiPodatkeSaForme()
        {
            return new LekarDto(NoviLekar.Ime, NoviLekar.Prezime, NoviLekar.Jmbg, NoviLekar.DatumRodjenja,
                                                         NoviLekar.AdresaStanovanja.Drzava, NoviLekar.AdresaStanovanja.Grad,
                                                         NoviLekar.AdresaStanovanja.Ulica, NoviLekar.AdresaStanovanja.Broj,
                                                        NoviLekar.Telefon, NoviLekar.Email, NoviLekar.Korisnik.KorisnickoIme
                                                        , NoviLekar.Korisnik.Lozinka, SpecijalizacijaLekara.Naziv);
        }
    }
}
