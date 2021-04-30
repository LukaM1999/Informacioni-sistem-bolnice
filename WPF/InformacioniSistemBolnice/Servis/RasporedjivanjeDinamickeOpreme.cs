using System;
using System.Linq;
using Model;
using Repozitorijum;

namespace Servis
{
    public class RasporedjivanjeDinamickeOpreme
    {
        private static readonly Lazy<RasporedjivanjeDinamickeOpreme>
           lazy =
           new Lazy<RasporedjivanjeDinamickeOpreme>
               (() => new RasporedjivanjeDinamickeOpreme());

        public static RasporedjivanjeDinamickeOpreme Instance { get { return lazy.Value; } }

        public void Premestanje(Prostorija izProstorije, Prostorija uProstoriju, Model.DinamickaOprema dinamickaOprema, int kolicina)
        {
            if (izProstorije == null)
            {
                Repozitorijum.DinamickaOprema.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOprema.Instance.listaOpreme.IndexOf(dinamickaOprema)).kolicina -= kolicina;
                Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();

                foreach (Model.DinamickaOprema p in Prostorije.Instance.getSelected(uProstoriju).inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.getSelected(uProstoriju).inventar.getSelected(p).kolicina += kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        return;
                    }
                }
                Prostorije.Instance.getSelected(uProstoriju).inventar.dinamickaOprema.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
            }
            if(uProstoriju == null)
            {
                foreach (Model.DinamickaOprema p in Prostorije.Instance.getSelected(izProstorije).inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.getSelected(izProstorije).inventar.getSelected(p).kolicina -= kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();

                        foreach (Model.DinamickaOprema n in Repozitorijum.DinamickaOprema.Instance.listaOpreme)
                        {
                            if (p.tip.Equals(n.tip))
                            {
                                Repozitorijum.DinamickaOprema.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOprema.Instance.listaOpreme.IndexOf(n)).kolicina += kolicina;
                                Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
                                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
                                return;
                            }
                        }

                        
                    }
                }

                Repozitorijum.DinamickaOprema.Instance.listaOpreme.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Repozitorijum.DinamickaOprema.Instance.Serijalizacija();
                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
                return;

            }
            else
            {
                foreach (Model.DinamickaOprema p in Prostorije.Instance.getSelected(izProstorije).inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.getSelected(izProstorije).inventar.getSelected(p).kolicina -= kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Model.DinamickaOprema p in Prostorije.Instance.getSelected(uProstoriju).inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.getSelected(uProstoriju).inventar.getSelected(p).kolicina += kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        return;
                    }
                }
                Prostorije.Instance.getSelected(uProstoriju).inventar.dinamickaOprema.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
            }
            
        }

        public Repozitorijum.StatickaOprema magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}