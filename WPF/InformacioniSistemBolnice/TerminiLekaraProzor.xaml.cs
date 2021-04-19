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
using Repozitorijum;
using Model;
using System.Collections.ObjectModel;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class TerminiLekaraProzor : Window
    {

        public TerminiLekaraProzor()
        {
            InitializeComponent();

            Termini.Instance.Deserijalizacija("../../../json/zakazaniTermini.json");
            ObservableCollection<Termin> zakazaniTermini = new ObservableCollection<Termin>();
            listaZakazanihTerminaLekara.ItemsSource = zakazaniTermini;


        }

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                PomeranjeTerminaLekaraProzor pomeranje = new PomeranjeTerminaLekaraProzor(listaZakazanihTerminaLekara);
                pomeranje.Show();
            }
        }


        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaLekaraProzor zakazivanjeProzor = new ZakazivanjeTerminaLekaraProzor();
            zakazivanjeProzor.Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.Otkazivanje(listaZakazanihTerminaLekara);
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.Uvid(listaZakazanihTerminaLekara);
        }
    }
}
