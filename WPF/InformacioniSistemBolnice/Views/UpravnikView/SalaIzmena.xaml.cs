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
using Kontroler;
using InformacioniSistemBolnice.DTO;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaIzmena.xaml
    /// </summary>
    public partial class SalaIzmena : Page
    {
        private Prostorija IzabranaProstorija { get; set; }
        public SalaIzmena(Prostorija izabranaProstorija)
        {
            InitializeComponent();
            IzabranaProstorija = izabranaProstorija;
            tbSprat.Text = izabranaProstorija.Sprat.ToString();
            tbId.Text = izabranaProstorija.Id;
            cbTipProstorije.SelectedItem = izabranaProstorija.Tip;
            cbTipProstorije.IsHitTestVisible = false;
            if (izabranaProstorija.JeZauzeta) rbZauzet.IsChecked = true;
            else rbSlobodna.IsChecked = true;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbId.Text) && !string.IsNullOrEmpty(tbSprat.Text)
                && tbSprat.Text.All(char.IsDigit))
            {
                ProstorijaDto dto = new();
                if ((bool)rbZauzet.IsChecked) dto.JeZauzeta = true;
                if ((bool)rbSlobodna.IsChecked) dto.JeZauzeta = false;
                dto = new(Int32.Parse(tbSprat.Text), (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipProstorije.Text),
                        tbId.Text, dto.JeZauzeta, IzabranaProstorija.Inventar);
                ProstorijaKontroler.Instance.IzmenaProstorije(dto);
                this.NavigationService.Navigate(new Sale());
            }
        }
    }
}
