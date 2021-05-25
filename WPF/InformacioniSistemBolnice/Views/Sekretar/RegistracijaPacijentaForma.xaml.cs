using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class RegistracijaPacijentaForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        public RegistracijaPacijentaForma(PacijentiProzor pacijentiProzor)
        {
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            PacijentDto registracijaPacijentaDto = new(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text,
                                             DateTime.Parse(datumUnos.Text), telUnos.Text, mailUnos.Text,
                                             korisnikUnos.Text, lozinkaUnos.Password,
                                             drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text);
            SekretarKontroler.Instance.KreiranjeNaloga(registracijaPacijentaDto);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
                  => pocetna.contentControl.Content = pacijentiProzor.Content;

    }
}
