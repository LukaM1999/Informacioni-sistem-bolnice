using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class UpravljanjeDinamickomOpremom
    {
        private static readonly Lazy<UpravljanjeDinamickomOpremom> lazy = new Lazy<UpravljanjeDinamickomOpremom> (() => new UpravljanjeDinamickomOpremom());
        public static UpravljanjeDinamickomOpremom Instance { get { return lazy.Value; } }
        public void KreiranjeOpreme(DinamickaOpremaDto dto)
        {
            if (DinamickaOpremaRepo.Instance.NadjiPoTipu(dto.Tip) == null)
            {
                DinamickaOpremaRepo.Instance.ListaOpreme.Add(new(dto.Kolicina, dto.Tip));
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

        public void IzmenaOpreme(Model.DinamickaOprema oprema, MagacinIzmeniDinamickuOpremu p)
        {
            oprema.Kolicina = Int32.Parse(p.tb1.Text);
            oprema.Tip = (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), p.cb1.Text, true);

            Repozitorijum.DinamickaOpremaRepo.Instance.Serijalizacija();
            Repozitorijum.DinamickaOpremaRepo.Instance.Deserijalizacija();
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.StatickaOpremaRepo magacin;

    }
}