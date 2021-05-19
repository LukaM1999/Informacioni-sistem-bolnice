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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ZakazivanjeHitnogTermina.xaml
    /// </summary>
    public partial class ZakazivanjeHitnogTermina : UserControl
    {
        public UpravljanjeUrgentnimSistemomProzor upravljanjeUrgentnimSistemomProzor;
        public PocetnaStranicaSekretara pocetna;
        public ZakazivanjeHitnogTermina(UpravljanjeUrgentnimSistemomProzor upravljanje)
        {
            upravljanjeUrgentnimSistemomProzor = upravljanje;
            pocetna = upravljanje.pocetna;
            InitializeComponent();

            vrijeme.Content = DateTime.Now.TimeOfDay.ToString();

            Specijalizacije.Instance.Deserijalizacija();
            specijalizacijeLekara.ItemsSource = Specijalizacije.Instance.listaSpecijalizacija;

            Pacijenti.Instance.Deserijalizacija();
            pacijenti.ItemsSource = Pacijenti.Instance.ListaPacijenata;

            Prostorije.Instance.Deserijalizacija();
            prostorije.ItemsSource = Prostorije.Instance.ListaProstorija;

        }

        private void zakaziHitanTermin_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = (Pacijent)this.pacijenti.SelectedItem;
            Prostorija prostorija = (Prostorija)this.prostorije.SelectedItem;

            HitnoZakazivanjeDto hitnoZakazivanjeDto = new HitnoZakazivanjeDto(this.specijalizacijeLekara.SelectedItem.ToString(),
                pacijent.jmbg, prostorija.Id);
            SekretarKontroler.Instance.ZakazivanjeHitnogTermina(hitnoZakazivanjeDto);
            
            Termini.Instance.Deserijalizacija();
            upravljanjeUrgentnimSistemomProzor.ListaTermina.ItemsSource = Termini.Instance.listaTermina;
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.upravljanjeUrgentnimSistemomProzor.Content;


        }

        private void pomeriPostojeciTermin_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaPomeranje izborTerminaZaPomeranje = new IzborTerminaZaPomeranje(this);
            izborTerminaZaPomeranje.Show();
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.upravljanjeUrgentnimSistemomProzor.Content;
        }
    }
}
