using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class DefinisanjeAlergenaForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;

        public DefinisanjeAlergenaForma(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
        }

        private void DefinisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            AlergenDto alergenDto = new(nazivAlergenaUnos.Text);
            SekretarKontroler.Instance.DefinisanjeAlergena(alergenDto);
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }

}
