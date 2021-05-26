using System;
using System.Windows;
using System.Windows.Controls;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UpravljanjeUrgentnimSistemomProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UpravljanjeUrgentnimSistemomProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            ListaTermina.ItemsSource = TerminRepo.Instance.Termini;
            pocetna = pocetnaStranicaSekretara;
        }

        private void zakaziHitanTermin(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new ZakazivanjeHitnogTermina(this);
        }

    }
}
