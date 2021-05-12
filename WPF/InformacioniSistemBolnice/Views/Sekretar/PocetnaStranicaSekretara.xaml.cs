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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PocetnaStranicaSekretara.xaml
    /// </summary>
    public partial class PocetnaStranicaSekretara : Window
    {
        public PocetnaStranicaSekretara()
        {
            InitializeComponent();
            
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new UCMenuSekretara(this);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}