using System;
using Model;
using RadSaDatotekama;
using InformacioniSistemBolnice;
using System.Windows.Controls;
namespace Logika
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
            Prostorija prostorija = new Prostorija(Int32.Parse(p.spratUnos.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), p.tipUnos.Text), p.idUnos.Text, false);
            Prostorije.Instance.listaProstorija.Add(prostorija);
            Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
        }
      
      public void UklanjanjeProstorije(ListView ListaProstorija)
      {
            if (ListaProstorija.SelectedValue != null)
            {
                Prostorija pr = (Prostorija)ListaProstorija.SelectedValue;
                Prostorije prostorije = Prostorije.Instance;
                foreach (Prostorija p in prostorije.listaProstorija)
                {
                    if (p.id.Equals(pr.id))
                    {
                        prostorije.listaProstorija.Remove(p);
                        Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
                        break;
                    }
                }
            }
        }
      
      public void IzmenaProstorije(ProstorijaFormaIzmeni izmena, Prostorija p)
      {
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
            Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
            Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
        }
      
      public void PregledProstorije(Prostorija pr)
      {
            ProstorijaInfoForma p = new ProstorijaInfoForma(pr);
            p.Show();
        }
   
   }
}