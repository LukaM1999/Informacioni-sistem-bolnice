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
    public class UpravljanjeObavestenjimaTerapija
    {
        private static readonly Lazy<UpravljanjeObavestenjimaTerapija>
            Lazy = new(() => new UpravljanjeObavestenjimaTerapija());
        public static UpravljanjeObavestenjimaTerapija Instance { get { return Lazy.Value; } }

        public Pacijent ulogovanPacijent;
        public Terapija trenutnaTerapija;

        public void UkljuciObavestenja(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            ZakaziObavestenja();
            while (true) PrikaziObavestenja();
        }

        private void PrikaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.Recepti)
            {
                foreach (Terapija t in recept.Terapije)
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
            return DateTime.Now > trenutnaTerapija.pocetakTerapije && DateTime.Now < trenutnaTerapija.krajTerapije;
        }

        private void ZakaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.Recepti)
            {
                foreach (Terapija terapija in recept.Terapije)
                {
                    JobManager.Initialize();
                    ZakaziPrvoObavestenje(terapija);
                    ZakaziDaljaObavestenja(terapija);
                }
            }
        }

        private static void ZakaziDaljaObavestenja(Terapija terapija)
        {
            int redovnost = DobaviRedovnostTerapije(terapija);
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.mera + " mg."),
                (s) => s.WithName("uzimanje: " + terapija.Lek.Naziv).ToRunEvery(redovnost).Seconds()
                    .DelayFor(redovnost - DateTime.Now.Second % redovnost).Seconds());
        }

        private static int DobaviRedovnostTerapije(Terapija terapija)
        {
            return (int)terapija.redovnost;
        }

        private static void ZakaziPrvoObavestenje(Terapija terapija)
        {
            int redovnost = DobaviRedovnostTerapije(terapija);
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.mera + " mg."),
                (s) => s.WithName("prvo uzimanje: " + terapija.Lek.Naziv)
                    .ToRunOnceIn(redovnost - DateTime.Now.Second % redovnost).Seconds());
        }
    }
}
