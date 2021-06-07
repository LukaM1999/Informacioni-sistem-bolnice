using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaNalogaPacijentaForma : UserControl
    {
        public ListView listaPacijenata;
        public PregledZdravstvenogKartona pregledZdravstvenogKartona;
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        public Pacijent Pacijent { get; set; }

        public IzmenaNalogaPacijentaForma(PacijentiProzor pacijentiProzor, Pacijent izabraniPacijent)
        {
            Pacijent = izabraniPacijent;
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            listaPacijenata = pacijentiProzor.ListaPacijenata;
            pocetna = pacijentiProzor.pocetna;
        }

        private void PotvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentDto pacijentDto = PokupiPodatkeSaForme();
            NalogPacijentaKontroler.Instance.IzmenaNaloga(pacijentDto, (Pacijent)listaPacijenata.SelectedItem);
            ZdravstveniKartonKontroler.Instance.DodelaZdravstvenogKartonaPacijentu();
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private PacijentDto PokupiPodatkeSaForme()
        {
            Korisnik korisnik = Pacijent.Korisnik;
            Adresa adresa = Pacijent.AdresaStanovanja;
            return new PacijentDto(Pacijent.Ime, Pacijent.Prezime, Pacijent.Jmbg, Pacijent.DatumRodjenja, Pacijent.Telefon,
                                    Pacijent.Email, korisnik.KorisnickoIme, korisnik.Lozinka,
                                    adresa.Drzava, adresa.Grad, adresa.Ulica, adresa.Broj);
        }

        private void ZdravstveniKartonDugme_Click(object sender, RoutedEventArgs e)
        {
            if (Pacijent.zdravstveniKarton is null)
            {
                pocetna.contentControl.Content = new ZdravstveniKartonForma(pocetna, this, Pacijent);
            }
            else MessageBox.Show("Pacijent vec ima kreiran zdravstveni karton");
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
    }
}
