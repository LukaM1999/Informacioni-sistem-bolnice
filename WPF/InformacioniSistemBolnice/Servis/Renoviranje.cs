﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    class Renoviranje
    {
        private static readonly Lazy<Renoviranje>
           lazy =
           new Lazy<Renoviranje>
               (() => new Renoviranje());

        public static Renoviranje Instance { get { return lazy.Value; } }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeTermin novTermin = new RenoviranjeTermin(dto.PocetakRenoviranja, dto.KrajRenoviranja, dto.Prostorija.id);
            Prostorije.Instance.uzmiIzabranuProstoriju(dto.Prostorija).Renoviranje = novTermin;
            Prostorije.Instance.Serijalizacija();
            Prostorije.Instance.Deserijalizacija();
            RenoviranjeTermini.Instance.listaTermina.Add(novTermin);
            RenoviranjeTermini.Instance.Serijalizacija();
            RenoviranjeTermini.Instance.Deserijalizacija();
        }

        public void ProveraRenoviranja()
        {
            while (true)
            {
                foreach(RenoviranjeTermin termin in RenoviranjeTermini.Instance.listaTermina.ToList())
                {
                    if(daLiJeRenoviranjePocelo(termin))
                    {
                        zauzmiProstoriju(termin);
                    }
                    if (daLiSeRenoviranjeZavrsilo(termin))
                    {
                        oslobodiProstoriju(termin);
                    }
                }
            }
        }

        public bool daLiJeRenoviranjePocelo(RenoviranjeTermin termin)
        {
            return termin.PocetakRenoviranja <= DateTime.Now;
        }

        public bool daLiSeRenoviranjeZavrsilo(RenoviranjeTermin termin)
        {
            return termin.KrajRenoviranja <= DateTime.Now;
        }

        public void zauzmiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id.Equals(termin.idProstorije))
                {
                    Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).jeZauzeta = true;
                    Prostorije.Instance.Serijalizacija();
                    Prostorije.Instance.Deserijalizacija();
                }
            }
        }

        public void oslobodiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id.Equals(termin.idProstorije))
                {
                    Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).jeZauzeta = false;
                    Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).Renoviranje = null;
                    RenoviranjeTermini.Instance.listaTermina.Remove(termin);
                    Prostorije.Instance.Serijalizacija();
                    Prostorije.Instance.Deserijalizacija();
                    RenoviranjeTermini.Instance.Serijalizacija();
                    RenoviranjeTermini.Instance.Deserijalizacija();
                }
            }
        }
    }
}
