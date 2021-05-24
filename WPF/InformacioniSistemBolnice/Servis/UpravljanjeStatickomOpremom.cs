using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;

namespace Servis
{
    public class UpravljanjeStatickomOpremom
    {

        private static readonly Lazy<UpravljanjeStatickomOpremom>
           lazy =
           new Lazy<UpravljanjeStatickomOpremom>
               (() => new UpravljanjeStatickomOpremom());

        public static UpravljanjeStatickomOpremom Instance { get { return lazy.Value; } }

        public void KreiranjeOpreme(StatickaOpremaDto dto)
        {
            if (StatickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip) == null)
            {
                StatickaOpremaRepo.Instance.ListaOpreme.Add(new(dto.Kolicina, dto.Tip));
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