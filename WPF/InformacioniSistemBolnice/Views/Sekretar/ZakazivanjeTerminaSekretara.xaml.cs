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
    /// Interaction logic for ZakazivanjeTerminaSekretara.xaml
    /// </summary>
    public partial class ZakazivanjeTerminaSekretara : Window
    {
        public List<string> listaDatuma = new List<string>();
        
        public TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara;
        public ZakazivanjeTerminaSekretara(TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara)
        {
            InitializeComponent();

            this.terminiPacijentaProzorSekretara = terminiPacijentaProzorSekretara;
            Lekari.Instance.Deserijalizacija();
            lekari.ItemsSource = Lekari.Instance.listaLekara;
            Pacijenti.Instance.Deserijalizacija();
            pacijenti.ItemsSource = Pacijenti.Instance.ListaPacijenata;
            Prostorije.Instance.Deserijalizacija();
            prostorije.ItemsSource = Prostorije.Instance.listaProstorija;


        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)pacijenti.SelectedItem;
            IzborTerminaPacijenta izborTermina = new IzborTerminaPacijenta(this, terminiPacijentaProzorSekretara);
            izborTermina.Visibility = Visibility.Visible;

        }
    }
}
