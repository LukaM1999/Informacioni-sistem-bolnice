using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Servis;

namespace Kontroler
{
    public class LekarKontroler
    {
        private static readonly Lazy<LekarKontroler>
             lazy =
             new Lazy<LekarKontroler>
                 (() => new LekarKontroler());

        public static LekarKontroler Instance { get { return lazy.Value; } }

        public void Zakazivanje(ZakazivanjeTerminaLekaraProzor zakazivanje)
        {
            UpravljanjeTerminimaLekara.Instance.Zakazivanje(zakazivanje);
        }

        public void Otkazivanje(DataGrid listaZakazanihTerminaLekara)
        {
            UpravljanjeTerminimaLekara.Instance.Otkazivanje(listaZakazanihTerminaLekara);
        }

        public void Pomeranje(PomeranjeTerminaLekaraProzor pomeranje)
        {
            UpravljanjeTerminimaLekara.Instance.Pomeranje(pomeranje);
        }

        public void Uvid(DataGrid listaZakazanihTerminaLekara)
        {
            UpravljanjeTerminimaLekara.Instance.Uvid(listaZakazanihTerminaLekara);
        }

        public UpravljanjeTerminimaLekara upravljanjeTerminimaLekara;
        public IzmenaKartonaPacijenta izmenaKartonaPacijenta;

    }
}