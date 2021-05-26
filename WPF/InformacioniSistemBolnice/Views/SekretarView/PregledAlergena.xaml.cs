using System;
using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledAlergena : UserControl
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public Alergen Alergen { get; set; }

        public PregledAlergena(AlergeniProzor alergeniProzor, Alergen izabraniAlergen)
        {
            Alergen = izabraniAlergen;
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new AlergeniProzor(pocetna);
    }
}
