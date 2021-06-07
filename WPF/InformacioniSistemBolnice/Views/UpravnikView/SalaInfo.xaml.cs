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
    /// Interaction logic for SalaInfo.xaml
    /// </summary>
    public partial class SalaInfo : Page
    {
        public SalaInfo(Prostorija izabranaProstorija)
        {
            InitializeComponent();
            tbSprat.Text = izabranaProstorija.Sprat.ToString();
            tbId.Text = izabranaProstorija.Id;
            cbTipProstorije.SelectedItem = izabranaProstorija.Tip;
            if (izabranaProstorija.JeZauzeta) rbZauzet.IsChecked = true;
            else rbSlobodna.IsChecked = true;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }
    }
}
