using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledNalogaPacijenta : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijenti;
        public Pacijent Pacijent { get; set; }

        public PregledNalogaPacijenta(PacijentiProzor pacijentiProzor, Pacijent izabraniPacijent)
        {
            Pacijent = izabraniPacijent;
            InitializeComponent();
            pacijenti = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => this.pocetna.contentControl.Content = this.pacijenti.Content;
    }
}
