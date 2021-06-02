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
    /// Interaction logic for ZahtevInfo.xaml
    /// </summary>
    public partial class ZahtevInfo : Page
    {
        private Zahtev IzabraniZahtev { get; set; }

        public ZahtevInfo(Zahtev izabraniZahtev)
        {
            InitializeComponent();
            IzabraniZahtev = izabraniZahtev;
            tbKomentar.Text = IzabraniZahtev.Komentar;
            tbPotpis.Text = IzabraniZahtev.Potpis;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void Lekovi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Lekovi());
        }
    }
}
