using System.Windows;
using System.Windows.Controls;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class PregledZdravstvenogKartona : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public Pacijent Pacijent { get; set; }
        
        public PregledZdravstvenogKartona(PacijentiProzor pacijentiProzor, Pacijent izabraniPacijent)
        {
            ZdravstveniKarton = izabraniPacijent.zdravstveniKarton;
            Pacijent = izabraniPacijent;
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
           =>  pocetna.contentControl.Content = pacijentiProzor.Content;

    }
}
