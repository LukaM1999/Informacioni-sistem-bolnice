using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Kontroler;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;
using InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima;
using InformacioniSistemBolnice.Utilities;

namespace InformacioniSistemBolnice
{
    public partial class Lekari : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public Lekari(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            LekarRepo.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = LekarRepo.Instance.Lekari;
        }

        private void RegistrujLekara_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new RegistracijaLekaraForma()
            { DataContext= new RegistracijaLekaraViewModel(pocetna) };
        }

        private void VidiLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniLekar = (Lekar)(ListaLekara.SelectedItem);
            if (izabraniLekar is not null)
                pocetna.contentControl.Content = new PregledNalogaLekara()
                { DataContext = new PregledNalogaLekaraViewModel(pocetna, this, (Lekar)ListaLekara.SelectedItem) };
        }

        private void ObrisiLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)ListaLekara.SelectedValue;
            NalogLekaraKontroler.Instance.UklanjanjeNalogaLekara(lekar);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private void IzmeniLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniLekar = (Lekar)(ListaLekara.SelectedItem);
            if (izabraniLekar is not null)
                pocetna.contentControl.Content = new IzmenaNalogaLekaraForma()
                { DataContext = new IzmenaLekaraViewModel(pocetna, this, izabraniLekar) };
        }

        private void Izvestaj_Click(object sender, RoutedEventArgs e)
        {
            IzvestajUtility izvestaj = new IzvestajSekretaraServis();
            izvestaj.GenerisiIzvestaj();
        }
    }
}
