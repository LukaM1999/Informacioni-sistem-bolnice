using System;
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
            RenoviranjeTermin novTermin = new RenoviranjeTermin(dto.PocetakRenoviranja, dto.KrajRenoviranja, dto.Prostorija.Id);
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
                foreach (RenoviranjeTermin termin in RenoviranjeTermini.Instance.listaTermina.ToList())
                {
                    if (!JeRenoviranjePocelo(termin)) continue;
                    ZauzmiProstoriju(termin);
                    if (!SeRenoviranjeZavrsilo(termin)) continue;
                    OslobodiProstoriju(termin);
                }
            }
        }

        public bool JeRenoviranjePocelo(RenoviranjeTermin termin)
        {
            return termin.PocetakRenoviranja <= DateTime.Now;
        }

        public bool SeRenoviranjeZavrsilo(RenoviranjeTermin termin)
        {
            return termin.KrajRenoviranja <= DateTime.Now;
        }

        public void ZauzmiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija.ToList())
            {
                if (!prostorija.Id.Equals(termin.idProstorije)) continue;
                Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).JeZauzeta = true;
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
                break;
            }
        }

        public void OslobodiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija.ToList())
            {
                if (!prostorija.Id.Equals(termin.idProstorije)) continue;
                Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).JeZauzeta = false;
                Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).Renoviranje = null;
                RenoviranjeTermini.Instance.listaTermina.Remove(termin);
                Prostorije.Instance.Serijalizacija();
                Prostorije.Instance.Deserijalizacija();
                RenoviranjeTermini.Instance.Serijalizacija();
                RenoviranjeTermini.Instance.Deserijalizacija();
                break;
            }
        }
    }
}
