using System.Windows;
using Repozitorijum;
using Kontroler;
using Model;
using System.Windows.Controls;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;
using InformacioniSistemBolnice.Views.SekretarView;

namespace InformacioniSistemBolnice
{
    public partial class AlergeniProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            AlergenRepo.Instance.Deserijalizacija();
            ListaAlergena.ItemsSource = AlergenRepo.Instance.Alergeni;
        }

        private void DefinisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new DefinisanjeAlergenaForma()
            { DataContext = new DefinisanjeAlergenaViewModel(this) };
        }

        private void ObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeAlergena((Alergen)ListaAlergena.SelectedItem);

        }

        private void IzmjeniAlergen_Click(object sender, RoutedEventArgs e)
        {
            Alergen izabraniAlergen = (Alergen)ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new IzmenaAlergenaForma()
                { DataContext = new IzmenaAlergenaViewModel(this, izabraniAlergen) };
        }

        private void PregledAlergena_Click(object sender, RoutedEventArgs e)
        {
            Alergen izabraniAlergen = (Alergen)ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new PregledAlergena()
                { DataContext = new PregledAlergenaViewModel(this, izabraniAlergen) };

        }

    }
}


