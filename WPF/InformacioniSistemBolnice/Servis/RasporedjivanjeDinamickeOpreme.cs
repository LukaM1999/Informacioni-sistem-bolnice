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
                Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme.IndexOf(dinamickaOprema)).kolicina -= kolicina;
                Repozitorijum.DinamickaOpremaRepo.Instance.Serijalizacija();
                Repozitorijum.DinamickaOpremaRepo.Instance.Deserijalizacija();

                foreach (Model.DinamickaOprema p in Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.getSelected(p).kolicina += kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        return;
                    }
                }
                Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.dinamickaOprema.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
                return;
            }
            if(uProstoriju == null)
            {
                foreach (Model.DinamickaOprema p in Prostorije.Instance.UzmiIzabranuProstoriju(izProstorije).Inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.UzmiIzabranuProstoriju(izProstorije).Inventar.getSelected(p).kolicina -= kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        Repozitorijum.DinamickaOpremaRepo.Instance.Deserijalizacija();

                        foreach (Model.DinamickaOprema n in Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme)
                        {
                            if (p.tip.Equals(n.tip))
                            {
                                Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme.IndexOf(n)).kolicina += kolicina;
                                Repozitorijum.DinamickaOpremaRepo.Instance.Serijalizacija();
                                Repozitorijum.DinamickaOpremaRepo.Instance.Deserijalizacija();
                                return;
                            }
                        }

                        
                    }
                }

                Repozitorijum.DinamickaOpremaRepo.Instance.listaOpreme.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Repozitorijum.DinamickaOpremaRepo.Instance.Serijalizacija();
                Repozitorijum.DinamickaOpremaRepo.Instance.Deserijalizacija();
                return;

            }
            else
            {
                foreach (Model.DinamickaOprema p in Prostorije.Instance.UzmiIzabranuProstoriju(izProstorije).Inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.UzmiIzabranuProstoriju(izProstorije).Inventar.getSelected(p).kolicina -= kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Model.DinamickaOprema p in Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(dinamickaOprema.tip))
                    {
                        Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.getSelected(p).kolicina += kolicina;
                        Prostorije.Instance.Serijalizacija();
                        Prostorije.Instance.Deserijalizacija();
                        return;
                    }
                }
                Prostorije.Instance.UzmiIzabranuProstoriju(uProstoriju).Inventar.dinamickaOprema.Add(new Model.DinamickaOprema(kolicina, dinamickaOprema.tip));
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
            }
            
        }

        public Repozitorijum.StatickaOprema magacin;
        public Repozitorijum.Prostorije prostorije;

    }
}