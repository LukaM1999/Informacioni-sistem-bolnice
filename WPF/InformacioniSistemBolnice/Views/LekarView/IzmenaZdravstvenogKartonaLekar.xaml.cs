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
using InformacioniSistemBolnice.Views;
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmenaZdravstvenogKartonaLekar : Window
    {
        public Pacijent pacijent;
        public GlavniProzorLekara glavniProzorLekara;

        public IzmenaZdravstvenogKartonaLekar(Pacijent pacijent, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            PopunjavaKarton(pacijent);
            glavniProzorLekara = glavni;
        }

        public IzmenaZdravstvenogKartonaLekar(Termin termin, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (pacijent.Jmbg.Equals(termin.PacijentJmbg))
                {
                    PopunjavaKarton(pacijent);

                }
            }
            glavniProzorLekara = glavni;
        }

        private void PopunjavaKarton(Pacijent pacijent)
        {
            AlergenRepo.Instance.Deserijalizacija();
            ZdravstveniKartonRepo.Instance.Deserijalizacija();
            this.pacijent = pacijent;
            this.brojKartonaLabela.Content = pacijent.zdravstveniKarton.BrojKartona;
            this.brojKnjiziceLabela.Content = pacijent.zdravstveniKarton.BrojKnjizice;
            this.jmbgLabela.Content = pacijent.zdravstveniKarton.Jmbg;
            this.prezimeLabela.Content = pacijent.Prezime;
            this.roditeljLabela.Content = pacijent.zdravstveniKarton.ImeJednogRoditelja;
            this.imeLabela.Content = pacijent.Ime;
            this.datumRodjenjaLabela.Content = pacijent.DatumRodjenja;
            this.adresaLabela.Content = pacijent.AdresaStanovanja.Grad + ", " + pacijent.AdresaStanovanja.Drzava;
            this.ulicaIBrojLabela.Content = pacijent.AdresaStanovanja.Ulica + " " + pacijent.AdresaStanovanja.Broj;
            this.telefonLabela.Content = pacijent.Telefon;
            this.polLabela.Content = pacijent.zdravstveniKarton.PolPacijenta;
            this.bracnoStanjeLabela.Content = pacijent.zdravstveniKarton.BracnoStanje;
            this.zdravstvenaZastitaLabela.Content = pacijent.zdravstveniKarton.LiceZaZdravstvenuZastitu;
            this.kategorijaZastiteLabela.Content = pacijent.zdravstveniKarton.KategorijaZdravstveneZastite;
            this.mestoRadaLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadnoMjesto;
            this.registarskiBrLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RegistarskiBroj;
            this.sifraDelatnostiLabela.Content =
                pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.SifraDelatnosti;
            this.posaoLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.PosaoKojiObavlja;
            this.OSIZLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.OSIZZdrZastite;
            this.posebniUsloviLabela.Content =
                pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.RadPodPosebnimUslovima;
            this.promeneLabela.Content = pacijent.zdravstveniKarton.PodaciOZaposlenjuIZanimanju.Promjene;
            this.ListaAlergena.ItemsSource = pacijent.zdravstveniKarton.Alergeni;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UCListaRecepata recepti = new UCListaRecepata(pacijent, glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = recepti;
        }

        private void Uput_Click(object sender, RoutedEventArgs e)
        {
            UputForma uputForma = new(pacijent);
            uputForma.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UCBolnickoLecenjeForma forma = new(pacijent, glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = forma;
        }
    }
}
