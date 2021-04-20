using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;

namespace Servis
{
    public class UpravljanjeStatickomOpremom
    {

        private static readonly Lazy<UpravljanjeStatickomOpremom>
           lazy =
           new Lazy<UpravljanjeStatickomOpremom>
               (() => new UpravljanjeStatickomOpremom());

        public static UpravljanjeStatickomOpremom Instance { get { return lazy.Value; } }

        public void KreiranjeOpreme(MagacinDodajProzor p)
        {
            Model.StatickaOprema oprema = new Model.StatickaOprema(Int32.Parse(p.tbKol.Text), (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), p.cb1.Text));
            Repozitorijum.StatickaOprema.Instance.listaOpreme.Add(oprema);
            Repozitorijum.StatickaOprema.Instance.Serijalizacija("../../../json/statickaOprema.json");
        }

        public void UklanjanjeOpreme(MagacinProzor p)
        {
            if (p.listViewStatOpreme.SelectedValue != null)
            {
                Model.StatickaOprema oprema = (Model.StatickaOprema)p.listViewStatOpreme.SelectedValue;
                Repozitorijum.StatickaOprema.Instance.listaOpreme.Remove(oprema);
                Repozitorijum.StatickaOprema.Instance.Serijalizacija("../../../json/statickaOprema.json");
                Repozitorijum.StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json");
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