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
using InformacioniSistemBolnice.Views.UpravnikView;
using Repozitorijum;
using Model;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima;
using Kontroler;
using InformacioniSistemBolnice.ViewModels.UpavnikViewModel;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class MeniProzor : Page
    {
        public MeniProzor()
        {
            InitializeComponent();
            ZahtevRepo.Instance.Deserijalizacija();
            dgListaZahteva.ItemsSource = ZahtevRepo.Instance.Zahtevi;
        }

        private void tgBtnOdjava_Checked(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LogovanjeProzor());
        }

        private void IzabraniLekovi(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Lekovi());
        }

        private void Feedback(object sender, RoutedEventArgs e)
        {
            FeedbackUpravnik feedback = new();
            FeedbackUpravnik feedback2 = feedback;
            feedback.DataContext = new FeedBackUpravnikViewModel(feedback2);
            NavigationService.Navigate(feedback);
        }

        private void IzabranaOprema(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Magacin());
        }

        private void IzabraneSale(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void ObrisiZahtev(object sender, RoutedEventArgs e)
        {
            Zahtev izabraniZahtev = (Zahtev)dgListaZahteva.SelectedValue;
            ZahtevRepo.Instance.BrisiPoKomentaru(izabraniZahtev.Komentar);
            ZahtevRepo.Instance.Serijalizacija();
            dgListaZahteva.ItemsSource = ZahtevRepo.Instance.Zahtevi;
        }

        private void InfoZahteva(object sender, RoutedEventArgs e)
        {
            Zahtev izabraniZahtev = (Zahtev)dgListaZahteva.SelectedValue;
            this.NavigationService.Navigate(new ZahtevInfo(izabraniZahtev));
            dgListaZahteva.ItemsSource = ZahtevRepo.Instance.Zahtevi;
        }

        private void NapraviIzvestaj(object sender, RoutedEventArgs e)
        {
            IzvestajKontroler.Instance.GenerisiUpravnikovIzvestaj();
        }

        private void Demo(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Demo());
        }
    }
}
