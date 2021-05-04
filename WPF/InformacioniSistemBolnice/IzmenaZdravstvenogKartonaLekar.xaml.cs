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
    
    public partial class IzmenaZdravstvenogKartonaLekar : Window
    {
        public Pacijent pacijent;
       

        public IzmenaZdravstvenogKartonaLekar(Pacijent pacijent)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            Alergeni.Instance.Deserijalizacija();
            Kartoni.Instance.Deserijalizacija();
            this.pacijent = pacijent;
            this.brojKartonaLabela.Content = pacijent.zdravstveniKarton.BrojKartona;
            this.brojKnjiziceLabela.Content = pacijent.zdravstveniKarton.BrojKnjizice;
            this.jmbgLabela.Content = pacijent.zdravstveniKarton.Jmbg;
            this.prezimeLabela.Content = pacijent.prezime;
            this.roditeljLabela.Content = pacijent.zdravstveniKarton.ImeJednogRoditelja;
            this.imeLabela.Content = pacijent.ime;
            this.datumRodjenjaLabela.Content = pacijent.datumRodjenja;
            this.adresaLabela.Content = pacijent.adresa.Grad + ", " + pacijent.adresa.Drzava;
            this.ulicaIBrojLabela.Content = pacijent.adresa.Ulica + " " + pacijent.adresa.Broj;
            this.telefonLabela.Content = pacijent.telefon;
            this.polLabela.Content = pacijent.zdravstveniKarton.PolPacijenta;
            this.bracnoStanjeLabela.Content = pacijent.zdravstveniKarton.BracnoStanje;
            this.zdravstvenaZastitaLabela.Content = pacijent.zdravstveniKarton.LiceZaZdravstvenuZastitu;
            this.kategorijaZastiteLabela.Content = pacijent.zdravstveniKarton.KategorijaZdravstveneZastite;
            this.mestoRadaLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada;
            this.registarskiBrLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj;
            this.sifraDelatnostiLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
            this.posaoLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
            this.OSIZLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
            this.posebniUsloviLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
            this.promeneLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promene;
            this.ListaAlergena.ItemsSource = pacijent.zdravstveniKarton.Alergeni;
        }

        public IzmenaZdravstvenogKartonaLekar(Termin termin)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                PopunjavaKarton(termin, pacijent);
            }
        }

        private void PopunjavaKarton(Termin termin, Pacijent pacijent)
        {
            if (pacijent.jmbg.Equals(termin.pacijentJMBG))
            {
                Alergeni.Instance.Deserijalizacija();
                Kartoni.Instance.Deserijalizacija();
                this.pacijent = pacijent;
                this.brojKartonaLabela.Content = pacijent.zdravstveniKarton.BrojKartona;
                this.brojKnjiziceLabela.Content = pacijent.zdravstveniKarton.BrojKnjizice;
                this.jmbgLabela.Content = pacijent.zdravstveniKarton.Jmbg;
                this.prezimeLabela.Content = pacijent.prezime;
                this.roditeljLabela.Content = pacijent.zdravstveniKarton.ImeJednogRoditelja;
                this.imeLabela.Content = pacijent.ime;
                this.datumRodjenjaLabela.Content = pacijent.datumRodjenja;
                this.adresaLabela.Content = pacijent.adresa.Grad + ", " + pacijent.adresa.Drzava;
                this.ulicaIBrojLabela.Content = pacijent.adresa.Ulica + " " + pacijent.adresa.Broj;
                this.telefonLabela.Content = pacijent.telefon;
                this.polLabela.Content = pacijent.zdravstveniKarton.PolPacijenta;
                this.bracnoStanjeLabela.Content = pacijent.zdravstveniKarton.BracnoStanje;
                this.zdravstvenaZastitaLabela.Content = pacijent.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                this.kategorijaZastiteLabela.Content = pacijent.zdravstveniKarton.KategorijaZdravstveneZastite;
                this.mestoRadaLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada;
                this.registarskiBrLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj;
                this.sifraDelatnostiLabela.Content =
                    pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
                this.posaoLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
                this.OSIZLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
                this.posebniUsloviLabela.Content =
                    pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
                this.promeneLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promene;
                this.ListaAlergena.ItemsSource = pacijent.zdravstveniKarton.Alergeni;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnezaForma anamneza = new AnamnezaForma(pacijent);
            anamneza.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReceptForma recept = new ReceptForma(pacijent);
            recept.Show();
        }
        private void Uput_Click(object sender, RoutedEventArgs e)
        {
            UputForma uputForma = new(pacijent);
            uputForma.Show();
        }
    }
}
