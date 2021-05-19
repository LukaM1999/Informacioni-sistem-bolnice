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
      
      public void UklanjanjeProstorije(DataGrid ListaProstorija)
      {
            if (ListaProstorija.SelectedValue != null)
            {
                Prostorija pr = (Prostorija)ListaProstorija.SelectedValue;
                Prostorije prostorije = Prostorije.Instance;

                foreach(Termin t in Termini.Instance.listaTermina)
                {
                    if (t.idProstorije == pr.Id)
                    {
                        t.idProstorije = null;
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                    }
                }


                foreach (Prostorija p in prostorije.ListaProstorija)
                {
                    if (p.Id.Equals(pr.Id))
                    {
                        prostorije.ListaProstorija.Remove(p);
                        Prostorije.Instance.Serijalizacija();
                        break;
                    }
                }
                ListaProstorija.ItemsSource = Prostorije.Instance.ListaProstorija;
            }
        }
      
      public void IzmenaProstorije(ProstorijaFormaIzmeni izmena, Prostorija p)
      {
            string stariId = p.Id;
            p.Sprat = Int32.Parse(izmena.tb1.Text);
            p.Tip = (TipProstorije)Enum.Parse(typeof(TipProstorije), izmena.tipIzmena.Text, true);
            p.Id = izmena.tb2.Text;
            if (izmena.rb1.IsChecked == true)
            {
                p.JeZauzeta = true;
            }
            else
            {
                p.JeZauzeta = false;
            }

            foreach (Termin t in Termini.Instance.listaTermina)
            {
                if (t.idProstorije != null)
                {
                    if (t.idProstorije == stariId)
                    {
                        t.idProstorije = p.Id;
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                    }
                }
            }

            Prostorije.Instance.Serijalizacija();
            Prostorije.Instance.Deserijalizacija();
        }
      
      public void PregledProstorije(ProstorijeProzor pr)
      {
            ProstorijaInfoForma p = new ProstorijaInfoForma(pr);
            p.Show();
        }
   
   }
}