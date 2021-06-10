using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Kontroler;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;
using InformacioniSistemBolnice.Servis.UpravljanjeIzvestajima;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.Views.SekretarView;
using System.Collections.ObjectModel;

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
            IzvestajKontroler.Instance.GenerisiSekretarovIzvestaj();
        }

        private void IzgenerisiGrafik_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new IzborIntervalaZauzetostiLekara()
            { DataContext = new IntervalZauzetostiViewModel(this.pocetna) };
        }


        private bool Nadji(string text, Lekar l)
        {
            return l.Ime.ToLower().Contains(text) || l.Prezime.ToLower().Contains(text) ||
                   l.Jmbg.ToLower().StartsWith(text);
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ObservableCollection<Lekar> Filter = new ObservableCollection<Lekar>();
            foreach (var lekar in LekarRepo.Instance.Lekari)
            {
                if (Nadji(searchBox.Text.ToLower(), lekar))
                {
                    Filter.Add(lekar);
                }
            }

            ListaLekara.ItemsSource = Filter;
        }
    }
}
