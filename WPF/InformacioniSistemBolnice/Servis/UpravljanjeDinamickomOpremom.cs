using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class UpravljanjeDinamickomOpremom
    {
        private static readonly Lazy<UpravljanjeDinamickomOpremom>
           lazy =
           new Lazy<UpravljanjeDinamickomOpremom>
               (() => new UpravljanjeDinamickomOpremom());

        public static UpravljanjeDinamickomOpremom Instance { get { return lazy.Value; } }

        public void KreiranjeOpreme(MagacinDodajDinamickuOpremu p)
        {
            Model.DinamickaOprema oprema = new Model.DinamickaOprema(Int32.Parse(p.tbKol.Text), (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), p.cb1.Text));
            Repozitorijum.DinamickaOprema.Instance.listaOpreme.Add(oprema);
            Repozitorijum.DinamickaOprema.Instance.Serijalizacija("../../../json/dinamickaOprema.json");
        }

        public void UklanjanjeOpreme(MagacinProzor p)
        {
            if (p.listViewDinamOpreme.SelectedValue != null)
            {
                Model.DinamickaOprema oprema = (Model.DinamickaOprema)p.listViewDinamOpreme.SelectedValue;
                Repozitorijum.DinamickaOprema.Instance.listaOpreme.Remove(oprema);
                Repozitorijum.DinamickaOprema.Instance.Serijalizacija("../../../json/dinamickaOprema.json");
                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
            }
        }

        public void IzmenaOpreme()
        {
            throw new NotImplementedException();
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.StatickaOprema magacin;

    }
}