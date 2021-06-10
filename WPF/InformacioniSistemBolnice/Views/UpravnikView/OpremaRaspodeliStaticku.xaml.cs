using Kontroler;
using Model;
using Repozitorijum;
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
    /// Interaction logic for OpremaRaspodeliStaticku.xaml
    /// </summary>
    public partial class OpremaRaspodeliStaticku : Page
    {
        private StatickaOprema IzabranaOprema { get; set; }
        public OpremaRaspodeliStaticku(StatickaOprema oprema)
        {
            InitializeComponent();
            IzabranaOprema = oprema;
            dpDatum.DisplayDateStart = DateTime.Today;
            cbListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbListaProstorija.SelectedIndex = 0;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (tbKolicina.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbKolicina.Text)
                && Int32.Parse(tbKolicina.Text) <= IzabranaOprema.Kolicina && dpDatum.SelectedDate != null)
            {
                Prostorija uProstoriju = (Prostorija)cbListaProstorija.SelectedItem;
                OpremaKontroler.Instance.RasporedjivanjeStatickeOpreme(new(null, uProstoriju.Id,
                    IzabranaOprema, Int32.Parse(tbKolicina.Text), (DateTime)dpDatum.SelectedDate));
                this.NavigationService.Navigate(new Magacin());
            }
        }
    }
}
