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

namespace InformacioniSistemBolnice
{
    public partial class PregledAlergena : UserControl
    {
        public ListView listaAlergena;
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor alergeni;
        public PregledAlergena(AlergeniProzor alergeniProzor)
        {
            InitializeComponent();
            alergeni = alergeniProzor;
            pocetna = alergeniProzor.pocetna;
        }

       
        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new AlergeniProzor(this.pocetna);
        }
    }
}
