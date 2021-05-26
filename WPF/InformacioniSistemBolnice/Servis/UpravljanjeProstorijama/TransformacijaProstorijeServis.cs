using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis.UpravljanjeProstorijama
{
    public class TransformacijaProstorijeServis
    {
        private static readonly Lazy<TransformacijaProstorijeServis> Lazy = new(() => new TransformacijaProstorijeServis());
        public static TransformacijaProstorijeServis Instance { get { return Lazy.Value; } }

        public void ZakaziTerminTransformacijeProstorija(TransformacijaProstorijaDto dto)
        {
            TransformacijaProstorijaRepo.Instance.DodajTerminTransformacijeProstorija(new(dto.Spajanje
                , dto.IdPrveStareProstorije, dto.IdDrugeStareProstorije, dto.PrvaNovaProstorija, dto.DrugaNovaProstorija
                , dto.PocetakRenoviranja, dto.KrajRenoviranja));
            TransformacijaProstorijaRepo.Instance.Serijalizacija();
        }

        public void ProveraTransformacijeProstorija()
        {
            foreach(TransformacijaProstorija termin in TransformacijaProstorijaRepo.Instance.Termini.ToList())
            {
                if(termin.Spajanje == true)
                {
                    if(termin.PocetakRenoviranja <= DateTime.Now)
                        ZauzmiStareProstorije(termin);
                    if (termin.KrajRenoviranja <= DateTime.Now)
                        BrisiStareIKreirajNovuProstoriju(termin);
                }
                else
                {
                    if (termin.PocetakRenoviranja <= DateTime.Now)
                        ZauzmiStaruProstoriju(termin);
                    if (termin.KrajRenoviranja <= DateTime.Now)
                        BrisiStaruIKreirajNoveProstorije(termin);
                }
            }
        }

        private void ZauzmiStareProstorije(TransformacijaProstorija termin)
        {
            Prostorija prvaStaraProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.IdPrveStareProstorije);
            Prostorija drugaStaraProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.IdDrugeStareProstorije);
            prvaStaraProstorija.JeZauzeta = true;
            drugaStaraProstorija.JeZauzeta = true;
            ProstorijaRepo.Instance.Serijalizacija();
        }

        private void BrisiStareIKreirajNovuProstoriju(TransformacijaProstorija termin)
        {
            ProstorijaRepo.Instance.BrisiPoId(termin.IdPrveStareProstorije);
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.BrisiPoId(termin.IdDrugeStareProstorije);
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.DodajProstoriju(termin.PrvaNovaProstorija);
            ProstorijaRepo.Instance.Serijalizacija();
            TransformacijaProstorijaRepo.Instance.BrisiTerminTransformacijeProstorije(termin);
            TransformacijaProstorijaRepo.Instance.Serijalizacija();
        }

        private void ZauzmiStaruProstoriju(TransformacijaProstorija termin)
        {
            Prostorija staraProstorija = ProstorijaRepo.Instance.NadjiPoId(termin.IdPrveStareProstorije);
            staraProstorija.JeZauzeta = true;
            ProstorijaRepo.Instance.Serijalizacija();
        }

        private void BrisiStaruIKreirajNoveProstorije(TransformacijaProstorija termin)
        {
            ProstorijaRepo.Instance.BrisiPoId(termin.IdPrveStareProstorije);
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.DodajProstoriju(termin.PrvaNovaProstorija);
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.DodajProstoriju(termin.DrugaNovaProstorija);
            ProstorijaRepo.Instance.Serijalizacija();
            TransformacijaProstorijaRepo.Instance.BrisiTerminTransformacijeProstorije(termin);
            TransformacijaProstorijaRepo.Instance.Serijalizacija();
        }
    }
}
