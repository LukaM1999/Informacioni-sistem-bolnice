using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;


namespace Servis
{
    public class UpravljanjeAlergenima
    {

        private static readonly Lazy<UpravljanjeAlergenima>
           lazy =
           new Lazy<UpravljanjeAlergenima>
               (() => new UpravljanjeAlergenima());

        public static UpravljanjeAlergenima Instance { get { return lazy.Value; } }

        public void KreiranjeAlergena(AlergenDto alergenDto)
        {
            Alergeni.Instance.DodajAlergen(new Alergen(alergenDto.Naziv));
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            Alergeni.Instance.BrisiPoNazivu(alergen.Naziv);
            Alergeni.Instance.SacuvajPromene();
        }

        public void IzmenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            stariAlergen.Naziv = noviAlergen.Naziv;
            Alergeni.Instance.SacuvajPromene();
        }

        public AlergenDto PregledAlergena(Alergen alergen)
        {
            AlergenDto alergenDto = new(alergen.Naziv);
            return alergenDto;
        }

    }
}