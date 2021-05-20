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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MagacinDodajProzor.xaml
    /// </summary>
    public partial class MagacinDodajProzor : Window
    {
        private DataGrid lista;
        public MagacinDodajProzor()
        {
            InitializeComponent();
        }

        public MagacinDodajProzor(DataGrid lv)
        {
            InitializeComponent();
            lista = lv;
        }

        private void dugmePotvrdi_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.KreiranjeStatickeOpreme(this);
            lista.ItemsSource = Repozitorijum.StatickaOpremaRepo.Instance.listaOpreme;
            this.Close();
        }
    }
}
