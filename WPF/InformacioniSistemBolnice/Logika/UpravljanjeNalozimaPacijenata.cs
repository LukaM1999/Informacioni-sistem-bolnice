using System;
using Model;
using RadSaDatotekama;
using System.Windows.Controls;
using InformacioniSistemBolnice;


namespace Logika
{
   public class UpravljanjeNalozimaPacijenata
   {
        private static readonly Lazy<UpravljanjeNalozimaPacijenata>
          lazy =
          new Lazy<UpravljanjeNalozimaPacijenata>
              (() => new UpravljanjeNalozimaPacijenata());

        public static UpravljanjeNalozimaPacijenata Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga()
      {
            RegistracijaPacijentaForma rP = new RegistracijaPacijentaForma();
            rP.Show();
           
      }
      
      public void UklanjanjeNaloga(ListView ListaPacijenata)
      {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent pacijent = (Pacijent)ListaPacijenata.SelectedValue;
                Pacijenti pacijenti = Pacijenti.Instance;
                foreach (Pacijent p in pacijenti.listaPacijenata)
                {
                    if (p.jmbg.Equals(pacijent.jmbg))
                    {
                        pacijenti.listaPacijenata.Remove(p);
                        Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                        break;
                    }
                }
            }
        }
      
      public void IzmenaNaloga(Pacijent pacijent)
      {
         throw new NotImplementedException();
      }
      
      public void PregledNaloga(Pacijent pacijent)
      {
         throw new NotImplementedException();
      }
   
   }
}