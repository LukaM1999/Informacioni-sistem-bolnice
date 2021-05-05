using System;
using System.Linq;
using System.Threading;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis
{
    class UpravljanjeAntiTrollMehanizmom
    {
        private static readonly Lazy<UpravljanjeAntiTrollMehanizmom>
            Lazy = new(() => new UpravljanjeAntiTrollMehanizmom());
        public static UpravljanjeAntiTrollMehanizmom Instance { get { return Lazy.Value; } }

        private const int MaksimalniBrojMeseci = 12;
        private const int MaksimalnoMesecnihTermina = 3;
        private const int MaksimalnoPomerenihTermina = 4;
        private const int Tromesecje = 3;

        public Pacijent UlogovanPacijent { get; set; }

        public void ProveriMalicioznostPacijenta(Pacijent ulogovan)
        {
            PronadjiUlogovanogPacijenta(ulogovan);
            while (!UlogovanPacijent.maliciozan)
            {
                if (ProveriMalicioznostZakazivanjaTermina()) break;
                if (ProveriMalicioznostPomeranjaTermina()) break;
                Thread.Sleep(5000);
            }
        }

        private void PronadjiUlogovanogPacijenta(Pacijent ulogovan)
        {
            foreach (Pacijent ulogovanPacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (ulogovanPacijent.jmbg != ulogovan.jmbg) continue;
                UlogovanPacijent = ulogovanPacijent;
            }
        }

        private bool ProveriMalicioznostPomeranjaTermina()
        {
            for (int i = 1; i <= MaksimalniBrojMeseci / Tromesecje; i++)
            {
                int pomerenihTermina = PrebrojPomereneTerminePacijenta(i);
                if (OznaciMalicioznogPacijenta(pomerenihTermina, MaksimalnoPomerenihTermina)) return true;
            }
            return false;
        }

        private int PrebrojPomereneTerminePacijenta(int mesec)
        {
            int pomerenihTermina = 0;
            foreach (Termin termin in UlogovanPacijent.zakazaniTermini.ToList())
            {
                if (JeUnutarTromesecnogIntervala(termin, mesec)) pomerenihTermina++;
            }
            return pomerenihTermina;
        }

        private static bool JeUnutarTromesecnogIntervala(Termin termin, int mesec)
        {
            return termin.vreme > DateTime.Now.AddMonths((mesec - 1) * 3) &&
                   termin.vreme < DateTime.Now.AddMonths(mesec * 3) && JePomerenPregled(termin);
        }

        private static bool JePomerenPregled(Termin termin)
        {
            return termin.status == StatusTermina.pomeren && termin.tipTermina == TipTermina.pregled;
        }

        private bool ProveriMalicioznostZakazivanjaTermina()
        {
            for (int i = 1; i <= MaksimalniBrojMeseci; i++)
            {
                int mesecnihTermina = PrebrojZakazaneTerminePacijenta(i);
                if (OznaciMalicioznogPacijenta(mesecnihTermina, MaksimalnoMesecnihTermina)) return true;
            }
            return false;
        }

        private int PrebrojZakazaneTerminePacijenta(int mesec)
        {
            int mesecnihTermina = 0;
            foreach (Termin termin in UlogovanPacijent.zakazaniTermini.ToList())
            {
                if (JeUnutarMesecnogIntervala(termin, mesec)) mesecnihTermina++;
            }
            return mesecnihTermina;
        }

        private static bool JeUnutarMesecnogIntervala(Termin termin, int mesec)
        {
            return termin.vreme > DateTime.Now.AddMonths(mesec - 1) &&
                   termin.vreme < DateTime.Now.AddMonths(mesec) && termin.tipTermina == TipTermina.pregled &&
                   termin.status == StatusTermina.zakazan;
        }

        private bool OznaciMalicioznogPacijenta(int mesecnihTermina, int maksimalnoTermina)
        {
            if (mesecnihTermina <= maksimalnoTermina) return false;
            OznaciUlogovanog();
            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            System.Diagnostics.Debug.WriteLine("Oznaceni ste kao maliciozni!");
            return true;
        }

        private void OznaciUlogovanog()
        {
            foreach (Pacijent maliciozanPacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (maliciozanPacijent.jmbg != UlogovanPacijent.jmbg) continue;
                maliciozanPacijent.maliciozan = true;
            }
        }
    }
}
