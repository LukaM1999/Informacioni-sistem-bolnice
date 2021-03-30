using System;
using Model;
using RadSaDatotekama;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;

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

        public void Zakazivanje(ZakazivanjeTerminaLekaraProzor zakazivanje)
        {
            if (zakazivanje.listaSati.SelectedIndex >= 0 && zakazivanje.datumTermina.SelectedDate != null)
            {
                DateTime datumTermina = (DateTime)zakazivanje.datumTermina.SelectedDate;
                string datumVrednost = (string)zakazivanje.listaSati.SelectedValue;
                string[] satiMinuti = datumVrednost.Split(":");
                double sat = double.Parse(satiMinuti[0]);
                if (satiMinuti[1].Equals("30"))
                {
                    sat += 0.5;
                }

                datumTermina = datumTermina.AddHours(sat);
                foreach (Termin t in Termini.Instance.listaTermina)
                {
                    if (t.vreme == datumTermina)
                    {
                        return;
                    }
                }

                Termin zakazanTermin = new Termin(datumTermina, double.Parse(zakazivanje.trajanjeTerminaUnos.Text), 
                    (TipTermina)Enum.Parse(typeof(TipTermina), zakazivanje.tipTerminaUnos.Text), StatusTermina.zakazan);

                foreach (Prostorija p in Prostorije.Instance.listaProstorija)
                {
                    if (p.id.Equals((string)zakazivanje.sala.SelectedItem))
                    {
                        zakazanTermin.prostorija = p;
                        break;
                    }
                }

                Termini.Instance.listaTermina.Add(zakazanTermin);
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                zakazivanje.Close();
            }
        }

        public void Otkazivanje(ListView listaZakazanihTerminaLekara)
      {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                Termin t = (Termin)listaZakazanihTerminaLekara.SelectedValue;
                Termini.Instance.listaTermina.Remove(t);
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
            }
        }
      
      public void Pomeranje(PomeranjeTerminaLekaraProzor pomeranje)
      {
            if (pomeranje.listaSati.SelectedIndex >= 0 && pomeranje.datumTermina.Text != null && pomeranje.tipTerminaUnos.SelectedIndex >= 0)
            {
                DateTime datumTermina = (DateTime)pomeranje.datumTermina.SelectedDate;
                string datumVrednost = (string)pomeranje.listaSati.SelectedValue;
                string[] satiMinuti = datumVrednost.Split(":");
                double sat = double.Parse(satiMinuti[0]);
                if (satiMinuti[1].Equals("30"))
                {
                    sat += 0.5;
                }

                datumTermina = datumTermina.AddHours(sat);

                if (!datumTermina.Equals(pomeranje.zakazanTermin.vreme))
                {
                    pomeranje.zakazanTermin.status = StatusTermina.pomeren;
                }

                pomeranje.zakazanTermin.vreme = datumTermina;
                pomeranje.zakazanTermin.trajanje = double.Parse(pomeranje.trajanjeTerminaUnos.Text);
                pomeranje.zakazanTermin.tipTermina = (TipTermina)Enum.Parse(typeof(TipTermina), pomeranje.tipTerminaUnos.Text);
                foreach (Prostorija p in Prostorije.Instance.listaProstorija)
                {
                    if (p.id.Equals((string)pomeranje.sala.SelectedItem))
                    {
                        pomeranje.zakazanTermin.prostorija = p;
                        break;
                    }
                }


                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");

                pomeranje.zakazaniTermini.ItemsSource = Termini.Instance.listaTermina;
                pomeranje.Close();

            }
        }
      
      public void Uvid(ListView listaZakazanihTerminaLekara)
      {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                TerminInfoProzor terminInfo = new TerminInfoProzor(listaZakazanihTerminaLekara);
                terminInfo.Show();
            }
        }
   
   }
}