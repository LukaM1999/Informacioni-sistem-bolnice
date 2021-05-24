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
            PacijentRepo.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti) pacijenti.Add(pacijent);
            ListaPacijenata.ItemsSource = pacijenti.ToList();
            pocetna = pocetnaStranicaSekretara;
        }

        private void registrujPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new RegistracijaPacijentaForma(this);
        }

        private void ObrisiPacijenta(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
                SekretarKontroler.Instance.UklanjanjeNaloga((Pacijent)ListaPacijenata.SelectedItem);
            this.pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }
        private void izmeniPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {

            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                IzmenaNalogaPacijentaForma izmena = new IzmenaNalogaPacijentaForma(this, pocetna);
                izmena.imeUnos.Text = p.Ime;
                izmena.prezimeUnos.Text = p.Prezime;
                izmena.JMBGUnos.Text = p.Jmbg;
                izmena.drzavaUnos.Text = p.AdresaStanovanja.Drzava;
                izmena.gradUnos.Text = p.AdresaStanovanja.Grad;
                izmena.ulicaUnos.Text = p.AdresaStanovanja.Ulica;
                izmena.brojUnos.Text = p.AdresaStanovanja.Broj;
                izmena.datumUnos.Text = p.DatumRodjenja.ToString("MM/dd/yyyy");
                izmena.telUnos.Text = p.Telefon;
                izmena.mailUnos.Text = p.Email;
                izmena.korisnikUnos.Text = p.Korisnik.KorisnickoIme;
                izmena.lozinkaUnos.Password = p.Korisnik.Lozinka;
                pocetna.contentControl.Content = izmena.Content;
            }


        }

 
        private void pregledPacijenta(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)ListaPacijenata.SelectedItem;
                PregledNalogaPacijenta pregledNalogaPacijenta = new PregledNalogaPacijenta(this);
                pregledNalogaPacijenta.imeLabela.Content = p.Ime;
                pregledNalogaPacijenta.prezimeLabela.Content = p.Prezime;
                pregledNalogaPacijenta.datumLabela.Content = p.DatumRodjenja.ToString("dd/MM/yyyy");
                pregledNalogaPacijenta.drzavaLabela.Content = p.AdresaStanovanja.Drzava;
                pregledNalogaPacijenta.gradLabela.Content = p.AdresaStanovanja.Grad;
                pregledNalogaPacijenta.ulicaLabela.Content = p.AdresaStanovanja.Ulica;
                pregledNalogaPacijenta.brojLabela.Content = p.AdresaStanovanja.Broj;
                pregledNalogaPacijenta.telLabela.Content = p.Telefon;
                pregledNalogaPacijenta.mailLabela.Content = p.Email;
                pregledNalogaPacijenta.korisnikLabela.Content = p.Korisnik.KorisnickoIme;
                pregledNalogaPacijenta.JMBGUnos.Content = p.Jmbg;
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
                    pregledZdravstvenogKartona.brojKnjizice.Content = p.zdravstveniKarton.BrojKnjizice;
                    pregledZdravstvenogKartona.imeLabela.Content = p.Ime;
                    pregledZdravstvenogKartona.prezime.Content = p.Prezime;
                    pregledZdravstvenogKartona.imeRoditelja.Content = p.zdravstveniKarton.ImeJednogRoditelja;
                    pregledZdravstvenogKartona.JMBG.Content = p.zdravstveniKarton.Jmbg;
                    pregledZdravstvenogKartona.datumRodjenja.Content = p.DatumRodjenja.ToString("dd/MM/yyyy");
                    pregledZdravstvenogKartona.telefon.Content = p.Telefon;
                    pregledZdravstvenogKartona.adresa.Content = p.AdresaStanovanja.Drzava + ", " + p.AdresaStanovanja.Grad;
                    pregledZdravstvenogKartona.ulicaIBroj.Content = p.AdresaStanovanja.Ulica + ", " + p.AdresaStanovanja.Broj;
                    pregledZdravstvenogKartona.liceZdrZastita.Content = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    pregledZdravstvenogKartona.pol.Content = p.zdravstveniKarton.PolPacijenta.ToString();
                    pregledZdravstvenogKartona.bracnoStanje.Content = p.zdravstveniKarton.BracnoStanje.ToString();
                    pregledZdravstvenogKartona.kategorijaZdravZastite.Content = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    pregledZdravstvenogKartona.radnoMjestoUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadnoMjesto;
                    pregledZdravstvenogKartona.registarskiBrojUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RegistarskiBroj;
                    pregledZdravstvenogKartona.sifraDjelatnostiUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.SifraDelatnosti;
                    pregledZdravstvenogKartona.posaoUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.PosaoKojiObavlja;
                    pregledZdravstvenogKartona.OSIZ.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.OSIZZdrZastite;
                    pregledZdravstvenogKartona.radUPosebnimUslovimaUnos.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadPodPosebnimUslovima;
                    pregledZdravstvenogKartona.promjene.Content = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.Promjene;
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
                    izmjenaZdravstvenogKartonaForma.imeLabela.Content = p.Ime;
                    izmjenaZdravstvenogKartonaForma.prezimeLabela.Content = p.Prezime;
                    izmjenaZdravstvenogKartonaForma.imeRoditelja.Text = p.zdravstveniKarton.ImeJednogRoditelja;
                    izmjenaZdravstvenogKartonaForma.JMBGLabela.Content = p.Jmbg;
                    izmjenaZdravstvenogKartonaForma.datumRodjenjaLabela.Content = p.DatumRodjenja.ToString("dd/MM/yyyy");
                    izmjenaZdravstvenogKartonaForma.telefon.Content = p.Telefon;
                    izmjenaZdravstvenogKartonaForma.adresa.Content = p.AdresaStanovanja.Drzava + ", " + p.AdresaStanovanja.Grad;
                    izmjenaZdravstvenogKartonaForma.ulicaIBrojLabela.Content = p.AdresaStanovanja.Ulica + ", " + p.AdresaStanovanja.Broj;
                    izmjenaZdravstvenogKartonaForma.liceZdrZastita.Text = p.zdravstveniKarton.LiceZaZdravstvenuZastitu;
                    izmjenaZdravstvenogKartonaForma.polUnos.Text = p.zdravstveniKarton.PolPacijenta.ToString();
                    izmjenaZdravstvenogKartonaForma.bracnoStanjeUnos.Text = p.zdravstveniKarton.BracnoStanje.ToString();
                    izmjenaZdravstvenogKartonaForma.kategorijaZdrZastiteUnos.Text = p.zdravstveniKarton.KategorijaZdravstveneZastite.ToString();
                    izmjenaZdravstvenogKartonaForma.radnoMjestoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadnoMjesto;
                    izmjenaZdravstvenogKartonaForma.registarskiBrojUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RegistarskiBroj;
                    izmjenaZdravstvenogKartonaForma.sifraDjelatnostiUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.SifraDelatnosti;
                    izmjenaZdravstvenogKartonaForma.posaoUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.PosaoKojiObavlja;
                    izmjenaZdravstvenogKartonaForma.OSIZ.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.OSIZZdrZastite;
                    izmjenaZdravstvenogKartonaForma.radUPosebnimUslovimaUnos.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadPodPosebnimUslovima;
                    izmjenaZdravstvenogKartonaForma.promjene.Text = p.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.Promjene;
                    izmjenaZdravstvenogKartonaForma.ListaAlergena.ItemsSource = p.zdravstveniKarton.Alergeni.ToList();
                    pocetna.contentControl.Content = izmjenaZdravstvenogKartonaForma.Content;
                }
                else
                {
                    MessageBox.Show("Zdravstveni karton za pacijenta nije kreiran");
                }
            }

        }

    }
}
