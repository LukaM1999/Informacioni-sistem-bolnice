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
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaRaspodela.xaml
    /// </summary>
    public partial class SalaRaspodela : Page
    {
        private Prostorija IzabranaProstorija { get; set; }

        public SalaRaspodela(Prostorija izabranaProstorija)
        {
            InitializeComponent();
            labela.Content = izabranaProstorija.Id;
            IzabranaProstorija = ProstorijaRepo.Instance.NadjiPoId(izabranaProstorija.Id);
            dgListaOpreme.ItemsSource = IzabranaProstorija.Inventar.DinamickaOprema;
            btnDinamicka.IsHitTestVisible = false;
            btnDinamicka.Foreground = Brushes.Black;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (dgListaOpreme.SelectedValue != null)
            {
                if (btnDinamicka.IsHitTestVisible)
                {
                    StatickaOprema izabranaOprema = (StatickaOprema)dgListaOpreme.SelectedValue;
                    this.NavigationService.Navigate(new SalaRaspodelaStaticke(izabranaOprema));
                }
                if (btnStaticka.IsHitTestVisible)
                {
                    DinamickaOprema izabranaOprema = (DinamickaOprema)dgListaOpreme.SelectedValue;
                    this.NavigationService.Navigate(new SalaRaspodelaDinamicke(izabranaOprema));
                }
            }
        }

        private void DinamickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = IzabranaProstorija.Inventar.DinamickaOprema;
            btnDinamicka.IsHitTestVisible = false;
            btnDinamicka.Foreground = Brushes.Black;
            btnStaticka.IsHitTestVisible = true;
            btnStaticka.Foreground = Brushes.White;
        }

        private void StatickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = IzabranaProstorija.Inventar.StatickaOprema;
            btnStaticka.IsHitTestVisible = false;
            btnStaticka.Foreground = Brushes.Black;
            btnDinamicka.IsHitTestVisible = true;
            btnDinamicka.Foreground = Brushes.White;
        }
    }
}
