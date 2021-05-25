using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;


namespace Servis
{
    public class AlergenServis
    {

        private static readonly Lazy<AlergenServis>
           Lazy =
           new Lazy<AlergenServis>
               (() => new AlergenServis());

        public static AlergenServis Instance => Lazy.Value;

        public void KreiranjeAlergena(AlergenDto alergenDto)
        {
            AlergenRepo.Instance.DodajAlergen(new Alergen(alergenDto.Naziv));
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            AlergenRepo.Instance.BrisiPoNazivu(alergen.Naziv);
            AlergenRepo.Instance.Serijalizacija();
        }

        public void IzmenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            stariAlergen.Naziv = noviAlergen.Naziv;
            AlergenRepo.Instance.Serijalizacija();
        }

        public AlergenDto PregledAlergena(Alergen alergen)
        {
            AlergenDto alergenDto = new(alergen.Naziv);
            return alergenDto;
        }

    }
}