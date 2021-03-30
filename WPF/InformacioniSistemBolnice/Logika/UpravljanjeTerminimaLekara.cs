using System;
using Model;
using RadSaDatotekama;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Logika
{
   public class UpravljanjeTerminimaLekara
   {
        private static readonly Lazy<UpravljanjeTerminimaLekara>
           lazy =
           new Lazy<UpravljanjeTerminimaLekara>
               (() => new UpravljanjeTerminimaLekara());

        public static UpravljanjeTerminimaLekara Instance { get { return lazy.Value; } }

        public Dictionary<Termin, int> terminIndeks = new Dictionary<Termin, int>();

        public void Zakazivanje(ListView listaZakazanihTermina, ListView listaSlobodnihTermina)
        {
            Termin t = (Termin)listaSlobodnihTermina.SelectedValue;
            terminIndeks.Add(t, listaSlobodnihTermina.SelectedIndex);
            t.status = StatusTermina.zakazan;
            Termini.Instance.Serijalizacija("../../../json/slobodniTerminiPacijenta.json");
            listaZakazanihTermina.Items.Add(t);
        }

        public void Otkazivanje(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void Pomeranje(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void Uvid(Termin termin)
      {
         throw new NotImplementedException();
      }
   
   }
}