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
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class PacijentiProzor : Window
    {
       
        public PacijentiProzor()
        {
            InitializeComponent();
            
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void registrujPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {
            RegistracijaPacijentaForma rP = new RegistracijaPacijentaForma();
            rP.Show();
        }

        private void izmeniPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {
           
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                IzmenaNalogaPacijentaForma izmena = new IzmenaNalogaPacijentaForma(ListaPacijenata);
                izmena.imeUnos.Text = p.ime;
                izmena.prezimeUnos.Text = p.prezime;
                izmena.JMBGUnos.Text = p.jmbg;
                izmena.datumUnos.Text = p.datumRodjenja.ToString("MM/dd/yyyy");
                izmena.telUnos.Text = p.telefon;
                izmena.mailUnos.Text = p.email;
                izmena.korisnikUnos.Text = p.korisnik.korisnickoIme;
                izmena.lozinkaUnos.Password = p.korisnik.lozinka;
                izmena.Show();
            }
             

        }

        private void ObrisiPacijenta(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeNaloga(ListaPacijenata);
        }

       
        private void pregledPacijenta(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PregledNaloga(ListaPacijenata);

        }
    }
}
