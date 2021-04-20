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

        public void KreiranjeOpreme(MagacinDodajProzor p)
        {
            Model.StatickaOprema oprema = new Model.StatickaOprema(Int32.Parse(p.tbKol.Text), (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), p.cb1.Text));
            
            foreach (Model.StatickaOprema so in Repozitorijum.StatickaOprema.Instance.listaOpreme)
            {
                if (so.tip.Equals(oprema.tip))
                {
                    so.kolicina += oprema.kolicina;
                    Repozitorijum.StatickaOprema.Instance.Serijalizacija("../../../json/statickaOprema.json");
                    Repozitorijum.StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json");
                    return;
                }
            }

            Repozitorijum.StatickaOprema.Instance.listaOpreme.Add(oprema);
            Repozitorijum.StatickaOprema.Instance.Serijalizacija("../../../json/statickaOprema.json");
            Repozitorijum.StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json");
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

        public void IzmenaOpreme(Model.StatickaOprema oprema, MagacinIzmeniProzor p)
        {
             oprema.kolicina = Int32.Parse(p.tb1.Text);
             oprema.tip = (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), p.cb1.Text, true);

             Repozitorijum.StatickaOprema.Instance.Serijalizacija("../../../json/statickaOprema.json");
             Repozitorijum.StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json"); 
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.StatickaOprema magacin;

    }
}