using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
    public class DinamickaOpremaServis
    {
        private static readonly Lazy<DinamickaOpremaServis> Lazy = new(() => new DinamickaOpremaServis());
        public static DinamickaOpremaServis Instance => Lazy.Value;

        public void KreiranjeOpreme(DinamickaOpremaDto dto)
        {
            if (DinamickaOpremaRepo.Instance.NadjiPoId(dto.Tip) == null)
            {
                DinamickaOpremaRepo.Instance.Dodaj(new DinamickaOprema(dto.Kolicina, dto.Tip));
                return;
            }
            DinamickaOprema izabranaOprema = DinamickaOpremaRepo.Instance.NadjiPoId(dto.Tip) as DinamickaOprema;
            izabranaOprema.Kolicina += dto.Kolicina;
            DinamickaOpremaRepo.Instance.Serijalizacija();
        }

        public void UklanjanjeOpreme(DinamickaOpremaDto dto)
        {
            DinamickaOpremaRepo.Instance.ObrisiPoId(dto.Tip);
            DinamickaOpremaRepo.Instance.Serijalizacija();
        }

        public void IzmenaOpreme(DinamickaOpremaDto dto)
        {
            DinamickaOprema izabranaOprema = DinamickaOpremaRepo.Instance.NadjiPoId(dto.Tip) as DinamickaOprema;
            izabranaOprema.Kolicina = dto.Kolicina;
            DinamickaOpremaRepo.Instance.Serijalizacija();
        }
    }
}