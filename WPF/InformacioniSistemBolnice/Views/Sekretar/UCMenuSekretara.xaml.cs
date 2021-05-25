using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class UCMenuSekretara : UserControl
    {
        public PocetnaStranicaSekretara pocetna;

        public UCMenuSekretara(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
        }

        private void UpravljanjeNalozimaPacijenata_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new PacijentiProzor(pocetna);

        private void TerminiPacijenata_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new TerminiPacijentaProzorSekretara(pocetna);

        private void UpravljanjeAlergenima_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new AlergeniProzor(pocetna);

        private void GostujuciNalozi_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new GostujuciNaloziProzor(pocetna);

        private void ZakaziHitanPregled_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new UpravljanjeUrgentnimSistemomProzor(pocetna);

        private void RegistrujPacijenta_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new RegistrujNovogPacijenta(this);

        private void UpravljanjeVestima_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new VestiProzor(this);

        private void UpravljanjeNalozimaLekara_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new UCLekari(pocetna);
    }
}
