using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Kontroler;

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
            pocetna.contentControl.Content = new RegistracijaLekaraForma(pocetna);
        }

        private void VidiLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniLekar = (Lekar)(ListaLekara.SelectedItem);
            if (izabraniLekar is not null)
                pocetna.contentControl.Content = new PregledNalogaLekara(pocetna, this, (Lekar)ListaLekara.SelectedItem);
        }

        private void ObrisiLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)ListaLekara.SelectedValue;
            SekretarKontroler.Instance.UklanjanjeNalogaLekara(lekar);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private void IzmeniLekara_Click(object sender, RoutedEventArgs e)
        {
            Lekar izabraniLekar = (Lekar)(ListaLekara.SelectedItem);
            if (izabraniLekar is not null)
                pocetna.contentControl.Content = new IzmenaNalogaLekaraForma(pocetna, this, izabraniLekar);
        }
    }
}
