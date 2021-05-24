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

        public void Zakazivanje(Termin terminZaZakazivanje)
        {
            TerminServis.Instance.Zakazivanje(terminZaZakazivanje);
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            TerminServis.Instance.Otkazivanje(terminZaOtkazivanje);
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            TerminServis.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            TerminServis.Instance.Uvid(listaZakazanihTermina);
        }

        public void PopuniAnketuOLekaru(Termin zavrsenTermin, AnketaOLekaru novaAnketa)
        {
            AnketaServis.Instance.PopuniAnketuOLekaru(zavrsenTermin, novaAnketa);
        }

        public void PosaljiAnketuOBolnici(AnketaOBolnici anketa)
        {
            AnketaServis.Instance.PosaljiAnketuOBolnici(anketa);
        }

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
        {
            ObavestenjePacijentaServis.Instance.UkljuciNoviPodsetnik(novPodsetnik);
        }

        public void DodajBeleske(Pacijent ulogovanPacijent, string beleske)
        {
            AnamnezaServis.Instance.DodajBeleske(ulogovanPacijent, beleske);
        }
    }
}