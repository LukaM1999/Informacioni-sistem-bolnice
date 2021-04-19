using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class UpravljanjeTerminimaPacijenata
    {
        private static readonly Lazy<UpravljanjeTerminimaPacijenata>
           lazy =
           new Lazy<UpravljanjeTerminimaPacijenata>
               (() => new UpravljanjeTerminimaPacijenata());

        public static UpravljanjeTerminimaPacijenata Instance { get { return lazy.Value; } }

        public void Zakazivanje(IzborTermina izborTermina, string jmbgPacijenta)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg == jmbgPacijenta)
                {
                    foreach (Termin vecZakazan in pacijent.zakazaniTermini)
                    {
                        if (vecZakazan == (Termin)izborTermina.ponudjeniTermini.SelectedItem)
                        {
                            return;
                        }
                    }
                    foreach (Lekar lekar in Lekari.Instance.listaLekara)
                    {
                        if (lekar.jmbg == ((Termin)izborTermina.ponudjeniTermini.SelectedItem).lekarJMBG)
                        {
                            pacijent.zakazaniTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            lekar.zauzetiTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Termini.Instance.listaTermina.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Lekari.Instance.Serijalizacija("../../../json/lekari.json");
                            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                            Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
                            Lekari.Instance.Deserijalizacija("../../../json/lekari.json");
                            Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
                            izborTermina.zakazivanjeTerminaPacijenta.terminiPacijentaProzor.listaZakazanihTermina.ItemsSource
                                = pacijent.zakazaniTermini;
                            izborTermina.zakazivanjeTerminaPacijenta.Close();
                            izborTermina.Close();
                            break;
                        }
                    }
                }
            }
        }

        public void Otkazivanje(DataGrid listaZakazanihTermina)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                Termin t = (Termin)listaZakazanihTermina.SelectedItem;

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
                {
                    if (pacijent.jmbg == t.pacijentJMBG)
                    {
                        foreach (Termin termin in pacijent.zakazaniTermini)
                        {
                            if (termin.vreme == t.vreme)
                            {
                                pacijent.zakazaniTermini.Remove(termin);
                                Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
                                Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
                                listaZakazanihTermina.ItemsSource = null;
                                listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
                                break;
                            }
                        }

                    }
                }

                foreach (Lekar lekar in Lekari.Instance.listaLekara)
                {
                    if (lekar.jmbg == t.lekarJMBG)
                    {   
                        foreach(Termin termin in lekar.zauzetiTermini.ToList())
                        {
                            if (termin.vreme == t.vreme)
                            {
                                lekar.zauzetiTermini.Remove(termin);
                                Lekari.Instance.Serijalizacija("../../../json/lekari.json");
                                Lekari.Instance.Deserijalizacija("../../../json/lekari.json");
                                break;
                            }
                        }
                    }
                }

                foreach(Termin termin in Termini.Instance.listaTermina.ToList())
                {
                    if (termin.vreme == t.vreme)
                    {
                        Termini.Instance.listaTermina.Remove(termin);
                        Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                        Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
                    }
                }                
            }
        }

        public void Pomeranje(PomeranjeTerminaPacijentaProzor pomeranje)
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

                pomeranje.zakazanTermin.vreme = datumTermina;
                pomeranje.zakazanTermin.status = StatusTermina.pomeren;


                Termini.Instance.Serijalizacija("../../../json/zakazaniTermini.json");
                Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");

                pomeranje.zakazaniTermini.ItemsSource = Termini.Instance.listaTermina;
                pomeranje.Close();
            }
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            TerminInfoProzor terminInfo = new TerminInfoProzor(listaZakazanihTermina);
            terminInfo.Show();
        }

    }
}