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
using InformacioniSistemBolnice.ViewModels.UpavnikViewModel;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class Magacin : Page
    {
        private bool JeDinamicka { get; set; }
        public Magacin()
        {
            InitializeComponent();
            ProstorijaRepo.Instance.Deserijalizacija();
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            StatickaOpremaRepo.Instance.Deserijalizacija();
            dgListaOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
            btnDinamicka.IsHitTestVisible = false;
            btnDinamicka.Foreground = Brushes.Black;
            JeDinamicka = true;
        }

        private void VratiNaPrethodniProzor(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }

        private void DinamickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
            btnDinamicka.IsHitTestVisible = false;
            btnDinamicka.Foreground = Brushes.Black;
            btnStaticka.IsHitTestVisible = true;
            btnStaticka.Foreground = Brushes.White;
            JeDinamicka = true;
        }

        private void StatickaOprema(object sender, RoutedEventArgs e)
        {
            dgListaOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
            btnStaticka.IsHitTestVisible = false;
            btnStaticka.Foreground = Brushes.Black;
            btnDinamicka.IsHitTestVisible = true;
            btnDinamicka.Foreground = Brushes.White;
            JeDinamicka = false;
        }

        private void Raspodeli(object sender, RoutedEventArgs e)
        {
            if(dgListaOpreme.SelectedValue != null)
            {
                if (JeDinamicka)
                {
                    DinamickaOprema izabranaOprema = (DinamickaOprema)dgListaOpreme.SelectedValue;
                    OpremaRaspodeliDinamicku prozor = new();
                    OpremaRaspodeliDinamicku prozor2 = prozor;
                    prozor.DataContext = new OpremaRaspodeliDinamickuViewModel(prozor2, izabranaOprema);
                    this.NavigationService.Navigate(prozor);
                }
                else
                {
                    StatickaOprema izabranaOprema = (StatickaOprema)dgListaOpreme.SelectedValue;
                    this.NavigationService.Navigate(new OpremaRaspodeliStaticku(izabranaOprema));
                }
            }
        }

        private void DodajOpremu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OpremaDodaj(JeDinamicka));
        }

        private void BrisiOpremu(object sender, RoutedEventArgs e)
        {
            if (dgListaOpreme.SelectedValue != null)
            {
                if (JeDinamicka)
                {
                    DinamickaOprema oprema = (DinamickaOprema)dgListaOpreme.SelectedItem;
                    OpremaKontroler.Instance.BrisanjeDinamickeOpreme(new(oprema.Kolicina, oprema.Tip));
                    dgListaOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
                }
                else
                {
                    StatickaOprema oprema = (StatickaOprema)dgListaOpreme.SelectedItem;
                    OpremaKontroler.Instance.BrisanjeStatickeOpreme(new(oprema.Kolicina, oprema.Tip));
                    dgListaOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
                }
            }
        }

        private void IzmeniOpremu(object sender, RoutedEventArgs e)
        {
            if (dgListaOpreme.SelectedValue != null)
            {
                if (JeDinamicka)
                {
                    DinamickaOprema izabranaOprema = (DinamickaOprema)dgListaOpreme.SelectedValue;
                    this.NavigationService.Navigate(new OpremaIzmeniDinamicku(izabranaOprema));
                }
                else
                {
                    StatickaOprema izabranaOprema = (StatickaOprema)dgListaOpreme.SelectedValue;
                    this.NavigationService.Navigate(new OpremaIzmeniStaticku(izabranaOprema));
                }
            }
        }
    }
}
