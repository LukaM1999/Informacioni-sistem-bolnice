using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;

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
                StatickaOpremaRepo.Instance.StatickaOprema.Add(new(dto.Kolicina, dto.Tip));
                StatickaOpremaRepo.Instance.SacuvajPromene();
                return;
            }
            StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip).Kolicina += dto.Kolicina;
            StatickaOpremaRepo.Instance.SacuvajPromene();
        }

        public void UklanjanjeOpreme(StatickaOpremaDto dto)
        {
            StatickaOpremaRepo.Instance.BrisiPoTipu(dto.Tip);
            StatickaOpremaRepo.Instance.SacuvajPromene();
        }

        public void IzmenaOpreme(StatickaOpremaDto dto)
        {
            StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip).Kolicina = dto.Kolicina;
            StatickaOpremaRepo.Instance.SacuvajPromene();
        }
    }
}