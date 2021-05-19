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

namespace InformacioniSistemBolnice
{
    public partial class PregledNalogaLekara : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCLekari UCLekari;

        public PregledNalogaLekara(PocetnaStranicaSekretara pocetnaStranicaSekretara, UCLekari uCLekari)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            UCLekari = uCLekari;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = UCLekari.Content;
        }
    }
}
