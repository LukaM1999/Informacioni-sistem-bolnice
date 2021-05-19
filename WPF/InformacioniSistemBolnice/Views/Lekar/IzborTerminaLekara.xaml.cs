﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using Kontroler;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class IzborTerminaLekara : Window
    {
        public ObservableCollection<Termin> slobodniTermini = new();
        public UputDto uput;
        public IzborTerminaLekara(UputDto noviUput)
        {
            InitializeComponent();
            uput = noviUput;
            /*TimeSpan intervalDana = uput.Kraj - uput.Pocetak;
            DateTime slobodanTermin = uput.Pocetak.AddHours(7);*/
            GeneriseSlobodneTermine(uput.Kraj - uput.Pocetak, uput.Pocetak.AddHours(7));
            IzbacujeZauzeteTermineLekara();
            IzbacujeZauzeteTerminePacijenta();
            //slobodniTermini.Clear();
            NemaSlobodnihTermina();
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void NemaSlobodnihTermina()
        {
            if (slobodniTermini.Count == 0)
            {
                DateTime slobodanTermin = (uput.Pocetak.Subtract(new TimeSpan(48, 0, 0)));
                slobodanTermin = slobodanTermin.AddHours(7);
                GeneriseNovuListuSlobodnihTermina(slobodanTermin);
                slobodanTermin = uput.Kraj.AddHours(7);
                GeneriseNovuListuSlobodnihTermina(slobodanTermin);
                IzbacujeZauzeteTermineLekara();
                IzbacujeZauzeteTerminePacijenta();
            }
        }

        private void GeneriseNovuListuSlobodnihTermina(DateTime slobodanTermin)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    slobodanTermin = PopunjavaNovuListuSlobodnihTermina(slobodanTermin);
                }

                slobodanTermin = slobodanTermin.AddHours(10.5);
            }
        }

        private DateTime PopunjavaNovuListuSlobodnihTermina(DateTime slobodanTermin)
        {
            foreach (Termin zauzetTermin in uput.Lekar.zauzetiTermini)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                    uput.Tip, StatusTermina.slobodan,
                    uput.Pacijent.jmbg, uput.Lekar.jmbg, null, uput.Hitan));
                foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija)
                {
                    bool vecZakazan = false;
                    if (uput.Tip == TipTermina.pregled)
                    {
                        if (prostorija.Tip != TipProstorije.prostorijaZaPreglede) continue;
                        foreach (Termin termin in prostorija.TerminiProstorije)
                        {
                            if (slobodanTermin != termin.vreme) continue;
                            vecZakazan = true;
                            break;
                        }
                        if (vecZakazan) continue;
                        if (prostorija.Renoviranje != null)
                        {
                            if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                        }

                        slobodniTermini.Last().idProstorije = prostorija.Id;
                    }
                    else
                    {
                        if (prostorija.Tip != TipProstorije.operacionaSala) continue;
                        foreach (Termin termin in prostorija.TerminiProstorije)
                        {
                            if (slobodanTermin != termin.vreme) continue;
                            vecZakazan = true;
                            break;
                        }

                        if (vecZakazan) continue;
                        if (prostorija.Renoviranje != null)
                        {
                            if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                        }

                        slobodniTermini.Last().idProstorije = prostorija.Id;
                    }
                }
                if (slobodniTermini.Last().idProstorije == null)
                    slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            return slobodanTermin;
        }

        private void IzbacujeZauzeteTermineLekara()
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in uput.Lekar.zauzetiTermini)
                {
                    if (predlozenTermin.vreme == postojeciTermin.vreme)
                    {
                        slobodniTermini.Remove(predlozenTermin);
                        break;
                    }
                }
            }
        }
        private void IzbacujeZauzeteTerminePacijenta()
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in uput.Pacijent.zakazaniTermini)
                {
                    if (predlozenTermin.vreme == postojeciTermin.vreme)
                    {
                        slobodniTermini.Remove(predlozenTermin);
                        break;
                    }
                }
            }
        }

        private void GeneriseSlobodneTermine(TimeSpan intervalDana, DateTime slobodanTermin)
        {
            for (int i = 0; i < intervalDana.Days; i++)
            {
                slobodanTermin = PopunjavaListuSlobodnihTermina(slobodanTermin);
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }
        }

        private DateTime PopunjavaListuSlobodnihTermina(DateTime slobodanTermin)
        {
            for (int j = 0; j < 27; j++)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                    uput.Tip, StatusTermina.slobodan,
                    uput.Pacijent.jmbg, uput.Lekar.jmbg, null, uput.Hitan));

                foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija)
                {
                    bool vecZakazan = false;
                    if (uput.Tip == TipTermina.pregled)
                    {
                        if (prostorija.Tip != TipProstorije.prostorijaZaPreglede) continue;
                        foreach (Termin termin in prostorija.TerminiProstorije)
                        {
                            if (slobodanTermin != termin.vreme) continue;
                            vecZakazan = true;
                            break;
                        }
                        if (vecZakazan) continue;
                        if (prostorija.Renoviranje != null)
                        {
                            if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                        }

                        slobodniTermini.Last().idProstorije = prostorija.Id;
                    }
                    else
                    {
                        if (prostorija.Tip != TipProstorije.operacionaSala) continue;
                        foreach (Termin termin in prostorija.TerminiProstorije)
                        {
                            if (slobodanTermin != termin.vreme) continue;
                            vecZakazan = true;
                            break;
                        }

                        if (vecZakazan) continue;
                        if (prostorija.Renoviranje != null)
                        {
                            if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                        }

                        slobodniTermini.Last().idProstorije = prostorija.Id;
                    }
                }

                if (slobodniTermini.Last().idProstorije == null)
                    slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            return slobodanTermin;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ponudjeniTermini.SelectedIndex == -1) return;
            LekarKontroler.Instance.Zakazivanje((Termin)ponudjeniTermini.SelectedItem);
            Close();
        }
    }
}
