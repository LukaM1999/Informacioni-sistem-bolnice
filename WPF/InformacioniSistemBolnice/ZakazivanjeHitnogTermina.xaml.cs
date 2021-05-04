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
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ZakazivanjeHitnogTermina.xaml
    /// </summary>
    public partial class ZakazivanjeHitnogTermina : Window
    {
        public UpravljanjeUrgentnimSistemomProzor upravljanjeUrgentnimSistemomProzor;
        public ZakazivanjeHitnogTermina(UpravljanjeUrgentnimSistemomProzor upravljanje)
        {
            upravljanjeUrgentnimSistemomProzor = upravljanje;
            InitializeComponent();

            vrijeme.Content = DateTime.Now.TimeOfDay.ToString();

            Specijalizacije.Instance.Deserijalizacija();
            specijalizacijeLekara.ItemsSource = Specijalizacije.Instance.listaSpecijalizacija;

            Pacijenti.Instance.Deserijalizacija();
            pacijenti.ItemsSource = Pacijenti.Instance.listaPacijenata;

            Prostorije.Instance.Deserijalizacija();
            prostorije.ItemsSource = Prostorije.Instance.listaProstorija;

        }

        private void zakaziHitanTermin_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeUrgentnimSistemom.Instance.ZakazivanjeHitnogTermina(this);
            this.Close();
            Termini.Instance.Deserijalizacija();
            upravljanjeUrgentnimSistemomProzor.ListaTermina.ItemsSource = Termini.Instance.listaTermina;
        }

        private void pomeriPostojeciTermin_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaPomeranje izborTerminaZaPomeranje = new IzborTerminaZaPomeranje(this);
            izborTerminaZaPomeranje.Show();
        }
    }
}
