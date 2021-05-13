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
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmjenaZdravstvenogKartonaForma : UserControl 
    {
        public ListView listaPacijenata;
        public PacijentiProzor pacijentiProzor;
        public PocetnaStranicaSekretara pocetna;
       

        public IzmjenaZdravstvenogKartonaForma(PacijentiProzor pacijentiProzor, PocetnaStranicaSekretara pocetna)
        {
            InitializeComponent();
            listaPacijenata = pacijentiProzor.ListaPacijenata;
            this.pacijentiProzor = pacijentiProzor;
            this.pocetna = pocetna;
            Alergeni.Instance.Deserijalizacija();
            this.ListaAlergena.ItemsSource = ((Pacijent)listaPacijenata.SelectedItem).zdravstveniKarton.Alergeni;
        }

       
        private void izmjeniNalogPacijenta_Click(object sender, RoutedEventArgs e)
        {   
            SekretarKontroler.Instance.IzmenaZdravstvenogKartona(this, listaPacijenata);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
        }

        private void dodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            DodajAlergenPacijentu dodajAlergenPacijentu = new DodajAlergenPacijentu(this);
            dodajAlergenPacijentu.Show();
        }

        private void obrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeAlergenaIzZdravstvenogKartona(this);   
        }
    }
}
