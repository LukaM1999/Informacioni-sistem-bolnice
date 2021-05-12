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
    /// Interaction logic for KreirajVijestProzor.xaml
    /// </summary>
    public partial class KreirajVijestProzor : Window
    {
        public KreirajVijestProzor()
        {
            InitializeComponent();
        }

        private void kreirajVijest_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.KreiranjeVesti(this);
            this.Close();
        }
    }
}