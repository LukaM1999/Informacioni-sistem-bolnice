using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;

namespace Servis
{
   public class ProstorijaServis
   {
      private static readonly Lazy<ProstorijaServis> Lazy = new(() => new ProstorijaServis());
      public static ProstorijaServis Instance => Lazy.Value;

      public void KreiranjeProstorije(ProstorijaDto dto)
      {
            ProstorijaRepo.Instance.DodajProstoriju(new Prostorija(dto.Sprat, dto.Tip, dto.Id, dto.JeZauzeta, dto.Inventar));
            ProstorijaRepo.Instance.Serijalizacija();
        }
      
      public void UklanjanjeProstorije(ProstorijaDto dto)
      {
            ProstorijaRepo.Instance.BrisiProstorijuIzSvihTermina(dto.Id);
            if (!ProstorijaRepo.Instance.BrisiPoId(dto.Id)) return;
            ProstorijaRepo.Instance.Serijalizacija();            
       }
      
      public void IzmenaProstorije(ProstorijaDto dto)
      {
            IzmeniIzabranuProstoriju(dto);
            ProstorijaRepo.Instance.Serijalizacija();
       }
      private void IzmeniIzabranuProstoriju(ProstorijaDto dto)
       {
            Prostorija izabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(dto.Id);
            izabranaProstorija.Sprat = dto.Sprat;
            izabranaProstorija.Tip = dto.Tip;
            izabranaProstorija.Id = dto.Id;
            izabranaProstorija.JeZauzeta = dto.JeZauzeta;
            izabranaProstorija.Inventar = dto.Inventar;
       }
   }
}