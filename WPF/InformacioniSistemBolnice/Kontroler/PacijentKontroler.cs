using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Model;
using Servis;

namespace Kontroler
{
    public class PacijentKontroler
    {
        private static readonly Lazy<PacijentKontroler>
           lazy =
           new Lazy<PacijentKontroler>
               (() => new PacijentKontroler());

        public static PacijentKontroler Instance { get { return lazy.Value; } }

        public void Zakazivanje(IzborTermina izborTermina, string jmbgPacijenta)
        {
            UpravljanjeTerminimaPacijenata.Instance.Zakazivanje(izborTermina, jmbgPacijenta);
        }

        public void Otkazivanje(DataGrid listaZakazanihTermina)
        {
            UpravljanjeTerminimaPacijenata.Instance.Otkazivanje(listaZakazanihTermina);
        }

        public void Pomeranje(PomeranjeTerminaPacijentaProzor pomeranje)
        {
            UpravljanjeTerminimaPacijenata.Instance.Pomeranje(pomeranje);
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

    }
}