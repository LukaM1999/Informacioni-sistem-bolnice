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
            Prostorije.Instance.ListaProstorija.Add(new Prostorija(dto.Sprat, dto.Tip, dto.Id, dto.JeZauzeta, dto.Inventar));
            Prostorije.Instance.Serijalizacija();
            Prostorije.Instance.Deserijalizacija();
        }
      
      public void UklanjanjeProstorije(ProstorijaDto dto)
      {
            Prostorije.Instance.BrisiProstorijuIzSvihTermina(dto.Id);
            if (!Prostorije.Instance.BrisiPoId(dto.Id)) return;
            Prostorije.Instance.SacuvajPromene();            
        }
      
      public void IzmenaProstorije(ProstorijaDto dto)
      {
            IzmeniIzabranuProstoriju(dto);
            Prostorije.Instance.SacuvajPromene();
        }
      private void IzmeniIzabranuProstoriju(ProstorijaDto dto)
        {
            Prostorija izabranaProstorija = Prostorije.Instance.NadjiPoId(dto.Id);
            izabranaProstorija.Sprat = dto.Sprat;
            izabranaProstorija.Tip = dto.Tip;
            izabranaProstorija.Id = dto.Id;
            izabranaProstorija.JeZauzeta = dto.JeZauzeta;
            izabranaProstorija.Inventar = dto.Inventar;
        }
      public void PregledProstorije(ProstorijeProzor pr)
      {
            ProstorijaInfoForma p = new ProstorijaInfoForma(pr);
            p.Show();
        }
   
   }
}