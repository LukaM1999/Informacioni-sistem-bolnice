using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class RegistrujNovogPacijenta : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCMenuSekretara menu;
        public RegistrujNovogPacijenta(UCMenuSekretara menuSekretara)
        {
            InitializeComponent();
            menu = menuSekretara;
            pocetna = menuSekretara.pocetna;
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            PacijentDto pacijentDto = new(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text,
                                             DateTime.Parse(datumUnos.Text), telUnos.Text, mailUnos.Text,
                                             korisnikUnos.Text, lozinkaUnos.Password,
                                             drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text);
            NalogPacijentaKontroler.Instance.KreiranjeNaloga(pacijentDto);
            NalogPacijentaKontroler.Instance.KreiranjeNaloga(pacijentDto);
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = new UCMenuSekretara(menu.pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
           => this.pocetna.contentControl.Content = this.menu.Content;
    }
}
