using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Model;
using Servis;

namespace Kontroler
{
    public class PacijentKontroler
    {
        private static readonly Lazy<PacijentKontroler> Lazy = new(() => new PacijentKontroler());
        public static PacijentKontroler Instance => Lazy.Value;

        public void ZakaziTermin(Termin terminZaZakazivanje)
            => TerminServis.Instance.ZakaziTermin(terminZaZakazivanje);

        public void OtkaziTermin(Termin terminZaOtkazivanje)
            => TerminServis.Instance.OtkaziTermin(terminZaOtkazivanje);

        public void PomeriTermin(Termin terminZaPomeranje, Termin noviTermin)
            => TerminServis.Instance.PomeriTermin(terminZaPomeranje, noviTermin);

        public void PosaljiAnketuOLekaru(Termin zavrsenTermin, AnketaDto novaAnketa)
            => SlanjeAnketeServis.Instance.PosaljiAnketuOLekaru(zavrsenTermin, novaAnketa);

        public void PosaljiAnketuOBolnici(AnketaDto anketa)
            => SlanjeAnketeServis.Instance.PosaljiAnketuOBolnici(anketa);

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
            => ObavestenjePacijentaServis.Instance.UkljuciNoviPodsetnik(novPodsetnik);

        public void DodajBeleske(Pacijent ulogovanPacijent, string beleske)
            => AnamnezaServis.Instance.DodajBeleske(ulogovanPacijent, beleske);
    }
}