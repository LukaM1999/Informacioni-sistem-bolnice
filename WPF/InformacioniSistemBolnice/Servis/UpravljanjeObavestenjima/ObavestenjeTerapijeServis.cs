using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Model;

namespace Servis
{
    public class ObavestenjeTerapijeServis
    {
        private static readonly Lazy<ObavestenjeTerapijeServis> Lazy = new(() => new ObavestenjeTerapijeServis());
        public static ObavestenjeTerapijeServis Instance => Lazy.Value;

        private Pacijent ulogovanPacijent;
        private Terapija trenutnaTerapija;

        public void UkljuciObavestenja(Pacijent pacijent)
        {
            if (pacijent.zdravstveniKarton == null) return;
            ulogovanPacijent = pacijent;
            ZakaziObavestenja();
            while (true) PrikaziObavestenja();
        }

        private void PrikaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.recepti)
            {
                foreach (Terapija t in recept.terapije)
                {
                    trenutnaTerapija = t;
                    if (JeVremeZaPrikaz()) OmoguciPrikazObavestenja();
                    else OnemoguciPrikazObavestenja();
                }
            }
        }

        private void OmoguciPrikazObavestenja()
        {
            JobManager.GetSchedule("prvo uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Enable();
            JobManager.GetSchedule("uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Enable();
        }

        private void OnemoguciPrikazObavestenja()
        {
            JobManager.GetSchedule("prvo uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Disable();
            JobManager.GetSchedule("uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Disable();
        }

        private bool JeVremeZaPrikaz()
        {
            return DateTime.Now > trenutnaTerapija.PocetakTerapije && DateTime.Now < trenutnaTerapija.KrajTerapije;
        }

        private void ZakaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.recepti)
            {
                foreach (Terapija terapija in recept.terapije)
                {
                    JobManager.Initialize();
                    ZakaziPrvoObavestenje(terapija);
                    ZakaziDaljaObavestenja(terapija);
                }
            }
        }

        private static void ZakaziDaljaObavestenja(Terapija terapija)
        {
            int redovnost = terapija.DobaviRedovnostTerapije();
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.MeraLeka + " mg."),
                (s) => s.WithName("uzimanje: " + terapija.Lek.Naziv).ToRunEvery(redovnost).Seconds()
                    .DelayFor(redovnost - DateTime.Now.Second % redovnost).Seconds());
        }

        private static void ZakaziPrvoObavestenje(Terapija terapija)
        {
            int redovnost = terapija.DobaviRedovnostTerapije();
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.MeraLeka + " mg."),
                (s) => s.WithName("prvo uzimanje: " + terapija.Lek.Naziv)
                    .ToRunOnceIn(redovnost - DateTime.Now.Second % redovnost).Seconds());
        }
    }
}
