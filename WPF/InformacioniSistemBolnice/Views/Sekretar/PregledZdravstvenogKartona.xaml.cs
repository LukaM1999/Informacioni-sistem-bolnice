using System.Windows;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class PregledZdravstvenogKartona : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        
        public PregledZdravstvenogKartona(PacijentiProzor pacijentiProzor)
        {
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
           =>  pocetna.contentControl.Content = pacijentiProzor.Content;

    }
}
