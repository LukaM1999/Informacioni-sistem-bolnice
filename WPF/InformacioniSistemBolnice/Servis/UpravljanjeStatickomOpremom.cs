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
            
            foreach (Model.StatickaOprema so in Repozitorijum.StatickaOpremaRepo.Instance.ListaOpreme)
            {
                if (so.Tip.Equals(oprema.Tip))
                {
                    so.Kolicina += oprema.Kolicina;
                    Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
                    Repozitorijum.StatickaOpremaRepo.Instance.Deserijalizacija();
                    return;
                }
            }

            Repozitorijum.StatickaOpremaRepo.Instance.ListaOpreme.Add(oprema);
            Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
            Repozitorijum.StatickaOpremaRepo.Instance.Deserijalizacija();
        }

        public void UklanjanjeOpreme(MagacinProzor p)
        {
            if (p.listViewStatOpreme.SelectedValue != null)
            {
                Model.StatickaOprema oprema = (Model.StatickaOprema)p.listViewStatOpreme.SelectedValue;
                Repozitorijum.StatickaOpremaRepo.Instance.ListaOpreme.Remove(oprema);
                Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
            }
        }

        public void IzmenaOpreme(Model.StatickaOprema oprema, MagacinIzmeniProzor p)
        {
             oprema.Kolicina = Int32.Parse(p.tb1.Text);
             oprema.Tip = (TipStatickeOpreme)Enum.Parse(typeof(TipStatickeOpreme), p.cb1.Text, true);

             Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
             Repozitorijum.StatickaOpremaRepo.Instance.Deserijalizacija(); 
        }

        public void PregledOpreme()
        {
            throw new NotImplementedException();
        }

        public Repozitorijum.StatickaOpremaRepo magacin;

    }
}