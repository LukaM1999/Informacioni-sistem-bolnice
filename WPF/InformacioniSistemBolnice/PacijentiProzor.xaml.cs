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

            Pacijenti.Instance.Deserijalizacija();
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
                izmena.drzavaUnos.Text = p.adresa.Drzava;
                izmena.gradUnos.Text = p.adresa.Grad;
                izmena.ulicaUnos.Text = p.adresa.Ulica;
                izmena.brojUnos.Text = p.adresa.Broj;
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
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                PregledNalogaPacijenta pregledNalogaPacijenta = new PregledNalogaPacijenta(ListaPacijenata);

                pregledNalogaPacijenta.imeLabela.Content = p.ime;
                pregledNalogaPacijenta.prezimeLabela.Content = p.prezime;
                pregledNalogaPacijenta.datumLabela.Content = p.datumRodjenja.ToString();
                pregledNalogaPacijenta.drzavaLabela.Content = p.adresa.Drzava;
                pregledNalogaPacijenta.gradLabela.Content = p.adresa.Grad;
                pregledNalogaPacijenta.ulicaLabela.Content = p.adresa.Ulica;
                pregledNalogaPacijenta.brojLabela.Content = p.adresa.Broj;
                pregledNalogaPacijenta.telLabela.Content = p.telefon;
                pregledNalogaPacijenta.mailLabela.Content = p.email;
                pregledNalogaPacijenta.korisnikLabela.Content = p.korisnik.korisnickoIme;
                pregledNalogaPacijenta.JMBGUnos.Text = p.jmbg;
                pregledNalogaPacijenta.Show();


            }
        }

        private void zdravstveniKartonPacijenta(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                if (p.zdravstveniKarton != null)
                {
                    PregledZdravstvenogKartona pregledZdravstvenogKartona = new PregledZdravstvenogKartona();
                    pregledZdravstvenogKartona.brojKartona.Text = p.zdravstveniKarton.BrojKartona;
                    pregledZdravstvenogKartona.brojKnjizice.Text = p.zdravstveniKarton.BrojKnjizice;
                    pregledZdravstvenogKartona.ime.Text = p.ime;
                    pregledZdravstvenogKartona.prezime.Text = p.prezime;
                    pregledZdravstvenogKartona.imeRoditelja.Text = p.zdravstveniKarton.ImeJednogRoditelja;
                    pregledZdravstvenogKartona.JMBG.Content = p.zdravstveniKarton.Jmbg;
                    pregledZdravstvenogKartona.datumRodjenja.Content = p.datumRodjenja.ToString();
                    pregledZdravstvenogKartona.telefon.Text = p.telefon;
                    pregledZdravstvenogKartona.adresa.Text = p.adresa.Drzava + ", " + p.adresa.Grad;
                    pregledZdravstvenogKartona.ulicaIBroj.Text = p.adresa.Ulica + ", " + p.adresa.Broj;
                    pregledZdravstvenogKartona.liceZdrZastita.Text = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    pregledZdravstvenogKartona.pol.Text = p.zdravstveniKarton.PolPacijenta.ToString();
                    pregledZdravstvenogKartona.bracnoStanje.Text = p.zdravstveniKarton.BracnoStanje.ToString();
                    pregledZdravstvenogKartona.kategorijaZdravZastite.Text = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    pregledZdravstvenogKartona.radnoMjestoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada;
                    pregledZdravstvenogKartona.registarskiBrojUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj;
                    pregledZdravstvenogKartona.sifraDjelatnostiUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
                    pregledZdravstvenogKartona.posaoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
                    pregledZdravstvenogKartona.OSIZ.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
                    pregledZdravstvenogKartona.radUPosebnimUslovimaUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
                    pregledZdravstvenogKartona.promjene.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promene;

                    pregledZdravstvenogKartona.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni;


                    pregledZdravstvenogKartona.Show();
                }
                else
                {
                    MessageBox.Show("Zdravstveni karton za pacijenta nije kreiran");
                }

            }


        }


        private void izmjeniZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                if (p.zdravstveniKarton != null)
                {
                    IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma = new IzmjenaZdravstvenogKartonaForma(ListaPacijenata);
                    izmjenaZdravstvenogKartonaForma.brojKartonaUnos.Text = p.zdravstveniKarton.BrojKartona.ToString();
                    izmjenaZdravstvenogKartonaForma.brojKnjiziceUnos.Text = p.zdravstveniKarton.BrojKnjizice;
                    izmjenaZdravstvenogKartonaForma.imeLabela.Content = p.ime;
                    izmjenaZdravstvenogKartonaForma.prezimeLabela.Content = p.prezime;
                    izmjenaZdravstvenogKartonaForma.imeRoditeljaUnos.Text = p.zdravstveniKarton.ImeJednogRoditelja;
                    izmjenaZdravstvenogKartonaForma.JMBGLabela.Content = p.jmbg;
                    izmjenaZdravstvenogKartonaForma.datumRodjenjaLabela.Content = p.datumRodjenja.ToString();
                    izmjenaZdravstvenogKartonaForma.telefon.Content = p.telefon;
                    izmjenaZdravstvenogKartonaForma.adresaLabela.Content = p.adresa.Drzava + ", " + p.adresa.Grad;
                    izmjenaZdravstvenogKartonaForma.ulicaIBrojLabela.Content = p.adresa.Ulica + ", " + p.adresa.Broj;
                    izmjenaZdravstvenogKartonaForma.liceZdrZastitaUnos.Text = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    izmjenaZdravstvenogKartonaForma.polUnos.Text = p.zdravstveniKarton.PolPacijenta.ToString();
                    izmjenaZdravstvenogKartonaForma.bracnoStanjeUnos.Text = p.zdravstveniKarton.BracnoStanje.ToString();
                    izmjenaZdravstvenogKartonaForma.kategorijaZdrZastiteUnos.Text = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    izmjenaZdravstvenogKartonaForma.radnoMjestoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.MestoRada;
                    izmjenaZdravstvenogKartonaForma.registarskiBrojUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegBroj;
                    izmjenaZdravstvenogKartonaForma.sifraDjelatnostiUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
                    izmjenaZdravstvenogKartonaForma.posaoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
                    izmjenaZdravstvenogKartonaForma.OSIZ.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
                    izmjenaZdravstvenogKartonaForma.radUPosebnimUslovimaUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
                    izmjenaZdravstvenogKartonaForma.promjene.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promene;
                   
                    izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni;
                    
                    

                    izmjenaZdravstvenogKartonaForma.Show();
                }
                else
                {
                    MessageBox.Show("Zdravstveni karton za pacijenta nije kreiran");
                }
            }

        }

        private void kButton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma(ListaPacijenata);
            zdravstveniKartonForma.Show();

        }
    }
}
