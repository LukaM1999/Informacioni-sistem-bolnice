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
using InformacioniSistemBolnice;

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

            Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
            listaZakazanihTermina.ItemsSource = Termini.Instance.listaTermina;


        }

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                PomeranjeTerminaPacijentaProzor pomeranje = new PomeranjeTerminaPacijentaProzor(listaZakazanihTermina);
                pomeranje.Show();
            }
        }


        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaPacijentaProzor zakazivanjeProzor = new ZakazivanjeTerminaPacijentaProzor();
            zakazivanjeProzor.Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeTerminimaPacijenata.Instance.Otkazivanje(listaZakazanihTermina);
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeTerminimaPacijenata.Instance.Uvid(listaZakazanihTermina);
        }
    }
}
