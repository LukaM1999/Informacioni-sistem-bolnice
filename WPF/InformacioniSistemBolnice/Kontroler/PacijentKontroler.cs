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
            UpravljanjeTerminima.Instance.Zakazivanje(terminZaZakazivanje);
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            UpravljanjeTerminima.Instance.Otkazivanje(terminZaOtkazivanje);
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            UpravljanjeTerminima.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            UpravljanjeTerminima.Instance.Uvid(listaZakazanihTermina);
        }

        public void PopuniAnketuOLekaru(Termin zavrsenTermin, AnketaOLekaru novaAnketa)
        {
            UpravljanjeAnketama.Instance.PopuniAnketuOLekaru(zavrsenTermin, novaAnketa);
        }

        public void PosaljiAnketuOBolnici(AnketaOBolnici anketa)
        {
            UpravljanjeAnketama.Instance.PosaljiAnketuOBolnici(anketa);
        }

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
        {
            UpravljanjeObavestenjimaPacijenta.Instance.UkljuciNoviPodsetnik(novPodsetnik);
        }

        public void DodajBeleske(Pacijent ulogovanPacijent, string beleske)
        {
            IzmenaKartonaPacijenta.Instance.DodajBeleske(ulogovanPacijent, beleske);
        }
    }
}