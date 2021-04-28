using System;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using Servis;
using Model;


namespace Kontroler
{
    public class LekarKontroler
    {
        private static readonly Lazy<LekarKontroler>
             lazy =
             new Lazy<LekarKontroler>
                 (() => new LekarKontroler());

        public static LekarKontroler Instance { get { return lazy.Value; } }

        public void Zakazivanje(ZakazivanjeTerminaLekaraProzor zakazivanje, string jmbgLekar)
        {
            UpravljanjeTerminimaLekara.Instance.Zakazivanje(zakazivanje, jmbgLekar);
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
        
        public void IzdavanjeRecepta(ReceptDto recept)
        {
            IzmenaKartonaPacijenta.Instance.IzdavanjeRecepta(recept);
        }

        public void DodavanjeAnamneze(AnamnezaForma anamneza)
        {
            IzmenaKartonaPacijenta.Instance.DodavanjeAnamneze(anamneza);
        }
    }
}