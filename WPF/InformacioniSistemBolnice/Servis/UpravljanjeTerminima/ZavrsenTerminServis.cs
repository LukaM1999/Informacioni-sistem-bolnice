using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    public class ZavrsenTerminServis<T> where T: IUcesnikPregleda
    {
        private T ulogovanKorisnik;
        private Termin korisnikovTermin;

        public void PokreniProveruZavrsenostiTermina(T korisnik)
        {
            ulogovanKorisnik = korisnik;
            while (true)
            {
                ProveriZavrsenostTermina();
                Thread.Sleep(1000);
            }
        }

        private void ProveriZavrsenostTermina()
        {
            foreach (Termin termin in TerminRepo.Instance.Termini.ToList())
            {
                korisnikovTermin = termin;
                if (!JeTerminZavrsen()) continue;
                ZavrsiKorisnikovTermin();
                termin.Status = StatusTermina.zavrsen;
                TerminRepo.Instance.Serijalizacija();
            }
        }

        private void ZavrsiKorisnikovTermin()
        {
            foreach (Termin termin in ulogovanKorisnik.ZakazaniTermini)
            {
                if (!JeKorisnikovNezavrsenTermin(termin)) continue;
                korisnikovTermin.Status = StatusTermina.zavrsen;
                break;
            }
        }

        private bool JeKorisnikovNezavrsenTermin(Termin termin)
        {
            return termin.Vreme == korisnikovTermin.Vreme && korisnikovTermin.Status is not StatusTermina.zavrsen;
        }

        private bool JeTerminZavrsen()
        {
            return korisnikovTermin.Vreme.AddMinutes(korisnikovTermin.Trajanje) < DateTime.Now &&
                   korisnikovTermin.Status != StatusTermina.zavrsen;
        }
    }
}
