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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    
    public partial class UCLekari : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCLekari(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            Lekari.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = Lekari.Instance.listaLekara;

        }

        private void RegistrujLekara_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PregledNalogaLekara_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ObrisiLekara_Click(object sender, RoutedEventArgs e)
        {

        }

        private void izmeniLekara_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
