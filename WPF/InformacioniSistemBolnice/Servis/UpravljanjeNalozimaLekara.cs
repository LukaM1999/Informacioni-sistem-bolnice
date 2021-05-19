using InformacioniSistemBolnice;
using Model;
using Repozitorijum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class UpravljanjeNalozimaLekara
    {
        private static readonly Lazy<UpravljanjeNalozimaLekara>
           lazy =
           new Lazy<UpravljanjeNalozimaLekara>
               (() => new UpravljanjeNalozimaLekara());

        public static UpravljanjeNalozimaLekara Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(LekarDto lekarDto)
        {
            Korisnik korisnik = KreiranjeKorisnika(lekarDto);
            KreiranjeLekara(lekarDto, korisnik);
        }

        private static Korisnik KreiranjeKorisnika(LekarDto lekarDto)
        {
            Korisnik korisnik = new(lekarDto.korisnickoIme, lekarDto.lozinka,
                                    (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "lekar"));
            Korisnici.Instance.DodajKorisnika(korisnik);
            return korisnik;
        }
        private static void KreiranjeLekara(LekarDto lekarDto, Korisnik korisnik)
        {
            Lekar lekar = new Lekar(new Osoba(lekarDto.ime, lekarDto.prezime, lekarDto.jmbg,
                                    DateTime.Parse(lekarDto.datumRodjenja.ToString()), lekarDto.telefon, lekarDto.email, korisnik,
                                    new Adresa(lekarDto.drzava, lekarDto.grad, lekarDto.ulica, lekarDto.broj)),
                                    new Specijalizacija(lekarDto.specijalizacija));
            ObservableCollection<Termin> zauzetiTermini = new ObservableCollection<Termin>();
            lekar.zauzetiTermini = zauzetiTermini;
            Lekari.Instance.DodajLekara(lekar);
        }
        private static void SacuvajURepozitorijum()
        {
            Korisnici.Instance.SacuvajPromene();
            Lekari.Instance.SacuvajPromene();
        }
        public void UklanjanjeNaloga(Lekar lekar)
        {
            Lekari.Instance.ObrisiLekara(lekar);
            Korisnici.Instance.ObrisiKorisnika(lekar.korisnik);
            SacuvajURepozitorijum();
        }
        

        public void IzmenaNaloga(LekarDto lekarDto, Lekar lekar)
        {
            IzmeniLicnePodatke(lekarDto, lekar);
            IzmeniAdresu(lekarDto, lekar);
            IzmeniKorisnickePodatke(lekarDto, lekar);
            IzmeniSpecijalizaciju(lekarDto, lekar);
            SacuvajURepozitorijum();
        }

        private static void IzmeniSpecijalizaciju(LekarDto lekarDto, Lekar lekar)
        {
            if (lekarDto.specijalizacija != null) lekar.specijalizacija.Naziv = lekarDto.specijalizacija;
        }

        private static void IzmeniLicnePodatke(LekarDto lekarDto, Lekar lekar)
        {
            lekar.ime = lekarDto.ime;
            lekar.prezime = lekarDto.prezime;
            lekar.jmbg = lekarDto.jmbg;
            lekar.datumRodjenja = lekarDto.datumRodjenja;
            lekar.telefon = lekarDto.telefon;
            lekar.email = lekarDto.email;
        }

        private static void IzmeniKorisnickePodatke(LekarDto lekarDto, Lekar lekar)
        {
            lekar.korisnik.korisnickoIme = lekarDto.korisnickoIme;
            lekar.korisnik.lozinka = lekarDto.lozinka;
            Korisnik korisnik = lekar.korisnik;
        }

        private static void IzmeniAdresu(LekarDto lekarDto, Lekar lekar)
        {
            lekar.adresa.Drzava = lekarDto.drzava;
            lekar.adresa.Grad = lekarDto.grad;
            lekar.adresa.Ulica = lekarDto.ulica;
            lekar.adresa.Broj = lekarDto.broj;
        }

        public LekarDto PregledNaloga(Lekar lekar)
        {
            LekarDto lekarDto = new LekarDto();
            PregledLicnihPodataka(lekarDto, lekar);
            PregledKorisnickihPodataka(lekarDto, lekar);
            PregledAdrese(lekar, lekarDto);
            return lekarDto;
        }

        private static void PregledKorisnickihPodataka(LekarDto lekarDto, Lekar lekar)
        {
            lekarDto.korisnickoIme = lekar.korisnik.korisnickoIme;
            lekarDto.lozinka = lekar.korisnik.lozinka;
        }

        private static void PregledAdrese(Lekar lekar, LekarDto lekarDto)
        {
            lekarDto.drzava = lekar.adresa.Drzava;
            lekarDto.ulica = lekar.adresa.Ulica;
            lekarDto.grad = lekar.adresa.Grad;
            lekarDto.broj = lekar.adresa.Broj;
        }

        private static void PregledLicnihPodataka(LekarDto lekarDto, Lekar lekar)
        {
            lekarDto.ime = lekar.ime;
            lekarDto.prezime = lekar.prezime;
            lekarDto.jmbg = lekar.jmbg;
            lekarDto.telefon = lekar.telefon;
            lekarDto.email = lekar.email;
            lekarDto.datumRodjenja = lekar.datumRodjenja;
            lekarDto.specijalizacija = lekar.specijalizacija.Naziv;
        }
    }
}
