using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaRaspodelaDinamicke.xaml
    /// </summary>
    public partial class SalaRaspodelaDinamicke : Page
    {
        private ObservableCollection<Prostorija> ListaProstorija { get; set; }
        private DinamickaOprema IzabranaOprema { get; set; }
        public Prostorija IzabranaProstorija { get; set; }

        public SalaRaspodelaDinamicke(DinamickaOprema izabranaOprema, Prostorija izabranaProstorija)
        {
            InitializeComponent();
            labOprema.Content = izabranaOprema.Tip.ToString();
            IzabranaOprema = izabranaOprema;
            IzabranaProstorija = izabranaProstorija;
            ListaProstorija = ProstorijaRepo.Instance.Prostorije;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked && tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text))
            {
                RaspodelaDinamickeOpremeDto dtoRapsodelaUMagacin = new(IzabranaProstorija.Id, null,
                    IzabranaOprema, Int32.Parse(tbKolicina.Text));
                OpremaKontroler.Instance.RasporedjivanjeDinamickeOpreme(dtoRapsodelaUMagacin);
                this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
            }
            if ((bool)rbProstorije.IsChecked && tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text)
                && cbListaProstorija.SelectedValue != null && (Prostorija)cbListaProstorija.SelectedItem != IzabranaProstorija)
            {
                Prostorija uProstoriju = (Prostorija)cbListaProstorija.SelectedItem;
                RaspodelaDinamickeOpremeDto dtoRaspodelaUDruguProstoriju = new(IzabranaProstorija.Id, uProstoriju.Id,
                        IzabranaOprema, Int32.Parse(tbKolicina.Text));
                OpremaKontroler.Instance.RasporedjivanjeDinamickeOpreme(dtoRaspodelaUDruguProstoriju);
                this.NavigationService.Navigate(new SalaRaspodela(IzabranaProstorija));
            }
        }

        private void rbMagacin_Checked(object sender, RoutedEventArgs e)
        {
            cbListaProstorija.ItemsSource = null;
        }

        private void rbProstorije_Checked(object sender, RoutedEventArgs e)
        {
            cbListaProstorija.ItemsSource = ListaProstorija;
            cbListaProstorija.SelectedIndex = 0;
        }
    }
}
