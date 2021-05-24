using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;
namespace Servis
{
   public class UpravljanjeProstorijama
   {
      private static readonly Lazy<UpravljanjeProstorijama> lazy = new Lazy<UpravljanjeProstorijama> (() => new UpravljanjeProstorijama());

      public static UpravljanjeProstorijama Instance { get { return lazy.Value; } }
      
      public void KreiranjeProstorije(ProstorijaDto dto)
      {
            ProstorijaRepo.Instance.Prostorije.Add(new Prostorija(dto.Sprat, dto.Tip, dto.Id, dto.JeZauzeta, dto.Inventar));
            ProstorijaRepo.Instance.Serijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
        }
      
      public void UklanjanjeProstorije(ProstorijaDto dto)
      {
            ProstorijaRepo.Instance.BrisiProstorijuIzSvihTermina(dto.Id);
            if (!ProstorijaRepo.Instance.BrisiPoId(dto.Id)) return;
            ProstorijaRepo.Instance.SacuvajPromene();            
       }
      
      public void IzmenaProstorije(ProstorijaDto dto)
      {
            IzmeniIzabranuProstoriju(dto);
            ProstorijaRepo.Instance.SacuvajPromene();
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