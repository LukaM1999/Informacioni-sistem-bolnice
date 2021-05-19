using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Model;
using Repozitorijum;

namespace Servis
{
    public class UpravljanjeObavestenjimaPacijenta
    {
        private static readonly Lazy<UpravljanjeObavestenjimaPacijenta>
            Lazy = new(() => new UpravljanjeObavestenjimaPacijenta());
        public static UpravljanjeObavestenjimaPacijenta Instance => Lazy.Value;

        public void UkljuciPodsetnike(string jmbgPacijenta)
        {
            Podsetnici.Instance.Deserijalizacija();
            JobManager.Initialize();
            foreach (Podsetnik pacijentovPodsetnik in Podsetnici.Instance.NadjiSvePoJmbg(jmbgPacijenta))
                UkljuciNoviPodsetnik(pacijentovPodsetnik);
        }

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
        {
            int[] satMinut = KonvertujVremePodsetnika(novPodsetnik.Vreme);
            JobManager.AddJob(() => Debug.WriteLine(novPodsetnik.Sadrzaj),
                (s) => s.WithName(novPodsetnik.PacijentJmbg + novPodsetnik.Vreme)
                    .ToRunEvery(1).Days().At(satMinut[0], satMinut[1]));
            if (!Podsetnici.Instance.PodsetnikVecPostoji(novPodsetnik))
                Podsetnici.Instance.DodajPodsetnik(novPodsetnik);
        }

        private int[] KonvertujVremePodsetnika(string vreme)
        {
            return new[] { int.Parse(vreme.Split(":")[0]), int.Parse(vreme.Split(":")[1]) };
        }
    }
}
