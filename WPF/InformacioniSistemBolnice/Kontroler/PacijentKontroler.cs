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
            UpravljanjeTerminimaPacijenata.Instance.Zakazivanje(terminZaZakazivanje);
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            UpravljanjeTerminimaPacijenata.Instance.Otkazivanje(terminZaOtkazivanje);
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            UpravljanjeTerminimaPacijenata.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            UpravljanjeTerminimaPacijenata.Instance.Uvid(listaZakazanihTermina);
        }

        public void PopuniAnketuOLekaru(Termin zavrsenTermin)
        {
            UpravljanjeAnketama.Instance.PopuniAnketuOLekaru(zavrsenTermin);
        }

        public void PopuniAnketuOBolnici(AnketaOBolnici anketa)
        {
            UpravljanjeAnketama.Instance.PopuniAnketuOBolnici(anketa);
        }

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
        {
            UpravljanjeObavestenjimaPacijenta.Instance.UkljuciNoviPodsetnik(novPodsetnik);
        }
    }
}