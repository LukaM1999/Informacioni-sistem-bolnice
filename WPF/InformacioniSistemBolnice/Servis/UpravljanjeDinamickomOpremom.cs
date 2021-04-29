using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using Model;

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

            foreach (Model.DinamickaOprema so in Repozitorijum.DinamickaOprema.Instance.listaOpreme)
            {
                if (so.tip.Equals(oprema.tip))
                {
                    so.kolicina += oprema.kolicina;
                    Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
                    Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
                    return;
                }
            }

            Repozitorijum.DinamickaOprema.Instance.listaOpreme.Add(oprema);
            Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
            Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
        }

        public void UklanjanjeOpreme(MagacinProzor p)
        {
            if (p.listViewDinamOpreme.SelectedValue != null)
            {
                Model.DinamickaOprema oprema = (Model.DinamickaOprema)p.listViewDinamOpreme.SelectedValue;
                Repozitorijum.DinamickaOprema.Instance.listaOpreme.Remove(oprema);
                Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
            }
        }

        public void IzmenaOpreme(Model.DinamickaOprema oprema, MagacinIzmeniDinamickuOpremu p)
        {
            oprema.kolicina = Int32.Parse(p.tb1.Text);
            oprema.tip = (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), p.cb1.Text, true);

            Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
            Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.StatickaOprema magacin;

    }
}