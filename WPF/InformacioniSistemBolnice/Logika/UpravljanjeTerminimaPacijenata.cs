using System;
using Model;
using RadSaDatotekama;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;

namespace Logika
{
    public class UpravljanjeTerminimaPacijenata
    {
        private static readonly Lazy<UpravljanjeTerminimaPacijenata>
           lazy =
           new Lazy<UpravljanjeTerminimaPacijenata>
               (() => new UpravljanjeTerminimaPacijenata());

        public static UpravljanjeTerminimaPacijenata Instance { get { return lazy.Value; } }

        public void Zakazivanje(ZakazivanjeTerminaPacijentaProzor zakazivanje)
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

                Termin zakazanTermin = new Termin(datumTermina, 30, TipTermina.pregled, StatusTermina.zakazan);
                Termini.Instance.listaTermina.Add(zakazanTermin);
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                zakazivanje.Close();
            }
        }
        public void Otkazivanje(ListView listaZakazanihTermina)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                Termin t = (Termin)listaZakazanihTermina.SelectedValue;
                Termini.Instance.listaTermina.Remove(t);
                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
            }
        }

        public void Pomeranje(PomeranjeTerminaPacijentaProzor pomeranje, ListView zakazaniTermini, Termin zakazanTermin)
        {
            if (pomeranje.listaSati.SelectedIndex >= 0 && pomeranje.datumTermina.SelectedDate != null)
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

                foreach (Termin t in Termini.Instance.listaTermina)
                {
                    if (t.vreme == datumTermina)
                    {
                        return;
                    }
                }


                zakazanTermin.vreme = datumTermina;
                zakazanTermin.status = StatusTermina.pomeren;


                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");

                zakazaniTermini.ItemsSource = Termini.Instance.listaTermina;
                pomeranje.Close();
            }
        }

        public void Uvid(ListView listaZakazanihTermina)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                TerminInfoProzor terminInfo = new TerminInfoProzor(listaZakazanihTermina);
                terminInfo.Show();
            }
        }

    }
}