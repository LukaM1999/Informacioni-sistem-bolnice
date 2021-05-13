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
           Lazy = new(() => new UpravljanjeTerminimaPacijenata());
        public static UpravljanjeTerminimaPacijenata Instance => Lazy.Value;

        public void Zakazivanje(IzborTermina izborTermina, string jmbgPacijenta)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata)
            {
                if (pacijent.jmbg != jmbgPacijenta) continue;
                foreach (Termin vecZakazan in pacijent.zakazaniTermini)
                {
                    if (vecZakazan == (Termin)izborTermina.ponudjeniTermini.SelectedItem)
                    {
                        return;
                    }
                }
                foreach (Lekar lekar in Lekari.Instance.listaLekara)
                {
                    if (lekar.jmbg != ((Termin) izborTermina.ponudjeniTermini.SelectedItem).lekarJMBG) continue;
                    foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                    {
                        if (prostorija.id != ((Termin) izborTermina.ponudjeniTermini.SelectedItem).idProstorije)
                            continue;
                        ((Termin)izborTermina.ponudjeniTermini.SelectedItem).status = StatusTermina.zakazan;
                        pacijent.zakazaniTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                        lekar.zauzetiTermini.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                        prostorija.termin.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                        Termini.Instance.listaTermina.Add((Termin)izborTermina.ponudjeniTermini.SelectedItem);
                        Lekari.Instance.Serijalizacija();
                        Pacijenti.Instance.Serijalizacija();
                        Termini.Instance.Serijalizacija();
                        Prostorije.Instance.Serijalizacija();
                        izborTermina.zakazivanjeTerminaPacijenta.terminiPacijentaProzor.listaZakazanihTermina.ItemsSource
                            = pacijent.zakazaniTermini;
                        izborTermina.zakazivanjeTerminaPacijenta.Close();
                        izborTermina.Close();
                        return;
                    }
                }
            }
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
            {
                if (pacijent.jmbg != terminZaOtkazivanje.pacijentJMBG) continue;
                foreach (Termin termin in pacijent.zakazaniTermini)
                {
                    if (termin.vreme != terminZaOtkazivanje.vreme) continue;
                    pacijent.zakazaniTermini.Remove(termin);
                    Pacijenti.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.jmbg != terminZaOtkazivanje.lekarJMBG) continue;
                foreach (Termin termin in lekar.zauzetiTermini.ToList())
                {
                    if (termin.vreme != terminZaOtkazivanje.vreme) continue;
                    lekar.zauzetiTermini.Remove(termin);
                    Lekari.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Termin termin in Termini.Instance.listaTermina.ToList())
            {
                if (termin.vreme != terminZaOtkazivanje.vreme) continue;
                Termini.Instance.listaTermina.Remove(termin);
                Termini.Instance.Serijalizacija();
                break;
            }

            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id != terminZaOtkazivanje.idProstorije) continue;
                prostorija.termin.Remove(terminZaOtkazivanje);
                Prostorije.Instance.Serijalizacija();
                break;
            }
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            noviTermin.status = StatusTermina.pomeren;
            foreach (Termin stariTermin in Termini.Instance.listaTermina.ToList())
            {
                if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                Termini.Instance.listaTermina.Remove(stariTermin);
                Termini.Instance.listaTermina.Add(noviTermin);
                Termini.Instance.Serijalizacija();
                break;
            }

            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata.ToList())
            {
                if (pacijent.jmbg != noviTermin.pacijentJMBG) continue;
                foreach (Termin stariTermin in pacijent.zakazaniTermini)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    pacijent.ObrisiTermin(stariTermin);
                    pacijent.DodajTermin(noviTermin);
                    Pacijenti.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Lekar lekar in Lekari.Instance.listaLekara.ToList())
            {
                if (lekar.jmbg != noviTermin.lekarJMBG) continue;
                foreach (Termin stariTermin in lekar.zauzetiTermini)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    lekar.zauzetiTermini.Remove(stariTermin);
                    lekar.zauzetiTermini.Add(noviTermin);
                    Lekari.Instance.Serijalizacija();
                    break;
                }
            }

            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
            {
                if (prostorija.id != noviTermin.idProstorije) continue;
                foreach (Termin stariTermin in prostorija.termin)
                {
                    if (stariTermin.vreme != terminZaPomeranje.vreme) continue;
                    prostorija.termin.Remove(stariTermin);
                    prostorija.termin.Add(noviTermin);
                    Prostorije.Instance.Serijalizacija();
                    break;
                }
            }
        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            new TerminInfoProzor(listaZakazanihTermina).Show();
        }

    }
}