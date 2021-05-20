using System;
using Model;
using Repozitorijum;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using InformacioniSistemBolnice;

namespace Servis
{
    public class RasporedjivanjeStatickeOpreme
    {

        private static readonly Lazy<RasporedjivanjeStatickeOpreme>
           lazy =
           new Lazy<RasporedjivanjeStatickeOpreme>
               (() => new RasporedjivanjeStatickeOpreme());

        public static RasporedjivanjeStatickeOpreme Instance { get { return lazy.Value; } }

        public void ZakazivanjePremestanja( Prostorija izProstorije, Prostorija uProstoriju, Model.StatickaOprema statickaOprema, int kolicina, DateTime datum)
        {
            if (izProstorije == null)
            {
                StatickaOpremaTermin novTermin1 = new StatickaOpremaTermin(izProstorije, uProstoriju, statickaOprema, kolicina, datum);
                StatickaOpremaTermini.Instance.listaTermina.Add(novTermin1);
                StatickaOpremaTermini.Instance.Serijalizacija();
                StatickaOpremaTermini.Instance.Deserijalizacija();
                return;
            }
            if (uProstoriju == null)
            {
                StatickaOpremaTermin novTermin2 = new StatickaOpremaTermin(izProstorije, uProstoriju, statickaOprema, kolicina, datum);
                StatickaOpremaTermini.Instance.listaTermina.Add(novTermin2);
                StatickaOpremaTermini.Instance.Serijalizacija();
                StatickaOpremaTermini.Instance.Deserijalizacija();
                return;
            }
            StatickaOpremaTermin novTermin3 = new StatickaOpremaTermin(izProstorije, uProstoriju, statickaOprema, kolicina, datum);
            StatickaOpremaTermini.Instance.listaTermina.Add(novTermin3);
            StatickaOpremaTermini.Instance.Serijalizacija();
            StatickaOpremaTermini.Instance.Deserijalizacija();
        }

        public void ProveraPremestajaOpreme()
        {
            while (true)
            {
                //while(StatickaOpremaTermini.Instance.listaTermina.ToList().Count == 0)
                //{
                //    Thread.Sleep(10000);
                //}
                int temp = 0;
                foreach(StatickaOpremaTermin t in StatickaOpremaTermini.Instance.listaTermina.ToList())
                {
                    if (t.datumPremestaja.Year <= DateTime.Now.Year && t.datumPremestaja.Day <= DateTime.Now.Day && t.datumPremestaja.Month <= DateTime.Now.Month)
                    {
                        if(t.izProstorije == null)
                        {
                            foreach (Model.StatickaOprema s in t.uProstoriju.Inventar.StatickaOprema.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Prostorije.Instance.NadjiPoId(t.uProstoriju.Id).Inventar.getSelectedS(s).kolicina += t.kolicina;
                                    temp = 1;
                                    break;
                                }
                            }
                            if (temp != 1)
                            {
                                Model.StatickaOprema stat = new Model.StatickaOprema(t.kolicina, t.oprema.tip);
                                Prostorije.Instance.NadjiPoId(t.uProstoriju.Id).Inventar.StatickaOprema.Add(stat);
                                temp = 0;
                            }
                            
                            Prostorije.Instance.Serijalizacija();
                            Prostorije.Instance.Deserijalizacija();
                            foreach (Model.StatickaOprema s in Repozitorijum.StatickaOpremaRepo.Instance.listaOpreme.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Repozitorijum.StatickaOpremaRepo.Instance.getSelected(s).kolicina -= t.kolicina;
                                    break;
                                }
                            }
                            Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
                            Repozitorijum.StatickaOpremaRepo.Instance.Deserijalizacija();

                            StatickaOpremaTermini.Instance.listaTermina.Remove(t);
                            StatickaOpremaTermini.Instance.Serijalizacija();
                            StatickaOpremaTermini.Instance.Deserijalizacija();
                        }
                        if (t.uProstoriju == null)
                        {
                            foreach (Model.StatickaOprema s in t.izProstorije.Inventar.StatickaOprema.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Prostorije.Instance.NadjiPoId(t.izProstorije.Id).Inventar.getSelectedS(s).kolicina -= t.kolicina;
                                    break;
                                }
                            }
                            Prostorije.Instance.Serijalizacija();
                            Prostorije.Instance.Deserijalizacija();
                            foreach (Model.StatickaOprema s in Repozitorijum.StatickaOpremaRepo.Instance.listaOpreme.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Repozitorijum.StatickaOpremaRepo.Instance.getSelected(s).kolicina += t.kolicina;
                                    break;
                                }
                            }
                            Repozitorijum.StatickaOpremaRepo.Instance.Serijalizacija();
                            Repozitorijum.StatickaOpremaRepo.Instance.Deserijalizacija();

                            StatickaOpremaTermini.Instance.listaTermina.Remove(t);
                            StatickaOpremaTermini.Instance.Serijalizacija();
                            StatickaOpremaTermini.Instance.Deserijalizacija();
                        }
                        if (t.uProstoriju != null && t.izProstorije != null)
                        {
                            foreach (Model.StatickaOprema s in t.uProstoriju.Inventar.StatickaOprema.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Prostorije.Instance.NadjiPoId(t.uProstoriju.Id).Inventar.getSelectedS(s).kolicina += t.kolicina;
                                    temp = 1;
                                    break;
                                }
                            }
                            if (temp != 1)
                            {
                                Model.StatickaOprema stat = new Model.StatickaOprema(t.kolicina, t.oprema.tip);
                                Prostorije.Instance.NadjiPoId(t.uProstoriju.Id).Inventar.StatickaOprema.Add(stat);
                                temp = 0;
                            }

                            foreach (Model.StatickaOprema s in t.izProstorije.Inventar.StatickaOprema.ToList())
                            {
                                if (s.tip.Equals(t.oprema.tip))
                                {
                                    Prostorije.Instance.NadjiPoId(t.izProstorije.Id).Inventar.getSelectedS(s).kolicina -= t.kolicina;
                                    break;
                                }
                            }
                            
                            Prostorije.Instance.Serijalizacija();
                            Prostorije.Instance.Deserijalizacija();
                            

                            StatickaOpremaTermini.Instance.listaTermina.Remove(t);
                            StatickaOpremaTermini.Instance.Serijalizacija();
                            StatickaOpremaTermini.Instance.Deserijalizacija();
                        }
                    }
                }
            }
        }

        public Repozitorijum.StatickaOpremaRepo magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}