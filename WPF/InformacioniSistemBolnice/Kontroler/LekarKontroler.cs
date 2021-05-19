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

        public void Zakazivanje(Termin izabranTermin)
        {
            UpravljanjeTerminima.Instance.Zakazivanje(izabranTermin);
        }

        public void Otkazivanje(Termin izabranTermin)
        {
            UpravljanjeTerminima.Instance.Otkazivanje(izabranTermin);
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            UpravljanjeTerminima.Instance.Pomeranje(terminZaPomeranje, noviTermin);
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