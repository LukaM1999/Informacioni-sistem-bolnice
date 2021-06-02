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
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCReceptInfo.xaml
    /// </summary>
    public partial class UCReceptInfo : MetroContentControl
    {
        public UCListaRecepata listaRecepata;
        public UCReceptInfo(UCListaRecepata recepti)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            Recept r = (Recept)recepti.listaRecepata.SelectedItem;
            listaTerapija.ItemsSource = r.terapije;
            ID.Content = r.ReceptId;
            listaRecepata = recepti;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listaRecepata.glavniProzorLekara.contentControl.Content = listaRecepata;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (listaTerapija.SelectedIndex > -1)
            {
                Terapija terapija = (Terapija)listaTerapija.SelectedItem;
                IspuniPodatkeOLeku(terapija);
            }
        }

        private void IspuniPodatkeOLeku(Terapija terapija)
        {
            if (terapija.Lek != null)
            {
                naziv.Content = terapija.Lek.Naziv;
                proizvodjac.Content = terapija.Lek.Proizvodjac;
                sastojci.Content = terapija.Lek.Sastojci;
                zamena.Content = terapija.Lek.Zamena;
            }
        }
    }
}
