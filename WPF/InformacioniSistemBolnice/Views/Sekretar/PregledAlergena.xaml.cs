using System;
using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class PregledAlergena : UserControl
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public PregledAlergena(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
    }
}
