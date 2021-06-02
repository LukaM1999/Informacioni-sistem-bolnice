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
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : Page
    {
        public Sale()
        {
            InitializeComponent();
            ProstorijaRepo.Instance.Deserijalizacija();
            dgListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void RaspodelaOpreme(object sender, RoutedEventArgs e)
        {

        }

        private void DodajProstoriju(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalaDodaj());
        }

        private void ObrisiProstoriju(object sender, RoutedEventArgs e)
        {


        }

        private void IzmeniProstoriju(object sender, RoutedEventArgs e)
        {

        }

        private void InfoProstorije(object sender, RoutedEventArgs e)
        {

        }
    }
}
