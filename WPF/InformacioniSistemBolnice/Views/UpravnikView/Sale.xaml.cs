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
using Model;
using InformacioniSistemBolnice.DTO;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : Page
    {
        private ObservableCollection<Prostorija> listaProstorija { get; set; }

        public Sale()
        {
            InitializeComponent();
            ProstorijaRepo.Instance.Deserijalizacija();
            dgListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            listaProstorija = ProstorijaRepo.Instance.Prostorije;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void RaspodelaOpreme(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                this.NavigationService.Navigate(new SalaRaspodela(izabranaProstorija));
            }
        }

        private void DodajProstoriju(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalaDodaj());
        }

        private void ObrisiProstoriju(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                UpravnikKontroler.Instance.UklanjanjeProstorije(new ProstorijaDto() { Id = izabranaProstorija.Id });
                listaProstorija = ProstorijaRepo.Instance.Prostorije;
                dgListaProstorija.ItemsSource = listaProstorija;
            }
        }

        private void IzmeniProstoriju(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                this.NavigationService.Navigate(new SalaIzmena(izabranaProstorija));
            }
        }

        private void InfoProstorije(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                this.NavigationService.Navigate(new SalaInfo(izabranaProstorija));
            }
        }
    }
}
