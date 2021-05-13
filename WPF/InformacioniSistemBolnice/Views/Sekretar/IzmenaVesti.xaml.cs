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
using Kontroler;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmenaVesti : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;
        public IzmenaVesti(VestiProzor vestiProzor, PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            vesti = vestiProzor;
            pocetna = pocetnaStranicaSekretara;
        }

        private void izmeniVest_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmenaVesti(vesti, this);
            pocetna.contentControl.Content = vesti.Content;
            
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = vesti.Content;
        }
    }
}
