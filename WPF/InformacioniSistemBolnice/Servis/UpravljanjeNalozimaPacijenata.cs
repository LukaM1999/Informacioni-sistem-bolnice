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
          Lazy =new(() => new UpravljanjeNalozimaPacijenata());
        public static UpravljanjeNalozimaPacijenata Instance => Lazy.Value;

        private PacijentDto pacijentoviPodaci;
        private Pacijent pacijentZaIzmenu;

        public void KreiranjeNaloga(PacijentDto pacijentDto)
        {
            Pacijent pacijent = new Pacijent(new Osoba(pacijentDto.ime, pacijentDto.prezime, pacijentDto.jmbg,
                                            DateTime.Parse(pacijentDto.datumRodjenja.ToString("g")), pacijentDto.telefon, pacijentDto.email,
                                            KreirajKorisnika(pacijentDto),
                                            new Adresa(pacijentDto.drzava, pacijentDto.grad, pacijentDto.ulica, pacijentDto.broj)));
            Pacijenti.Instance.DodajPacijenta(pacijent);
        }


        private Korisnik KreirajKorisnika(PacijentDto pacijentDto)
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
            Pacijenti.Instance.Serijalizacija();
            Korisnici.Instance.Serijalizacija();
        }

        public void IzmenaNaloga(PacijentDto pacijentDto, Pacijent pacijent)
        {
            pacijentoviPodaci = pacijentDto;
            pacijentZaIzmenu = pacijent;
            IzmeniLicnePodatke();
            IzmeniAdresu();
            IzmeniKorisnickePodatke();
            SacuvajURepozitorijum();
        }

        private void IzmeniKorisnickePodatke()
        {
            Korisnik korisnikZaIzmenu = pacijentZaIzmenu.korisnik;
            korisnikZaIzmenu.korisnickoIme = pacijentoviPodaci.korisnickoIme; 
            korisnikZaIzmenu.lozinka = pacijentoviPodaci.lozinka;
        }

        private void IzmeniAdresu()
        {
            Adresa adresaZaIzmenu = pacijentZaIzmenu.adresa;
            adresaZaIzmenu.Drzava = pacijentoviPodaci.drzava;
            adresaZaIzmenu.Grad = pacijentoviPodaci.grad;
            adresaZaIzmenu.Ulica = pacijentoviPodaci.ulica;
            adresaZaIzmenu.Broj = pacijentoviPodaci.broj;
        }

        private void IzmeniLicnePodatke()
        {
            pacijentZaIzmenu.ime = pacijentoviPodaci.ime;
            pacijentZaIzmenu.prezime = pacijentoviPodaci.prezime;
            pacijentZaIzmenu.jmbg = pacijentoviPodaci.jmbg;
            pacijentZaIzmenu.datumRodjenja = pacijentoviPodaci.datumRodjenja;
            pacijentZaIzmenu.telefon = pacijentoviPodaci.telefon;
            pacijentZaIzmenu.email = pacijentoviPodaci.email;
        }
    }

}



