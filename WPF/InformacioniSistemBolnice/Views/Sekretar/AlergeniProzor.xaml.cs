using System.Windows;
using Repozitorijum;
using Kontroler;
using Model;
using System.Windows.Controls;

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
            this.pocetna.contentControl.Content = new DefinisanjeAlergenaForma(this);
        }

        private void ObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeAlergena((Alergen)ListaAlergena.SelectedValue);
            pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }

        private void IzmeniAlergen_Click(object sender, RoutedEventArgs e)
        {
            Alergen izabraniAlergen = (Alergen)ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new IzmenaAlergenaForma(this, izabraniAlergen);
        }

        private void PregledAlergena_Click(object sender, RoutedEventArgs e)
        {
            Alergen izabraniAlergen = (Alergen)ListaAlergena.SelectedItem;
            if (izabraniAlergen is not null)
                pocetna.contentControl.Content = new PregledAlergena(this, izabraniAlergen);
        }


    }
}
