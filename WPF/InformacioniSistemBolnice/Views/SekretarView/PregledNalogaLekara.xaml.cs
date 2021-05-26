using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledNalogaLekara : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public Lekari lekari;
        public Lekar Lekar { get; set;}

        public PregledNalogaLekara(PocetnaStranicaSekretara pocetnaStranicaSekretara, Lekari uCLekari, Lekar izabraniLekar)
        {
            Lekar = izabraniLekar;
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            lekari = uCLekari;      
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
           => pocetna.contentControl.Content = lekari.Content;

        private void drzavaUnos_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
