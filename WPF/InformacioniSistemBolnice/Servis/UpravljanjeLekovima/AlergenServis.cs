using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice.DTO;


namespace Servis
{
    public class AlergenServis
    {
        private static readonly Lazy<AlergenServis> Lazy = new Lazy<AlergenServis>(() => new AlergenServis());

        public static AlergenServis Instance => Lazy.Value;

        public void KreiranjeAlergena(AlergenDto alergenDto)
        {
            AlergenRepo.Instance.Dodaj(new Alergen(alergenDto.Naziv));
        }

        public void UklanjanjeAlergena(Alergen alergen)
        {
            AlergenRepo.Instance.ObrisiPoId(alergen.Naziv);
            AlergenRepo.Instance.Serijalizacija();
        }

        public void IzmenaAlergena(AlergenDto noviAlergen, Alergen stariAlergen)
        {
            stariAlergen.Naziv = noviAlergen.Naziv;
            AlergenRepo.Instance.Serijalizacija();
        }

    }
}