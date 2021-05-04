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
    public class ProveraZavrsenostiTermina
    {
        private static readonly Lazy<ProveraZavrsenostiTermina>
            Lazy = new(() => new ProveraZavrsenostiTermina());
        public static ProveraZavrsenostiTermina Instance { get { return Lazy.Value; } }

        public Pacijent ulogovanPacijent;
        public Termin pacijentovTermin;

        public void ProveriZavrsenostTermina(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            while (true)
            {
                foreach (Termin termin in Termini.Instance.listaTermina.ToList())
                {
                    pacijentovTermin = termin;
                    if (!JeTerminZavrsen()) continue;
                    ZavrsiPacijentovTermin();
                    termin.status = StatusTermina.zavrsen;
                    Termini.Instance.Serijalizacija();
                    Termini.Instance.Deserijalizacija();
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
                pacijentovTermin.status = StatusTermina.zavrsen;
                break;
            }
        }

        private bool JePacijentovNezavrsenTermin(Termin termin)
        {
            return termin.vreme == pacijentovTermin.vreme && pacijentovTermin.status != StatusTermina.zavrsen;
        }

        private bool JeTerminZavrsen()
        {
            return pacijentovTermin.vreme.AddMinutes(pacijentovTermin.trajanje) < DateTime.Now &&
                   pacijentovTermin.status != StatusTermina.zavrsen;
        }
    }
}
