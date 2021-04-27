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

        public void OznaciMalicioznogKorisnika(Pacijent ulogovanPacijent)
        {
            int mesecnihTermina = 0;
            int pomerenihTermina = 0;
            while (!ulogovanPacijent.maliciozan)
            {
                for (int i = 1; i < 13; i++)
                {
                    foreach (Termin t in ulogovanPacijent.zakazaniTermini.ToList())
                    {
                        if (t.vreme > DateTime.Now.AddMonths(i - 1) && t.vreme < DateTime.Now.AddMonths(i))
                        {
                            mesecnihTermina++;
                        }

                        if (mesecnihTermina > 3)
                        {
                            ulogovanPacijent.maliciozan = true;

                            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");

                            System.Diagnostics.Debug.WriteLine("Precesto zakazivanje termina!");
                            break;
                        }
                    }

                    mesecnihTermina = 0;
                    if (ulogovanPacijent.maliciozan)
                    {
                        break;
                    }
                }

                for (int i = 1; i < 5; i++)
                {
                    foreach (Termin t in ulogovanPacijent.zakazaniTermini.ToList())
                    {
                        if (t.vreme > DateTime.Now.AddMonths((i - 1) * 3) && t.vreme < DateTime.Now.AddMonths(i * 3) &&
                            t.status == StatusTermina.pomeren)
                        {
                            pomerenihTermina++;
                        }

                        if (pomerenihTermina > 4)
                        {
                            ulogovanPacijent.maliciozan = true;

                            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");

                            System.Diagnostics.Debug.WriteLine("Precesto pomeranje termina!");
                            break;
                        }
                    }

                    pomerenihTermina = 0;
                    if (ulogovanPacijent.maliciozan)
                    {
                        break;
                    }
                }
            }
        }

    }
}
