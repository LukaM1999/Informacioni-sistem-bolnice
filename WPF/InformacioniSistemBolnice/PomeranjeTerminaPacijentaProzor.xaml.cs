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
using Model;
using Repozitorijum;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeTerminaPacijentaProzor : Window
    {
        public Termin zakazanTermin;
        public List<string> listaDatuma = new List<string>();
        public DataGrid zakazaniTermini;
        public PomeranjeTerminaPacijentaProzor(DataGrid zakazaniTermini)

        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }
            listaSati.ItemsSource = listaDatuma;
            this.zakazaniTermini = zakazaniTermini;
            zakazanTermin = (Termin)zakazaniTermini.SelectedItem;
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Pomeranje(this);
        }
    }
}
