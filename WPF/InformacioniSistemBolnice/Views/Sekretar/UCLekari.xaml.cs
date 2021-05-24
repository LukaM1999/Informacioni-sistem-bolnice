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
            LekarRepo.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = LekarRepo.Instance.Lekari;

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
            pregledNalogaLekara.korisnikUnos.Text = lekar.KorisnickoIme;
            pregledNalogaLekara.specijalizacijeLekara.Text = lekar.Specijalizacija;
        }

        private static void PregledAdrese(LekarDto lekar, PregledNalogaLekara pregledNalogaLekara)
        {
            pregledNalogaLekara.drzavaUnos.Text = lekar.Drzava;
            pregledNalogaLekara.gradUnos.Text = lekar.Grad;
            pregledNalogaLekara.ulicaUnos.Text = lekar.Ulica;
            pregledNalogaLekara.brojUnos.Text = lekar.Broj;
        }

        private static void PregledLicnihPodataka(LekarDto lekar, PregledNalogaLekara pregledNalogaLekara)
        {
            pregledNalogaLekara.imeUnos.Text = lekar.Ime;
            pregledNalogaLekara.prezimeUnos.Text = lekar.Prezime;
            pregledNalogaLekara.JMBGUnos.Text = lekar.LekarJmbg;
            pregledNalogaLekara.datumUnos.Text = lekar.DatumRodjenja.ToString("MM/dd/yyyy");
            pregledNalogaLekara.telUnos.Text = lekar.Telefon;
            pregledNalogaLekara.mailUnos.Text = lekar.Email;
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
            LekarRepo.Instance.Deserijalizacija();
            ListaLekara.ItemsSource = LekarRepo.Instance.Lekari;
        }

        private void izmeniLekara_Click(object sender, RoutedEventArgs e)
        {
            if (ListaLekara.SelectedValue != null)
            {
                Lekar lekar = (Lekar)(ListaLekara.SelectedItem);
                IzmenaNalogaLekaraForma izmena = new IzmenaNalogaLekaraForma(pocetna, this);
                izmena.imeUnos.Text = lekar.Ime;
                izmena.prezimeUnos.Text = lekar.Prezime;
                izmena.JMBGUnos.Text = lekar.Jmbg;
                izmena.drzavaUnos.Text = lekar.AdresaStanovanja.Drzava;
                izmena.gradUnos.Text = lekar.AdresaStanovanja.Grad;
                izmena.ulicaUnos.Text = lekar.AdresaStanovanja.Ulica;
                izmena.brojUnos.Text = lekar.AdresaStanovanja.Broj;
                izmena.datumUnos.Text = lekar.DatumRodjenja.ToString("MM/dd/yyyy");
                izmena.telUnos.Text = lekar.Telefon;
                izmena.mailUnos.Text = lekar.Email;
                izmena.korisnikUnos.Text = lekar.Korisnik.KorisnickoIme;
                izmena.lozinkaUnos.Password = lekar.Korisnik.Lozinka;
                izmena.specijalizacijeLekara.SelectedItem = lekar.Specijalizacija;
                pocetna.contentControl.Content = izmena.Content;
            }

        }
    }
}
