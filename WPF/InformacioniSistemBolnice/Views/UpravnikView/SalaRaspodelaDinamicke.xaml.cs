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
using Model;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaRaspodelaDinamicke.xaml
    /// </summary>
    public partial class SalaRaspodelaDinamicke : Page
    {
        private DinamickaOprema IzabranaOprema { get; set; }
        private Prostorija IzabranaProstorija { get; set; }

        public SalaRaspodelaDinamicke(DinamickaOprema izabranaOprema, Prostorija izabranaProstorija)
        {
            InitializeComponent();
            IzabranaOprema = izabranaOprema;
            IzabranaProstorija = izabranaProstorija;
            //labOprema.Content = izabranaOprema.Tip;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked)
            {
                this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
            }
            if ((bool)rb.IsChecked)
            {
                this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
            }
        }
    }
}
