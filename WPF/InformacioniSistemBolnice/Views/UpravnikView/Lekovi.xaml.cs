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
using Model;
using Repozitorijum;
using InformacioniSistemBolnice.Views.UpravnikView;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for Lekovi.xaml
    /// </summary>
    public partial class Lekovi : Page
    {
        public Lekovi()
        {
            InitializeComponent();
            LekRepo.Instance.Deserijalizacija();
            dgListaLekova.ItemsSource = LekRepo.Instance.Lekovi;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void InfoLeka(object sender, RoutedEventArgs e)
        {
            if (dgListaLekova.SelectedValue != null)
            {
                Lek izabraniLek = (Lek)dgListaLekova.SelectedValue;
                this.NavigationService.Navigate(new LekInfo(izabraniLek));
            }
        }

        private void IzmenaLeka(object sender, RoutedEventArgs e)
        {
            if (dgListaLekova.SelectedValue != null)
            {
                Lek izabraniLek = (Lek)dgListaLekova.SelectedValue;
                this.NavigationService.Navigate(new LekIzmena(izabraniLek));
            }
        }

        private void BrisanjeLeka(object sender, RoutedEventArgs e)
        {
            if (dgListaLekova.SelectedValue != null)
            {
                Lek izabraniLek = (Lek)dgListaLekova.SelectedValue;
                LekKontroler.Instance.BrisanjeLeka(new(izabraniLek.Naziv, izabraniLek.Proizvodjac, izabraniLek.Sastojci, izabraniLek.Zamena, izabraniLek.Alergen));
                dgListaLekova.ItemsSource = LekRepo.Instance.Lekovi;
            }
        }

        private void DodajLek(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LekDodaj());
        }
    }
}
