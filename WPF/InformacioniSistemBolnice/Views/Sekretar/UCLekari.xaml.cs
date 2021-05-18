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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Repozitorijum;
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    
    public partial class UCLekari : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCLekari(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            Lekari.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = Lekari.Instance.listaLekara;

        }

        private void RegistrujLekara_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new RegistracijaLekaraForma(pocetna);
        }

        private void PregledNalogaLekara_Click(object sender, RoutedEventArgs e)
        {
            if (ListaLekara.SelectedValue != null)
            {
                LekarDto lekar = SekretarKontroler.Instance.PregledNalogaLekara((Lekar)ListaLekara.SelectedItem);
                PregledNalogaLekara pregledNalogaLekara = new PregledNalogaLekara(pocetna, this);
                PregledLicnihPodataka(lekar, pregledNalogaLekara);
                PregledAdrese(lekar, pregledNalogaLekara);
                PregledKorisnickihPodataka(lekar, pregledNalogaLekara);
                pocetna.contentControl.Content = pregledNalogaLekara.Content;
            }
        }

        private static void PregledKorisnickihPodataka(LekarDto lekar, PregledNalogaLekara pregledNalogaLekara)
        {
            pregledNalogaLekara.korisnikUnos.Text = lekar.korisnickoIme;
            pregledNalogaLekara.specijalizacijeLekara.Text = lekar.specijalizacija;
        }

        private static void PregledAdrese(LekarDto lekar, PregledNalogaLekara pregledNalogaLekara)
        {
            pregledNalogaLekara.drzavaUnos.Text = lekar.drzava;
            pregledNalogaLekara.gradUnos.Text = lekar.grad;
            pregledNalogaLekara.ulicaUnos.Text = lekar.ulica;
            pregledNalogaLekara.brojUnos.Text = lekar.broj;
        }

        private static void PregledLicnihPodataka(LekarDto lekar, PregledNalogaLekara pregledNalogaLekara)
        {
            pregledNalogaLekara.imeUnos.Text = lekar.ime;
            pregledNalogaLekara.prezimeUnos.Text = lekar.prezime;
            pregledNalogaLekara.JMBGUnos.Text = lekar.jmbg;
            pregledNalogaLekara.datumUnos.Text = lekar.datumRodjenja.ToString("MM/dd/yyyy");
            pregledNalogaLekara.telUnos.Text = lekar.telefon;
            pregledNalogaLekara.mailUnos.Text = lekar.email;
        }

        private void ObrisiLekara_Click(object sender, RoutedEventArgs e)
        {
            if (ListaLekara.SelectedValue != null)
            {
                Lekar lekar = (Lekar)ListaLekara.SelectedValue;
                SekretarKontroler.Instance.UklanjanjeNalogaLekara(lekar);
                AzurirajListuLekara();
            }
        }

        private void AzurirajListuLekara()
        {
            Lekari.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = Lekari.Instance.listaLekara;
        }

        private void izmeniLekara_Click(object sender, RoutedEventArgs e)
        {
            if (ListaLekara.SelectedValue != null)
            {
                Lekar lekar = (Lekar)(ListaLekara.SelectedItem);
                IzmenaNalogaLekaraForma izmena = new IzmenaNalogaLekaraForma(pocetna, this);
                izmena.imeUnos.Text = lekar.ime;
                izmena.prezimeUnos.Text = lekar.prezime;
                izmena.JMBGUnos.Text = lekar.jmbg;
                izmena.drzavaUnos.Text = lekar.adresa.Drzava;
                izmena.gradUnos.Text = lekar.adresa.Grad;
                izmena.ulicaUnos.Text = lekar.adresa.Ulica;
                izmena.brojUnos.Text = lekar.adresa.Broj;
                izmena.datumUnos.Text = lekar.datumRodjenja.ToString("MM/dd/yyyy");
                izmena.telUnos.Text = lekar.telefon;
                izmena.mailUnos.Text = lekar.email;
                izmena.korisnikUnos.Text = lekar.korisnik.korisnickoIme;
                izmena.lozinkaUnos.Password = lekar.korisnik.lozinka;
                izmena.specijalizacijeLekara.SelectedItem = lekar.specijalizacija;
                pocetna.contentControl.Content = izmena.Content;
            }

        }
    }
}
