using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice.Servis
{
    class UpravljanjeAntiTrollMehanizmom
    {
        private static readonly Lazy<UpravljanjeAntiTrollMehanizmom>
            lazy =
                new Lazy<UpravljanjeAntiTrollMehanizmom>
                    (() => new UpravljanjeAntiTrollMehanizmom());

        public static UpravljanjeAntiTrollMehanizmom Instance { get { return lazy.Value; } }

        private int maksimalniBrojMeseci = 12;
        private int maksimalnoMesecnihTermina = 3;
        private int maksimalnoPomerenihTermina = 4;

        public Pacijent UlogovanPacijent { get; set; }

        public void ProveriMalicioznostPacijenta(Pacijent ulogovan)
        {
            UlogovanPacijent = ulogovan;
            int mesecnihTermina = 0;
            int pomerenihTermina = 0;
            while (!UlogovanPacijent.maliciozan)
            {
                ProveraMalicioznogZakazivanjaTermina(mesecnihTermina);

                ProveraMalicioznogPomeranjaTermina(pomerenihTermina);
            }
        }

        private void ProveraMalicioznogPomeranjaTermina(int pomerenihTermina)
        {
            for (int i = 1; i <= maksimalniBrojMeseci / 3; i++)
            {
                pomerenihTermina = PrebrojPomereneTerminePacijenta(pomerenihTermina, i);
                if (OznaciMalicioznogKorisnika(pomerenihTermina, maksimalnoPomerenihTermina)) break;
                pomerenihTermina = 0;
            }
        }

        private int PrebrojPomereneTerminePacijenta(int pomerenihTermina, int i)
        {
            foreach (Termin t in UlogovanPacijent.zakazaniTermini.ToList())
            {
                if (JeUnutarTromesecnogIntervala(t, i))
                {
                    pomerenihTermina++;
                }
            }

            return pomerenihTermina;
        }

        private static bool JeUnutarTromesecnogIntervala(Termin t, int i)
        {
            return t.vreme > DateTime.Now.AddMonths((i - 1) * 3) && t.vreme < DateTime.Now.AddMonths(i * 3) &&
                   t.status == StatusTermina.pomeren && t.tipTermina == TipTermina.pregled;
        }

        private void ProveraMalicioznogZakazivanjaTermina(int mesecnihTermina)
        {
            for (int i = 1; i <= maksimalniBrojMeseci; i++)
            {
                mesecnihTermina = PrebrojZakazaneTerminePacijenta(mesecnihTermina, i);
                if (OznaciMalicioznogKorisnika(mesecnihTermina, maksimalnoMesecnihTermina)) break;
                mesecnihTermina = 0;
            }
        }

        private int PrebrojZakazaneTerminePacijenta(int mesecnihTermina, int i)
        {
            foreach (Termin t in UlogovanPacijent.zakazaniTermini.ToList())
            {
                if (JeUnutarMesecnogIntervala(t, i))
                {
                    mesecnihTermina++;
                }
            }

            return mesecnihTermina;
        }

        private bool OznaciMalicioznogKorisnika(int mesecnihTermina, int maksimalnoTermina)
        {
            if (mesecnihTermina > maksimalnoTermina)
            {
                UlogovanPacijent.maliciozan = true;
                Pacijenti.Instance.Serijalizacija();
                Pacijenti.Instance.Deserijalizacija();
                System.Diagnostics.Debug.WriteLine("Precesto zakazivanje termina!");
                return true;
            }

            return false;
        }

        private static bool JeUnutarMesecnogIntervala(Termin t, int i)
        {
            return t.vreme > DateTime.Now.AddMonths(i - 1) && t.vreme < DateTime.Now.AddMonths(i) && t.tipTermina == TipTermina.pregled;
        }
    }
}
