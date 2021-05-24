using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace Servis
{
    public class RenoviranjeServis
    {
        private static readonly Lazy<RenoviranjeServis>
           Lazy =
           new Lazy<RenoviranjeServis>
               (() => new RenoviranjeServis());

        public static RenoviranjeServis Instance { get { return Lazy.Value; } }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeTermin novTermin = new RenoviranjeTermin(dto.PocetakRenoviranja, dto.KrajRenoviranja, dto.Prostorija.Id);
            ProstorijaRepo.Instance.NadjiPoId(dto.Prostorija.Id).Renoviranje = novTermin;
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            RenoviranjeRepo.Instance.RenoviranjeTermini.Add(novTermin);
            RenoviranjeRepo.Instance.Serijalizacija();
            RenoviranjeRepo.Instance.Deserijalizacija();
        }

        public void ProveraRenoviranja()
        {
            while (true)
            {
                foreach (RenoviranjeTermin termin in RenoviranjeRepo.Instance.RenoviranjeTermini.ToList())
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
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije.ToList())
            {
                if (!prostorija.Id.Equals(termin.idProstorije)) continue;
                ProstorijaRepo.Instance.NadjiPoId(prostorija.Id).JeZauzeta = true;
                ProstorijaRepo.Instance.Serijalizacija();
                ProstorijaRepo.Instance.Deserijalizacija();
                break;
            }
        }

        public void OslobodiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije.ToList())
            {
                if (!prostorija.Id.Equals(termin.idProstorije)) continue;
                ProstorijaRepo.Instance.NadjiPoId(prostorija.Id).JeZauzeta = false;
                ProstorijaRepo.Instance.NadjiPoId(prostorija.Id).Renoviranje = null;
                RenoviranjeRepo.Instance.RenoviranjeTermini.Remove(termin);
                ProstorijaRepo.Instance.Serijalizacija();
                ProstorijaRepo.Instance.Deserijalizacija();
                RenoviranjeRepo.Instance.Serijalizacija();
                RenoviranjeRepo.Instance.Deserijalizacija();
                break;
            }
        }
    }
}
