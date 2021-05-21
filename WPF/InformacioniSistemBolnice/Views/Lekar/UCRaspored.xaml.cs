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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using Kontroler;

namespace InformacioniSistemBolnice.Views.Lekar
{
    /// <summary>
    /// Interaction logic for UCRaspored.xaml
    /// </summary>
    public partial class UCRaspored : UserControl
    {
        public Model.Lekar lekar;
        public GlavniProzorLekara glavniProzor;
        public UCRaspored(GlavniProzorLekara glavniProzorLekara)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            Termini.Instance.Deserijalizacija();
            lekar = glavniProzorLekara.ulogovanLekar;
            lekar.zauzetiTermini.Clear();
            PronadjiTermineLekara();
            ZakazaniTermini.ItemsSource = lekar.zauzetiTermini;
            glavniProzor = glavniProzorLekara;
        }

        private void PronadjiTermineLekara()
        {
            foreach (Termin termin in Termini.Instance.listaTermina.ToList())
            {
                if (lekar.jmbg.Equals(termin.lekarJMBG))
                {
                    lekar.zauzetiTermini.Add(termin);
                }
            }
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new UCZakazivanje(glavniProzor);
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex > -1)
            {
                LekarKontroler.Instance.Otkazivanje((Termin)ZakazaniTermini.SelectedItem);
            }
        }

        private void Pomeri_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex > -1)
            {
                PomeranjeTerminaLekaraProzor pomeranje = new PomeranjeTerminaLekaraProzor((Termin)ZakazaniTermini.SelectedItem);
                pomeranje.Show();
            }
        }

        private void Pregled_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex >= 0)
            {
                IzmenaZdravstvenogKartonaLekar izmena = new IzmenaZdravstvenogKartonaLekar(
                    (Termin)ZakazaniTermini.SelectedItem, glavniProzor);
                izmena.Show();
            }
        }
    }
}