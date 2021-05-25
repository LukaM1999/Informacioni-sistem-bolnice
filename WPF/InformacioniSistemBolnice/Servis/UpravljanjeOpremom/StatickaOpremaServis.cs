using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class StatickaOpremaServis
    {
        private static readonly Lazy<StatickaOpremaServis> Lazy = new(() => new StatickaOpremaServis());
        public static StatickaOpremaServis Instance => Lazy.Value;

        public void KreiranjeOpreme(StatickaOpremaDto dto)
        {
            if (StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip) == null)
            {
                StatickaOpremaRepo.Instance.DodajStatickuOpremu(new(dto.Kolicina, dto.Tip));
                StatickaOpremaRepo.Instance.Serijalizacija();
                return;
            }
            StatickaOprema izabranaOprema = StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip);
            izabranaOprema.Kolicina += dto.Kolicina;
            StatickaOpremaRepo.Instance.Serijalizacija();
        }

        public void UklanjanjeOpreme(StatickaOpremaDto dto)
        {
            StatickaOpremaRepo.Instance.BrisiPoTipu(dto.Tip);
            StatickaOpremaRepo.Instance.Serijalizacija();
        }

        public void IzmenaOpreme(StatickaOpremaDto dto)
        {
            StatickaOprema izabranaOprema = StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip);
            izabranaOprema.Kolicina = dto.Kolicina;
            StatickaOpremaRepo.Instance.Serijalizacija();
        }
    }
}