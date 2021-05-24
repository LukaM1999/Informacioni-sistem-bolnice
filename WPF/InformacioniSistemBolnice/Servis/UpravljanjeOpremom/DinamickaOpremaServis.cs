using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class DinamickaOpremaServis
    {
        private static readonly Lazy<DinamickaOpremaServis> Lazy = new(() => new DinamickaOpremaServis());
        public static DinamickaOpremaServis Instance => Lazy.Value;

        public void KreiranjeOpreme(DinamickaOpremaDto dto)
        {
            if (DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip) == null)
            {
                DinamickaOpremaRepo.Instance.DinamickaOprema.Add(new(dto.Kolicina, dto.Tip));
                DinamickaOpremaRepo.Instance.SacuvajPromene();
                return;
            }
            DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip).Kolicina += dto.Kolicina;
            DinamickaOpremaRepo.Instance.SacuvajPromene();
        }

        public void UklanjanjeOpreme(DinamickaOpremaDto dto)
        {
            DinamickaOpremaRepo.Instance.BrisiPoTipu(dto.Tip);
            DinamickaOpremaRepo.Instance.SacuvajPromene();
        }

        public void IzmenaOpreme(DinamickaOpremaDto dto)
        {
            DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip).Kolicina = dto.Kolicina;
            DinamickaOpremaRepo.Instance.SacuvajPromene();
        }
    }
}