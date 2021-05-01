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
                            ((Termin)izborTermina.ponudjeniTermini.SelectedItem).status = StatusTermina.zakazan;
                            pacijent.zakazaniTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            lekar.zauzetiTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Termini.Instance.listaTermina.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                            Lekari.Instance.Serijalizacija();
                            Pacijenti.Instance.Serijalizacija();
                            Termini.Instance.Serijalizacija();
                            Pacijenti.Instance.Deserijalizacija();
                            Lekari.Instance.Deserijalizacija();
                            Termini.Instance.Deserijalizacija();
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
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();
                                listaZakazanihTermina.ItemsSource = null;
                                listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
                               
                                break;
                            }
                        }

                    }
                }

                foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
                {
                    if (lekar.jmbg == t.lekarJMBG)
                    {
                        foreach (Termin termin in lekar.zauzetiTermini.ToList())
                        {
                            if (termin.vreme == t.vreme)
                            {
                                lekar.zauzetiTermini.Remove(termin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }

                foreach (Termin termin in Termini.Instance.listaTermina.ToList())
                {
                    if (termin.vreme == t.vreme)
                    {
                        Termini.Instance.listaTermina.Remove(termin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                    }
                }
            }
        }

        public void Pomeranje(PomeranjeTerminaPacijentaProzor pomeranje)
        {
            if (pomeranje.ponudjeniTermini.SelectedIndex >= 0)
            {
                DateTime staroVreme = ((Termin)pomeranje.terminiPacijenta.listaZakazanihTermina.SelectedItem).vreme;
                Termin noviTermin = (Termin)pomeranje.ponudjeniTermini.SelectedItem;
                noviTermin.status = StatusTermina.pomeren;

                foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
                {
                    if (stariTermin.vreme == staroVreme)
                    {
                        Termini.Instance.listaTermina.Remove(stariTermin);
                        Termini.Instance.listaTermina.Add(noviTermin);
                        Termini.Instance.Serijalizacija();
                        Termini.Instance.Deserijalizacija();
                        break;
                    }
                }

                foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata.ToList())
                {
                    if (pacijent.jmbg == noviTermin.pacijentJMBG)
                    {
                        foreach (Termin stariTermin in pacijent.zakazaniTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                pacijent.zakazaniTermini.Remove(stariTermin);
                                pacijent.zakazaniTermini.Add(noviTermin);
                                pomeranje.terminiPacijenta.listaZakazanihTermina.ItemsSource = pacijent.zakazaniTermini;
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }

                foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
                {
                    if (lekar.jmbg == noviTermin.lekarJMBG)
                    {
                        foreach (Termin stariTermin in lekar.zauzetiTermini)
                        {
                            if (stariTermin.vreme == staroVreme)
                            {
                                lekar.zauzetiTermini.Remove(stariTermin);
                                lekar.zauzetiTermini.Add(noviTermin);
                                Lekari.Instance.Serijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                break;
                            }
                        }
                    }
                }
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