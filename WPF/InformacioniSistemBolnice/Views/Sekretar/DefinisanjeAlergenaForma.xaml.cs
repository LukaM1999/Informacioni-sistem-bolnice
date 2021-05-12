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
    public partial class DefinisanjeAlergenaForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public DefinisanjeAlergenaForma(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
        }

        private void definisiAlergenDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.DefinisanjeAlergena(this);
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }

}
