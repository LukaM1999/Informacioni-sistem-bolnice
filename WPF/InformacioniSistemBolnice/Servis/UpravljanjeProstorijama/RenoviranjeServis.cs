using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice.Servis.UpravljanjeProstorijama;

namespace Servis
{
    public class RenoviranjeServis
    {
        private static readonly Lazy<RenoviranjeServis> Lazy = new(() => new RenoviranjeServis());
        public static RenoviranjeServis Instance { get { return Lazy.Value; } }

        public void ZakazivanjeRenoviranja(ProstorijaRenoviranjeDto dto)
        {
            RenoviranjeTermin novTermin = new RenoviranjeTermin(dto.PocetakRenoviranja, dto.KrajRenoviranja, dto.Prostorija.Id);
            Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.Prostorija.Id);
            izabranaProstorija.Renoviranje = novTermin;
            ProstorijaRepo.Instance.Serijalizacija();
            RenoviranjeRepo.Instance.DodajTerminRenoviranja(novTermin);
            RenoviranjeRepo.Instance.Serijalizacija();
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
                TransformacijaProstorijeServis.Instance.ProveraTransformacijeProstorija();
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
                Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(prostorija.Id);
                izabranaProstorija.JeZauzeta = true;
                ProstorijaRepo.Instance.Serijalizacija();
                break;
            }
        }

        public void OslobodiProstoriju(RenoviranjeTermin termin)
        {
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije.ToList())
            {
                if (!prostorija.Id.Equals(termin.idProstorije)) continue;
                Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(prostorija.Id);
                izabranaProstorija.JeZauzeta = false;
                izabranaProstorija.Renoviranje = null;
                RenoviranjeRepo.Instance.BrisiTerminRenoviranja(termin);
                ProstorijaRepo.Instance.Serijalizacija();
                RenoviranjeRepo.Instance.Serijalizacija();
                break;
            }
        }
    }
}
