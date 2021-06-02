using Model;
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

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for LekInfo.xaml
    /// </summary>
    public partial class LekInfo : Page
    {
        public LekInfo(Lek izabraniLek)
        {
            InitializeComponent();
            tbNaziv.Text = izabraniLek.Naziv;
            tbProizvodjac.Text = izabraniLek.Proizvodjac;
            tbZamena.Text = izabraniLek.Zamena;
            tbSastojci.Text = izabraniLek.Sastojci;
            dgListaAlergena.ItemsSource = izabraniLek.Alergen;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Lekovi());
        }
    }
}
