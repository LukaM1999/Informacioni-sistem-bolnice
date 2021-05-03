using System;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using System.Windows.Controls;
namespace Servis
{
   public class UpravljanjeProstorijama
   {
      private static readonly Lazy<UpravljanjeProstorijama>
        lazy =
        new Lazy<UpravljanjeProstorijama>
            (() => new UpravljanjeProstorijama());

      public static UpravljanjeProstorijama Instance { get { return lazy.Value; } }
      
      public void KreiranjeProstorije(ProstorijaForma p)
      {
            Inventar i = new Inventar();
            Model.DinamickaOprema oprema = new Model.DinamickaOprema();
            i.dinamickaOprema.Add(oprema);
            Prostorija prostorija = new Prostorija(Int32.Parse(p.spratUnos.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), p.tipUnos.Text), p.idUnos.Text, false, i);
            Prostorije.Instance.listaProstorija.Add(prostorija);
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
                    if (t.idProstorije == pr.id)
                    {
                        t.idProstorije = null;
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                    }
                }


                foreach (Prostorija p in prostorije.listaProstorija)
                {
                    if (p.id.Equals(pr.id))
                    {
                        prostorije.listaProstorija.Remove(p);
                        Prostorije.Instance.Serijalizacija();
                        break;
                    }
                }
                ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
            }
        }
      
      public void IzmenaProstorije(ProstorijaFormaIzmeni izmena, Prostorija p)
      {
            string stariId = p.id;
            p.sprat = Int32.Parse(izmena.tb1.Text);
            p.tip = (TipProstorije)Enum.Parse(typeof(TipProstorije), izmena.tipIzmena.Text, true);
            p.id = izmena.tb2.Text;
            if (izmena.rb1.IsChecked == true)
            {
                p.jeZauzeta = true;
            }
            else
            {
                p.jeZauzeta = false;
            }

            foreach (Termin t in Termini.Instance.listaTermina)
            {
                if (t.idProstorije != null)
                {
                    if (t.idProstorije == stariId)
                    {
                        t.idProstorije = p.id;
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