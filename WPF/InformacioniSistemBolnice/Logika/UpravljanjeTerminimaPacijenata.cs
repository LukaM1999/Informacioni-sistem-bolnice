using System;
using Model;
using RadSaDatotekama;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Logika
{
    public class UpravljanjeTerminimaPacijenata
    {   
        private static readonly Lazy<UpravljanjeTerminimaPacijenata>
           lazy =
           new Lazy<UpravljanjeTerminimaPacijenata>
               (() => new UpravljanjeTerminimaPacijenata());

        public static UpravljanjeTerminimaPacijenata Instance { get { return lazy.Value; } }

        public Dictionary<Termin, int> terminIndeks = new Dictionary<Termin, int>();

        public void Zakazivanje(ListView listaZakazanihTermina, ListView listaSlobodnihTermina)
        {
            Termin t = (Termin)listaSlobodnihTermina.SelectedValue;
            terminIndeks.Add(t, listaSlobodnihTermina.SelectedIndex);
            t.status = StatusTermina.zakazan;
            Termini.Instance.Serijalizacija("../../../json/slobodniTerminiPacijenta");
            listaZakazanihTermina.Items.Add(t);
        }

        public void Otkazivanje(ListView listaZakazanihTermina)
        {
            Termin t = (Termin)listaZakazanihTermina.SelectedValue;
            foreach (KeyValuePair<Termin, int> ti in terminIndeks)
            {
                if (ti.Key == t)
                {
                    Termini.Instance.listaTermina.Insert(ti.Value, t);
                    listaZakazanihTermina.Items.Remove(t);
                    terminIndeks.Remove(ti.Key);
                }
            }
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