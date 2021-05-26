using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Collections.ObjectModel;
using System.Linq;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class PacijentServis
    {
        private static readonly Lazy<PacijentServis>
          Lazy =new(() => new PacijentServis());
        public static PacijentServis Instance => Lazy.Value;

        private PacijentDto pacijentoviPodaci;
        private Pacijent pacijentZaIzmenu;

        public void KreirajNalog(PacijentDto pacijentDto)
        {
            Pacijent pacijent = new Pacijent(new Osoba(pacijentDto.Ime, pacijentDto.Prezime, pacijentDto.PacijentJmbg,
                                            DateTime.Parse(pacijentDto.DatumRodjenja.ToString("g")), pacijentDto.Telefon, pacijentDto.Email,
                                            KreirajKorisnika(pacijentDto),
                                            new Adresa(pacijentDto.Drzava, pacijentDto.Grad, pacijentDto.Ulica, pacijentDto.Broj)));
            PacijentRepo.Instance.DodajPacijenta(pacijent);
        }


        private Korisnik KreirajKorisnika(PacijentDto pacijentDto)
        {
            Korisnik korisnik = new Korisnik(pacijentDto.KorisnickoIme, pacijentDto.Lozinka,
                                            (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent"));
            KorisnikRepo.Instance.DodajKorisnika(korisnik);
            return korisnik;
        }

        public void UkloniNalog(Pacijent pacijentZaBrisanje)
        {
            PacijentRepo.Instance.ObrisiPacijenta(pacijentZaBrisanje);
            KorisnikRepo.Instance.ObrisiKorisnika(pacijentZaBrisanje.Korisnik);
            SacuvajURepozitorijum();
        }

        private static void SacuvajURepozitorijum()
        {
            PacijentRepo.Instance.Serijalizacija();
            KorisnikRepo.Instance.Serijalizacija();
        }

        public void IzmeniNalog(PacijentDto pacijentDto, Pacijent pacijent)
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
            Korisnik korisnikZaIzmenu = pacijentZaIzmenu.Korisnik;
            korisnikZaIzmenu.KorisnickoIme = pacijentoviPodaci.KorisnickoIme; 
            korisnikZaIzmenu.Lozinka = pacijentoviPodaci.Lozinka;
        }

        private void IzmeniAdresu()
        {
            Adresa adresaZaIzmenu = pacijentZaIzmenu.AdresaStanovanja;
            adresaZaIzmenu.Drzava = pacijentoviPodaci.Drzava;
            adresaZaIzmenu.Grad = pacijentoviPodaci.Grad;
            adresaZaIzmenu.Ulica = pacijentoviPodaci.Ulica;
            adresaZaIzmenu.Broj = pacijentoviPodaci.Broj;
        }

        private void IzmeniLicnePodatke()
        {
            pacijentZaIzmenu.Ime = pacijentoviPodaci.Ime;
            pacijentZaIzmenu.Prezime = pacijentoviPodaci.Prezime;
            pacijentZaIzmenu.Jmbg = pacijentoviPodaci.PacijentJmbg;
            pacijentZaIzmenu.DatumRodjenja = pacijentoviPodaci.DatumRodjenja;
            pacijentZaIzmenu.Telefon = pacijentoviPodaci.Telefon;
            pacijentZaIzmenu.Email = pacijentoviPodaci.Email;
        }

        public void KreirajGostujuciNalog(GostujuciNalogDto gostujuciNalog)
        {
            Pacijent gostujuciPacijent = new(new Osoba(gostujuciNalog.Ime, gostujuciNalog.Prezime, gostujuciNalog.Jmbg,
                                            gostujuciNalog.DatumRodjenja, gostujuciNalog.Telefon, gostujuciNalog.Email,
                new Korisnik(null, null, (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "pacijent")), null));
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            gostujuciPacijent.zakazaniTermini = zakazaniTermini;
            PacijentRepo.Instance.DodajPacijenta(gostujuciPacijent);
        }

        public void UkloniGostujuciNalog(Pacijent pacijent)
        {
            PacijentRepo.Instance.ObrisiPacijenta(pacijent);
            KorisnikRepo.Instance.ObrisiKorisnika(pacijent.Korisnik);
            PacijentRepo.Instance.Serijalizacija();
            KorisnikRepo.Instance.Serijalizacija();
        }
    }
}



