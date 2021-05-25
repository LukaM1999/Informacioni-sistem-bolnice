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
        public Pacijent Pacijent { get; set; }

        public PregledNalogaPacijenta(PacijentiProzor pacijentiProzor, Pacijent izabraniPacijent)
        {
            Pacijent = izabraniPacijent;
            InitializeComponent();
            pacijenti = pacijentiProzor;
            pocetna = pacijentiProzor.pocetna;
           
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijenti.Content;
        }
    }
}
