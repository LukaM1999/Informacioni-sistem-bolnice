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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UpravljanjeUrgentnimSistemomProzor.xaml
    /// </summary>
    public partial class UpravljanjeUrgentnimSistemomProzor : Window
    {
        public UpravljanjeUrgentnimSistemomProzor()
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            Termini.Instance.Deserijalizacija();
            ListaTermina.ItemsSource = Termini.Instance.listaTermina;

        }

        private void zakaziHitanTermin(object sender, RoutedEventArgs e)
        {
            ZakazivanjeHitnogTermina zakazivanjeHitnogTermina = new ZakazivanjeHitnogTermina(this);
            zakazivanjeHitnogTermina.Show();
        }


        private void kreirajGostujucegPacijenta_click(object sender, RoutedEventArgs e)
        {
            KreiranjeGostujucegPacijentaProzor kreiranjeGostujucegPacijentaProzor = new KreiranjeGostujucegPacijentaProzor();
            kreiranjeGostujucegPacijentaProzor.Show();
        }
    }
}
