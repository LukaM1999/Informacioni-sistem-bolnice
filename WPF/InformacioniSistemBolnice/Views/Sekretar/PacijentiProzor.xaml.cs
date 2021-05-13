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
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class PacijentiProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Pacijent> pacijenti = new ObservableCollection<Pacijent>();
        public PacijentiProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija();
            
            foreach (Pacijent pacijent in Pacijenti.Instance.ListaPacijenata)
            {   
                if(pacijent.korisnik.korisnickoIme != null)
                {
                    pacijenti.Add(pacijent);
                }
            }
            ListaPacijenata.ItemsSource = pacijenti.ToList();
            pocetna = pocetnaStranicaSekretara;
        }

       

        private void registrujPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new RegistracijaPacijentaForma(this);
        }

        private void izmeniPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {

            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                IzmenaNalogaPacijentaForma izmena = new IzmenaNalogaPacijentaForma(this, pocetna);
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
                pocetna.contentControl.Content = izmena.Content;
            }


        }

        private void ObrisiPacijenta(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeNaloga(ListaPacijenata);
            PacijentiProzor pacijentiProzor = new PacijentiProzor(pocetna);
            this.pocetna.contentControl.Content = pacijentiProzor;
        }


        private void pregledPacijenta(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                PregledNalogaPacijenta pregledNalogaPacijenta = new PregledNalogaPacijenta(this);

                pregledNalogaPacijenta.imeLabela.Content = p.ime;
                pregledNalogaPacijenta.prezimeLabela.Content = p.prezime;
                pregledNalogaPacijenta.datumLabela.Content = p.datumRodjenja.ToString("dd/MM/yyyy");
                pregledNalogaPacijenta.drzavaLabela.Content = p.adresa.Drzava;
                pregledNalogaPacijenta.gradLabela.Content = p.adresa.Grad;
                pregledNalogaPacijenta.ulicaLabela.Content = p.adresa.Ulica;
                pregledNalogaPacijenta.brojLabela.Content = p.adresa.Broj;
                pregledNalogaPacijenta.telLabela.Content = p.telefon;
                pregledNalogaPacijenta.mailLabela.Content = p.email;
                pregledNalogaPacijenta.korisnikLabela.Content = p.korisnik.korisnickoIme;
                pregledNalogaPacijenta.JMBGUnos.Content = p.jmbg;
                pocetna.contentControl.Content = pregledNalogaPacijenta;


            }
        }

        private void zdravstveniKartonPacijenta(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                if (p.zdravstveniKarton != null)
                {
                    PregledZdravstvenogKartona pregledZdravstvenogKartona = new PregledZdravstvenogKartona(this);
                    pregledZdravstvenogKartona.brojKartona.Content = p.zdravstveniKarton.BrojKartona;
                    pregledZdravstvenogKartona.brojKnjizice.Content= p.zdravstveniKarton.BrojKnjizice;
                    pregledZdravstvenogKartona.imeLabela.Content = p.ime;
                    pregledZdravstvenogKartona.prezime.Content = p.prezime;
                    pregledZdravstvenogKartona.imeRoditelja.Content = p.zdravstveniKarton.ImeJednogRoditelja;
                    pregledZdravstvenogKartona.JMBG.Content = p.zdravstveniKarton.Jmbg;
                    pregledZdravstvenogKartona.datumRodjenja.Content = p.datumRodjenja.ToString("dd/MM/yyyy");
                    pregledZdravstvenogKartona.telefon.Content = p.telefon;
                    pregledZdravstvenogKartona.adresa.Content = p.adresa.Drzava + ", " + p.adresa.Grad;
                    pregledZdravstvenogKartona.ulicaIBroj.Content = p.adresa.Ulica + ", " + p.adresa.Broj;
                    pregledZdravstvenogKartona.liceZdrZastita.Content = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    pregledZdravstvenogKartona.pol.Content = p.zdravstveniKarton.PolPacijenta.ToString();
                    pregledZdravstvenogKartona.bracnoStanje.Content = p.zdravstveniKarton.BracnoStanje.ToString();
                    pregledZdravstvenogKartona.kategorijaZdravZastite.Content = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    pregledZdravstvenogKartona.radnoMjestoUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadnoMjesto;
                    pregledZdravstvenogKartona.registarskiBrojUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegistarskiBroj;
                    pregledZdravstvenogKartona.sifraDjelatnostiUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
                    pregledZdravstvenogKartona.posaoUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
                    pregledZdravstvenogKartona.OSIZ.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
                    pregledZdravstvenogKartona.radUPosebnimUslovimaUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
                    pregledZdravstvenogKartona.promjene.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promjene;
                    pregledZdravstvenogKartona.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni.ToList();
                    this.pocetna.contentControl.Content = pregledZdravstvenogKartona.Content;
                }
                else
                {
                    MessageBox.Show("Zdravstveni karton za pacijenta nije kreiran");
                }

            }


        }


        private void izmjeniZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                if (p.zdravstveniKarton != null)
                {
                    IzmjenaZdravstvenogKartonaForma izmjenaZdravstvenogKartonaForma = new IzmjenaZdravstvenogKartonaForma(this, pocetna);
                    izmjenaZdravstvenogKartonaForma.brojKartona.Text = p.zdravstveniKarton.BrojKartona.ToString();
                    izmjenaZdravstvenogKartonaForma.brojKnjizice.Text = p.zdravstveniKarton.BrojKnjizice;
                    izmjenaZdravstvenogKartonaForma.imeLabela.Content = p.ime;
                    izmjenaZdravstvenogKartonaForma.prezimeLabela.Content = p.prezime;
                    izmjenaZdravstvenogKartonaForma.imeRoditelja.Text = p.zdravstveniKarton.ImeJednogRoditelja;
                    izmjenaZdravstvenogKartonaForma.JMBGLabela.Content = p.jmbg;
                    izmjenaZdravstvenogKartonaForma.datumRodjenjaLabela.Content = p.datumRodjenja.ToString("dd/MM/yyyy");
                    izmjenaZdravstvenogKartonaForma.telefon.Content = p.telefon;
                    izmjenaZdravstvenogKartonaForma.adresa.Content = p.adresa.Drzava + ", " + p.adresa.Grad;
                    izmjenaZdravstvenogKartonaForma.ulicaIBrojLabela.Content = p.adresa.Ulica + ", " + p.adresa.Broj;
                    izmjenaZdravstvenogKartonaForma.liceZdrZastita.Text = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    izmjenaZdravstvenogKartonaForma.polUnos.Text = p.zdravstveniKarton.PolPacijenta.ToString();
                    izmjenaZdravstvenogKartonaForma.bracnoStanjeUnos.Text = p.zdravstveniKarton.BracnoStanje.ToString();
                    izmjenaZdravstvenogKartonaForma.kategorijaZdrZastiteUnos.Text = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    izmjenaZdravstvenogKartonaForma.radnoMjestoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadnoMjesto;
                    izmjenaZdravstvenogKartonaForma.registarskiBrojUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RegistarskiBroj;
                    izmjenaZdravstvenogKartonaForma.sifraDjelatnostiUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.SifraDelatnosti;
                    izmjenaZdravstvenogKartonaForma.posaoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.PosaoKojiObavlja;
                    izmjenaZdravstvenogKartonaForma.OSIZ.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.OSIZZdrZastite;
                    izmjenaZdravstvenogKartonaForma.radUPosebnimUslovimaUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.RadPodPosebnimUslovima;
                    izmjenaZdravstvenogKartonaForma.promjene.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanjuPacijenta.Promjene;
                    izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni.ToList();
                    pocetna.contentControl.Content = izmjenaZdravstvenogKartonaForma.Content;
                }
                else
                {
                    MessageBox.Show("Zdravstveni karton za pacijenta nije kreiran");
                }
            }

        }

        private void kButton_Click(object sender, RoutedEventArgs e)
        {
            //ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma(ListaPacijenata, pocetna, izmenaNalogaPacijentaForma);
            //zdravstveniKartonForma.Show();

        }

       
    }
}
