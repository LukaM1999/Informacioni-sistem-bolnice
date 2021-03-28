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
using RadSaDatotekama;
using Model;
using System.Collections.ObjectModel;
using Logika;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for TerminiPacijentaProzor.xaml
    /// </summary>
    public partial class TerminiPacijentaProzor : Window
    {

        public TerminiPacijentaProzor()
        {
            InitializeComponent();

            Termini.Instance.Deserijalizacija("../../../json/slobodniTerminiPacijenta.json");
            listaSlobodnihTermina.ItemsSource = Termini.Instance.listaTermina;
            
            
        }

        private void otkaziButton_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeTerminimaPacijenata.Instance.Otkazivanje(listaZakazanihTermina);
        }
        private void pomeriButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void prikazButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void zakaziButton_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeTerminimaPacijenata.Instance.Zakazivanje(listaZakazanihTermina, listaSlobodnihTermina);
        }

    }
}
