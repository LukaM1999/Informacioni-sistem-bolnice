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
    public partial class PomeranjeTerminaLekaraProzor : Window
    {
        public Termin zakazanTermin;
        public List<string> listaDatuma = new List<string>();
        public List<string> prostorijeID = new List<string>();
        public Termin terminZaPomeranje;
        public Termin noviTermin;
        public PomeranjeTerminaLekaraProzor(Termin zakazanTermin)

        {
            InitializeComponent();
            terminZaPomeranje = zakazanTermin;
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }
            listaSati.ItemsSource = listaDatuma;

            trajanjeTerminaUnos.Text = zakazanTermin.Trajanje.ToString();
            tipTerminaUnos.Text = zakazanTermin.Tip.ToString();

            ProstorijaRepo.Instance.Deserijalizacija();
            listaSati.ItemsSource = listaDatuma;
            
            foreach (Prostorija p in ProstorijaRepo.Instance.Prostorije)
            {
                prostorijeID.Add(p.Id);
            }
            sala.ItemsSource = prostorijeID;

            if (zakazanTermin.ProstorijaId != null)
            {
                sala.Text = zakazanTermin.ProstorijaId;
            }

            datumTermina.Text = zakazanTermin.Vreme.ToString("MM/dd/yyyy");

            string sat = zakazanTermin.Vreme.ToString("HH:mm");
            for (int i = 0; i < listaDatuma.Count; i++)
            {
                if (sat.Equals(listaDatuma[i]))
                {
                    listaSati.SelectedIndex = i;
                }
            }

            noviTermin = zakazanTermin;
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.Pomeranje(terminZaPomeranje, noviTermin);
        }
    }
}