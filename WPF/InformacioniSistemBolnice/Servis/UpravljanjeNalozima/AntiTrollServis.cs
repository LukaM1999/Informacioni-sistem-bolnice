using System;
using System.Linq;
using System.Threading;
using Model;
using Repozitorijum;

namespace Servis
{
    public class AntiTrollServis
    {
        private static readonly Lazy<AntiTrollServis> Lazy = new(() => new AntiTrollServis());
        public static AntiTrollServis Instance => Lazy.Value;

        private const int MaksimalniBrojMeseci = 12;
        private const int MaksimalnoMesecnihTermina = 3;
        private const int MaksimalnoPomerenihTermina = 4;
        private const int Tromesecje = 3;

        private Pacijent ulogovanPacijent;

        public void ProveriMalicioznostPacijenta(string pacijentJmbg)
        {
            ulogovanPacijent = PacijentRepo.Instance.NadjiPoJmbg(pacijentJmbg);
            while (!ulogovanPacijent.Maliciozan)
            {
                if (ProveriMalicioznostZakazivanjaTermina()) break;
                if (ProveriMalicioznostPomeranjaTermina()) break;
                Thread.Sleep(5000);
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
            foreach (Termin termin in ulogovanPacijent.ZakazaniTermini.ToList())
            {
                if (JeUnutarTromesecnogIntervala(termin, mesec)) pomerenihTermina++;
            }
            return pomerenihTermina;
        }

        private bool JeUnutarTromesecnogIntervala(Termin termin, int mesec)
        {
            return termin.Vreme > DateTime.Now.AddMonths((mesec - 1) * 3) &&
                   termin.Vreme < DateTime.Now.AddMonths(mesec * 3) && JePomerenPregled(termin);
        }

        private bool JePomerenPregled(Termin termin)
        {
            return termin.Status == StatusTermina.pomeren && termin.Tip == TipTermina.pregled;
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
            foreach (Termin termin in ulogovanPacijent.ZakazaniTermini.ToList())
            {
                if (JeUnutarMesecnogIntervala(termin, mesec)) mesecnihTermina++;
            }
            return mesecnihTermina;
        }

        private bool JeUnutarMesecnogIntervala(Termin termin, int mesec)
        {
            return termin.Vreme > DateTime.Now.AddMonths(mesec - 1) &&
                   termin.Vreme < DateTime.Now.AddMonths(mesec) && termin.Tip == TipTermina.pregled &&
                   termin.Status == StatusTermina.zakazan;
        }

        private bool OznaciMalicioznogPacijenta(int mesecnihTermina, int maksimalnoTermina)
        {
            if (mesecnihTermina <= maksimalnoTermina) return false;
            OznaciUlogovanog();
            PacijentRepo.Instance.Serijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            System.Diagnostics.Debug.WriteLine("Oznaceni ste kao maliciozni!");
            return true;
        }

        private void OznaciUlogovanog()
        {
            foreach (Pacijent maliciozanPacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (maliciozanPacijent.Jmbg != ulogovanPacijent.Jmbg) continue;
                maliciozanPacijent.Maliciozan = true;
            }
        }
    }
}
