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
            ObservableCollection<string> lista = new ObservableCollection<string>();
            cbTipOpreme.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
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
                ProstorijaKontroler.Instance.UklanjanjeProstorije(new ProstorijaDto() { Id = izabranaProstorija.Id });
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

        private void Filtriraj(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Prostorija> novaListaProstorija = new ObservableCollection<Prostorija>();
            if (tbFilter.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbFilter.Text))
            {
                if ((bool)rbDinamicka.IsChecked)
                {
                    novaListaProstorija = FormirajListuProstorijaPoKoliciniDinamickeOpreme();
                }
                else
                {
                    novaListaProstorija = FormirajListuProstorijaPoKoliciniStatickeOpreme();
                }
                listaProstorija = novaListaProstorija;
                dgListaProstorija.ItemsSource = listaProstorija;
            }
        }


        private void Resetuj(object sender, RoutedEventArgs e)
        {
            listaProstorija = ProstorijaRepo.Instance.Prostorije;
            dgListaProstorija.ItemsSource = listaProstorija;
        }

        private void rbDinamicka_Checked(object sender, RoutedEventArgs e)
        {
            cbTipOpreme.ItemsSource = null;
            cbTipOpreme.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
            cbTipOpreme.SelectedIndex = 0;
        }

        private void rbStaticka_Checked(object sender, RoutedEventArgs e)
        {
            cbTipOpreme.ItemsSource = null;
            cbTipOpreme.ItemsSource = Enum.GetValues(typeof(TipStatickeOpreme));
            cbTipOpreme.SelectedIndex = 0;
        }

        private ObservableCollection<Prostorija> FormirajListuProstorijaPoKoliciniDinamickeOpreme()
        {
            ObservableCollection<Prostorija> listaProstorija = new ObservableCollection<Prostorija>();
            int kolicina = Int32.Parse(tbFilter.Text);
            TipDinamickeOpreme tipDinamickeOpreme = (TipDinamickeOpreme)cbTipOpreme.SelectedItem;
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije.ToList())
            {
                foreach (DinamickaOprema dinamickaOprema in prostorija.Inventar.DinamickaOprema.ToList())
                {
                    if (dinamickaOprema.Tip.Equals(tipDinamickeOpreme) && dinamickaOprema.Kolicina >= kolicina)
                    {
                        listaProstorija.Add(prostorija);
                    }
                }
            }
            return listaProstorija;
        }

        private ObservableCollection<Prostorija> FormirajListuProstorijaPoKoliciniStatickeOpreme()
        {
            ObservableCollection<Prostorija> listaProstorija = new ObservableCollection<Prostorija>();
            int kolicina = Int32.Parse(tbFilter.Text);
            TipStatickeOpreme tipStatickeOpreme = (TipStatickeOpreme)cbTipOpreme.SelectedItem;
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije.ToList())
            {
                foreach (StatickaOprema statickaOprema in prostorija.Inventar.StatickaOprema.ToList())
                {
                    if (statickaOprema.Tip.Equals(tipStatickeOpreme) && statickaOprema.Kolicina >= kolicina)
                    {
                        listaProstorija.Add(prostorija);
                    }
                }
            }
            return listaProstorija;
        }

        private void SpojiProstorije(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SalaSpajanje());
        }

        private void RazdvojiProstorije(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                this.NavigationService.Navigate(new SalaRazdvajanje(izabranaProstorija));
            }
        }

        private void RenoviranjeProstorije(object sender, RoutedEventArgs e)
        {
            if (dgListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)dgListaProstorija.SelectedValue;
                this.NavigationService.Navigate(new SalaRenoviranje(izabranaProstorija));
            }
        }
    }
}
