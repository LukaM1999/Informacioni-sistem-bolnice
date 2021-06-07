using InformacioniSistemBolnice;
using Model;
using Repozitorijum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class LekarServis
    {
        private static readonly Lazy<LekarServis> Lazy = new(() => new LekarServis());
        public static LekarServis Instance => Lazy.Value;

        public bool KreirajNalog(LekarDto lekarDto)
        {
            Lekar lekar = new(new Osoba(lekarDto.Ime, lekarDto.Prezime, lekarDto.LekarJmbg,
                                        DateTime.Parse(lekarDto.DatumRodjenja.ToString("g")),
                                        lekarDto.Telefon, lekarDto.Email, KreirajKorisnika(lekarDto),
                                        new Adresa(lekarDto.Drzava, lekarDto.Grad, lekarDto.Ulica, lekarDto.Broj)),
                                        new Specijalizacija(lekarDto.Specijalizacija));
            if (LekarRepo.Instance.DodajLekara(lekar)) return true;
            return false;
        }

        private Korisnik KreirajKorisnika(LekarDto lekarDto)
        {
            Korisnik korisnik = new(lekarDto.KorisnickoIme, lekarDto.Lozinka,
                                    (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "lekar"));
            KorisnikRepo.Instance.DodajKorisnika(korisnik);
            return korisnik;
        }

        public void UkloniNalog(Lekar lekar)
        {
            LekarRepo.Instance.ObrisiLekara(lekar);
            KorisnikRepo.Instance.ObrisiKorisnika(lekar.Korisnik);
            SacuvajURepozitorijum();
        }

        public void IzmeniNalog(LekarDto lekarDto, Lekar lekar)
        {
            IzmeniLicnePodatke(lekarDto, lekar);
            IzmeniAdresu(lekarDto, lekar);
            IzmeniKorisnickePodatke(lekarDto, lekar);
            IzmeniSpecijalizaciju(lekarDto, lekar);
            SacuvajURepozitorijum();
        }

        private void IzmeniSpecijalizaciju(LekarDto lekarDto, Lekar lekar)
        {
            if (lekarDto.Specijalizacija != null) 
                lekar.Specijalizacija.Naziv = lekarDto.Specijalizacija;
        }

        private void IzmeniLicnePodatke(LekarDto lekarDto, Lekar lekar)
        {
            lekar.Ime = lekarDto.Ime;
            lekar.Prezime = lekarDto.Prezime;
            lekar.Jmbg = lekarDto.LekarJmbg;
            lekar.DatumRodjenja = lekarDto.DatumRodjenja;
            lekar.Telefon = lekarDto.Telefon;
            lekar.Email = lekarDto.Email;
        }

        private void IzmeniKorisnickePodatke(LekarDto lekarDto, Lekar lekar)
        {
            Korisnik korisnik = lekar.Korisnik;
            korisnik.KorisnickoIme = lekarDto.KorisnickoIme;
            korisnik.Lozinka = lekarDto.Lozinka;
        }

        private void IzmeniAdresu(LekarDto lekarDto, Lekar lekar)
        {
            Adresa adresa = lekar.AdresaStanovanja;
            adresa.Drzava = lekarDto.Drzava;
            adresa.Grad = lekarDto.Grad;
            adresa.Ulica = lekarDto.Ulica;
            adresa.Broj = lekarDto.Broj;
        }

        private void SacuvajURepozitorijum()
        {
            KorisnikRepo.Instance.Serijalizacija();
            LekarRepo.Instance.Serijalizacija();
        }
    }
}
