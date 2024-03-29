﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UCListaRecepata : MetroContentControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        public Pacijent pacijent;

        public UCListaRecepata(Pacijent pacijent, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzorLekara = glavni;
            this.pacijent = pacijent;
            listaRecepata.ItemsSource = pacijent.zdravstveniKarton.recepti;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Karton karton = new Karton() {DataContext = new KartonViewModel(glavniProzorLekara, pacijent.Jmbg)};
            glavniProzorLekara.contentControl.Content = karton;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (listaRecepata.SelectedIndex > -1)
            {
                UCReceptInfo recept = new UCReceptInfo(glavniProzorLekara, (Recept)listaRecepata.SelectedItem, this);
                glavniProzorLekara.contentControl.Content = recept;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Recept> Filter = new ObservableCollection<Recept>();
            foreach (var pacijent in pacijent.zdravstveniKarton.recepti)
            {
                if (Nadji(searchBox.Text.ToLower(), pacijent))
                {
                    Filter.Add(pacijent);
                }
            }

            listaRecepata.ItemsSource = Filter;
        }

        private bool Nadji(string text, Recept p)
        {
            foreach (Terapija t in p.terapije)
            {
                return Validno(text, p, t);
            }

            return false;
        }

        private bool Validno(string text, Recept p, Terapija t)
        {
            return p.ReceptId.ToLower().Contains(text) || t.Lek.Naziv.ToLower().Contains(text);
        }

    }
}
