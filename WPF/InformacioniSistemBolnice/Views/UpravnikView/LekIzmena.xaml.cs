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
using Repozitorijum;
using System.Collections.ObjectModel;
using Kontroler;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for LekIzmena.xaml
    /// </summary>
    public partial class LekIzmena : Page
    {
        private Lek IzabraniLek { get; set; }
        private ObservableCollection<Alergen> ListaAlergenaLeka { get; set; }

        public LekIzmena(Lek izabraniLek)
        {
            InitializeComponent();
            AlergenRepo.Instance.Deserijalizacija();
            IzabraniLek = izabraniLek;
            cbAlergeni.ItemsSource = AlergenRepo.Instance.Alergeni;
            dgListaAlergena.ItemsSource = IzabraniLek.Alergen;
            ListaAlergenaLeka = izabraniLek.Alergen;
            labAlergenPoruka.Visibility = Visibility.Hidden;
            tbNaziv.Text = izabraniLek.Naziv;
            tbProizvodjac.Text = izabraniLek.Proizvodjac;
            tbZamena.Text = izabraniLek.Zamena;
            tbSastojci.Text = izabraniLek.Sastojci;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            LekKontroler.Instance.IzmenaLeka(new(tbNaziv.Text, tbProizvodjac.Text, tbSastojci.Text,
                tbZamena.Text, ListaAlergenaLeka));
            this.NavigationService.Navigate(new Lekovi());
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Lekovi());
        }

        private void ObrisiZahtev(object sender, RoutedEventArgs e)
        {
            if (dgListaAlergena.SelectedValue != null)
            {
                Alergen izabraniAlergen = (Alergen)dgListaAlergena.SelectedValue;
                ListaAlergenaLeka.Remove(izabraniAlergen);
                dgListaAlergena.ItemsSource = ListaAlergenaLeka;
            }
        }

        private void DodajAlergen(object sender, RoutedEventArgs e)
        {
            labAlergenPoruka.Visibility = Visibility.Hidden;
            if (cbAlergeni.SelectedValue != null)
            {
                Alergen izabraniAlergen = (Alergen)cbAlergeni.SelectedItem;
                if (JeAlergenVecUListiAlergenaLeka(izabraniAlergen))
                {
                    labAlergenPoruka.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    ListaAlergenaLeka.Add(izabraniAlergen);
                    dgListaAlergena.ItemsSource = ListaAlergenaLeka;
                }
            }
        }

        private bool JeAlergenVecUListiAlergenaLeka(Alergen izabraniAlergen)
        {
            foreach(Alergen alergen in ListaAlergenaLeka)
            {
                if (alergen.Naziv.Equals(izabraniAlergen.Naziv)) return true;
            }
            return false;
        }
    }
}
