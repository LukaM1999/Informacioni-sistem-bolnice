﻿using Model;
using System;
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
using Kontroler;
using InformacioniSistemBolnice.DTO;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for OpremaIzmeni.xaml
    /// </summary>
    public partial class OpremaIzmeniDinamicku : Page
    {
        public OpremaIzmeniDinamicku(DinamickaOprema oprema)
        {
            InitializeComponent();
            cbTip.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
            cbTip.SelectedItem = oprema.Tip;
            cbTip.IsHitTestVisible = false;
            tbKolicina.Text = oprema.Kolicina.ToString();
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text))
            {
                OpremaKontroler.Instance.IzmenaDinamickeOpreme(new(Int32.Parse(tbKolicina.Text),
                    (TipDinamickeOpreme)Enum.Parse(typeof(TipDinamickeOpreme), cbTip.Text)));
                this.NavigationService.Navigate(new Magacin());
            }
        }
    }
}
