using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class PregledNalogaPacijenta : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijenti;
        public PregledNalogaPacijenta(PacijentiProzor pacijentiProzor)
        {
            InitializeComponent();
            pacijenti = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
           
        }

        private void zdravstveniKartonPacijenta_Click(object sender, RoutedEventArgs e)
        {
           //UpravljanjeNalozimaPacijenata.Instance.PregledZdravstvenogKartona(this);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijenti.Content;
        }
    }
}
