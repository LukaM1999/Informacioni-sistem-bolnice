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
            AlergenRepo.Instance.DodajAlergen(new Alergen(alergenDto.Naziv));
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            AlergenRepo.Instance.BrisiPoNazivu(alergen.Naziv);
            AlergenRepo.Instance.SacuvajPromene();
        }

        public void IzmenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            stariAlergen.Naziv = noviAlergen.Naziv;
            AlergenRepo.Instance.SacuvajPromene();
        }

        public AlergenDto PregledAlergena(Alergen alergen)
        {
            AlergenDto alergenDto = new(alergen.Naziv);
            return alergenDto;
        }

    }
}