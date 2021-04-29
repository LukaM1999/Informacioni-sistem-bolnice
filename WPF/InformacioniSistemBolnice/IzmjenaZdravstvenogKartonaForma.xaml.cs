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
    
    public partial class IzmjenaZdravstvenogKartonaForma : Window
    {
        public ListView ListaPacijenata;
       

        public IzmjenaZdravstvenogKartonaForma(ListView lp)
        {
            InitializeComponent();
            ListaPacijenata = lp;
            Alergeni.Instance.Deserijalizacija();
            this.ListaAlergena.ItemsSource = ((Pacijent)ListaPacijenata.SelectedItem).zdravstveniKarton.Alergeni;
        }

       
        private void izmjeniNalogPacijenta_Click(object sender, RoutedEventArgs e)
        {   
            SekretarKontroler.Instance.IzmenaZdravstvenogKartona(this, ListaPacijenata);
            this.Close();
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
