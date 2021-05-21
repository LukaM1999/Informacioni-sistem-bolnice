using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Collections.ObjectModel;
using System.Linq;

namespace Servis
{
    public class UpravljanjeNalozimaPacijenata
    {
        private static readonly Lazy<UpravljanjeNalozimaPacijenata>
          lazy =
          new Lazy<UpravljanjeNalozimaPacijenata>
              (() => new UpravljanjeNalozimaPacijenata());

        public static UpravljanjeNalozimaPacijenata Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(PacijentDto pacijentDto)
        {
            Pacijent pacijent = new Pacijent(new Osoba(pacijentDto.ime, pacijentDto.prezime, pacijentDto.jmbg,
                                            DateTime.Parse(pacijentDto.datumRodjenja.ToString()), pacijentDto.telefon, pacijentDto.email,
                                            KreirajKOrisnika(pacijentDto),
                                            new Adresa(pacijentDto.drzava, pacijentDto.grad, pacijentDto.ulica, pacijentDto.broj)));
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            pacijent.zakazaniTermini = zakazaniTermini;
            Pacijenti.Instance.DodajPacijenta(pacijent);
        }


        private static Korisnik KreirajKOrisnika(PacijentDto pacijentDto)
        {
            Korisnik korisnik = new Korisnik(pacijentDto.korisnickoIme, pacijentDto.lozinka,
                                            (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"));
            Korisnici.Instance.DodajKorisnika(korisnik);
            return korisnik;
        }

        public void UklanjanjeNaloga(Pacijent pacijentZaBrisanje)
        {
            Pacijenti.Instance.ObrisiPacijenta(pacijentZaBrisanje);
            Korisnici.Instance.ObrisiKorisnika(pacijentZaBrisanje.korisnik);
            SacuvajURepozitorijum();
        }

        private static void SacuvajURepozitorijum()
        {
            Pacijenti.Instance.SacuvajPromene();
            Korisnici.Instance.SacuvajPromene();
        }

        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
        {
            IzmeniLicnePodatke(pacijentDto, pacijent);
            IzmeniAdresu(pacijentDto, pacijent);
            IzmeniKorisnickePodatke(pacijentDto, pacijent);
            SacuvajURepozitorijum();

        }

        private static void IzmeniKorisnickePodatke(PacijentDto pacijentDto, Pacijent pacijent)
        {
            pacijent.korisnik.korisnickoIme = pacijentDto.korisnickoIme;
            pacijent.korisnik.lozinka = pacijentDto.lozinka;
            Korisnik korisnik = pacijent.korisnik;
        }

        private static void IzmeniAdresu(PacijentDto pacijentDto, Pacijent pacijent)
        {
            pacijent.adresa.Drzava = pacijentDto.drzava;
            pacijent.adresa.Grad = pacijentDto.grad;
            pacijent.adresa.Ulica = pacijentDto.ulica;
            pacijent.adresa.Broj = pacijentDto.broj;
        }

        private static void IzmeniLicnePodatke(PacijentDto pacijentDto, Pacijent pacijent)
        {
            pacijent.ime = pacijentDto.ime;
            pacijent.prezime = pacijentDto.prezime;
            pacijent.jmbg = pacijentDto.jmbg;
            pacijent.datumRodjenja = pacijentDto.datumRodjenja;
            pacijent.telefon = pacijentDto.telefon;
            pacijent.email = pacijentDto.email;
        }
    }

}



