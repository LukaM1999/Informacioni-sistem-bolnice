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
    public class ZavrsenTerminServis
    {
        private static readonly Lazy<ZavrsenTerminServis> Lazy = new(() => new ZavrsenTerminServis());
        public static ZavrsenTerminServis Instance => Lazy.Value;

        public Pacijent ulogovanPacijent;
        public Termin pacijentovTermin;

        public void ProveriZavrsenostTermina(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            while (true)
            {
                foreach (Termin termin in TerminRepo.Instance.Termini.ToList())
                {
                    pacijentovTermin = termin;
                    if (!JeTerminZavrsen()) continue;
                    ZavrsiPacijentovTermin();
                    termin.Status = StatusTermina.zavrsen;
                    TerminRepo.Instance.Serijalizacija();
                    TerminRepo.Instance.Deserijalizacija();
                    System.Diagnostics.Debug.WriteLine(DateTime.Now);
                }
                Thread.Sleep(1000);
            }
        }

        private void ZavrsiPacijentovTermin()
        {
            foreach (Termin termin in ulogovanPacijent.zakazaniTermini)
            {
                if (!JePacijentovNezavrsenTermin(termin)) continue;
                pacijentovTermin.Status = StatusTermina.zavrsen;
                break;
            }
        }

        private bool JePacijentovNezavrsenTermin(Termin termin)
        {
            return termin.Vreme == pacijentovTermin.Vreme && pacijentovTermin.Status != StatusTermina.zavrsen;
        }

        private bool JeTerminZavrsen()
        {
            return pacijentovTermin.Vreme.AddMinutes(pacijentovTermin.Trajanje) < DateTime.Now &&
                   pacijentovTermin.Status != StatusTermina.zavrsen;
        }
    }
}
