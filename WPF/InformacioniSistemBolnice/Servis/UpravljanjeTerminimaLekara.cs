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
    public class UpravljanjeTerminimaLekara
    {
        private static readonly Lazy<UpravljanjeTerminimaLekara>
           lazy =
           new Lazy<UpravljanjeTerminimaLekara>
               (() => new UpravljanjeTerminimaLekara());

        public static UpravljanjeTerminimaLekara Instance { get { return lazy.Value; } }

        public void Zakazivanje(ZakazivanjeTerminaLekaraProzor zakazivanje, string jmbgLekar)
        {
            if (zakazivanje.listaSati.SelectedIndex >= 0 && zakazivanje.datumTermina.SelectedDate != null)
            {
                
                foreach (Lekar lekar in Lekari.Instance.listaLekara)
                {
                    if (lekar.jmbg == jmbgLekar)
                    {

                        foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
                        {
                            if (pacijent.jmbg.Equals((string)zakazivanje.pacijenti.SelectedItem))
                            {

                                //parsiranje izabranog datuma i vremena
                                DateTime datumTermina = (DateTime)zakazivanje.datumTermina.SelectedDate;
                                string datumVrednost = (string)zakazivanje.listaSati.SelectedValue;
                                string[] satiMinuti = datumVrednost.Split(":");
                                double sat = double.Parse(satiMinuti[0]);
                                if (satiMinuti[1].Equals("30"))
                                {
                                    sat += 0.5;
                                }

                                datumTermina = datumTermina.AddHours(sat);


                                if (pacijent.zakazaniTermini != null)
                                {
                                    foreach (Termin t in lekar.zauzetiTermini)
                                    {
                                        if (t.vreme == datumTermina)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Zauzet lekar");

                                            return;
                                            //Vec postoji zakazan termin
                                        }
                                    }
                                }
                                if (pacijent.zakazaniTermini != null)
                                {
                                    foreach (Termin t in pacijent.zakazaniTermini)
                                    {
                                        if (t.vreme == datumTermina)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Zauzet pacijent");

                                            return;
                                            //Vec postoji zakazan termin
                                        }
                                    }
                                }

                                Termin zakazanTermin = new Termin(datumTermina, double.Parse(zakazivanje.trajanjeTerminaUnos.Text),
                                    (TipTermina)Enum.Parse(typeof(TipTermina), zakazivanje.tipTerminaUnos.Text), StatusTermina.zakazan);

                                zakazanTermin.lekarJMBG = lekar.jmbg;
                                zakazanTermin.pacijentJMBG = pacijent.jmbg;
                                zakazanTermin.Hitan = (bool)zakazivanje.hitan.IsChecked;

                                foreach (Prostorija p in Prostorije.Instance.listaProstorija)
                                {
                                    if (p.id.Equals((string)zakazivanje.sala.SelectedItem))
                                    {
                                        zakazanTermin.idProstorije = p.id;
                                        break;
                                    }
                                }

                                lekar.zauzetiTermini.Add(zakazanTermin);
                                pacijent.zakazaniTermini.Add(zakazanTermin);
                                Termini.Instance.listaTermina.Add(zakazanTermin);
                                Termini.Instance.Serijalizacija();
                                Lekari.Instance.Serijalizacija();
                                Pacijenti.Instance.Serijalizacija();
                                Pacijenti.Instance.Deserijalizacija();
                                Lekari.Instance.Deserijalizacija();
                                Termini.Instance.Deserijalizacija();
                                zakazivanje.listaZakazanihTermina.ItemsSource = lekar.zauzetiTermini;

                                zakazivanje.Close();
                            }
                        }
                    }
                }
            }
        }

        public void Otkazivanje(DataGrid listaZakazanihTerminaLekara)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                Termin t = (Termin)listaZakazanihTerminaLekara.SelectedItem;
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
                foreach (Lekar lekar in Lekari.Instance.listaLekara)
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
                                listaZakazanihTerminaLekara.ItemsSource = lekar.zauzetiTermini;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void Pomeranje(PomeranjeTerminaLekaraProzor pomeranje)
        {
            if (pomeranje.listaSati.SelectedIndex >= 0 && pomeranje.datumTermina.Text != null && pomeranje.tipTerminaUnos.SelectedIndex >= 0)
            {
                foreach (Lekar lekar in Lekari.Instance.listaLekara)
                {
                    if (lekar.jmbg == pomeranje.zakazanTermin.lekarJMBG)
                    {
                        foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
                        {
                            if (pacijent.jmbg == pomeranje.zakazanTermin.pacijentJMBG)
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

                                DateTime staro = pomeranje.zakazanTermin.vreme;
                                pomeranje.zakazanTermin.vreme = datumTermina;
                                pomeranje.zakazanTermin.trajanje = double.Parse(pomeranje.trajanjeTerminaUnos.Text);
                                pomeranje.zakazanTermin.tipTermina = (TipTermina)Enum.Parse(typeof(TipTermina), pomeranje.tipTerminaUnos.Text);
                                foreach (Prostorija p in Prostorije.Instance.listaProstorija)
                                {
                                    if (p.id.Equals((string)pomeranje.sala.SelectedItem))
                                    {
                                        pomeranje.zakazanTermin.idProstorije = p.id;
                                        break;
                                    }
                                }

                                Termini.Instance.Serijalizacija();
                                Termini.Instance.Deserijalizacija();

                                foreach (Termin t in lekar.zauzetiTermini.ToList())
                                {
                                    if (t.vreme == staro)
                                    {
                                        lekar.zauzetiTermini.Remove(t);
                                        lekar.zauzetiTermini.Add(pomeranje.zakazanTermin);
                                        Lekari.Instance.Serijalizacija();
                                        Lekari.Instance.Deserijalizacija();
                                    }
                                }                             

                                foreach (Termin t in pacijent.zakazaniTermini.ToList())
                                {
                                    if (t.vreme == staro)
                                    {
                                        pacijent.zakazaniTermini.Remove(t);
                                        pacijent.zakazaniTermini.Add(pomeranje.zakazanTermin);
                                        Pacijenti.Instance.Serijalizacija();
                                        Pacijenti.Instance.Deserijalizacija();
                                    }
                                }
                                Termini.Instance.Serijalizacija();
                                Termini.Instance.Deserijalizacija();
                                
                                pomeranje.zakazaniTermini.ItemsSource = lekar.zauzetiTermini;
                                pomeranje.Close();
                            }
                        }
                    }
                }

            }
        }

        public void Uvid(DataGrid listaZakazanihTerminaLekara)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                TerminInfoProzor terminInfo = new TerminInfoProzor(listaZakazanihTerminaLekara);
                terminInfo.Show();
            }
        }

        public void IzdavanjeUputa(UputDto uput, Termin izabranTermin)
        {
            if (JeZauzetTermin(izabranTermin)) return;
            PretragaLekara(izabranTermin);
        }

        private static void PretragaLekara(Termin izabranTermin)
        {
            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                if (lekar.jmbg == izabranTermin.lekarJMBG)
                {
                    PretragaPacijenta(izabranTermin, lekar);
                }
            }
        }

        private static void PretragaPacijenta(Termin izabranTermin, Lekar lekar)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.jmbg == izabranTermin.pacijentJMBG)
                {
                    ZakazujeKodSpecijaliste(izabranTermin, lekar, pacijent);
                    break;
                }
            }
        }

        private static void ZakazujeKodSpecijaliste(Termin izabranTermin, Lekar lekar, Pacijent pacijent)
        {
            izabranTermin.status = StatusTermina.zakazan;
            pacijent.zakazaniTermini.Add(izabranTermin);
            lekar.zauzetiTermini.Add(izabranTermin);
            Termini.Instance.listaTermina.Add(izabranTermin);
            Lekari.Instance.Serijalizacija();
            Pacijenti.Instance.Serijalizacija();
            Termini.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            Termini.Instance.Deserijalizacija();
        }

        private static bool JeZauzetTermin(Termin izabranTermin)
        {
            foreach (Termin vecZakazan in Termini.Instance.listaTermina)
            {
                if (ZauzetiLekarIPacijent(izabranTermin, vecZakazan)) return true;
            }
            return false;
        }

        private static bool ZauzetiLekarIPacijent(Termin izabranTermin, Termin vecZakazan)
        {
            return vecZakazan.vreme.Equals(izabranTermin.vreme) 
                   && vecZakazan.lekarJMBG.Equals(izabranTermin.lekarJMBG) 
                   && vecZakazan.pacijentJMBG.Equals(izabranTermin.pacijentJMBG);
        }
    }
}