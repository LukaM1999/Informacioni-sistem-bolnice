using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaZdravstvenogKartonaForma : UserControl
    {
        public PacijentiProzor pacijentiProzor;
        public PocetnaStranicaSekretara pocetna;
        public ZdravstveniKarton ZdravstveniKarton { get; set; }
        public Pacijent Pacijent { get; set; }

        public IzmenaZdravstvenogKartonaForma(PacijentiProzor pacijenti, 
            PocetnaStranicaSekretara pocetnaStranica, Pacijent izabraniPacijent)
        {
            ZdravstveniKarton = izabraniPacijent.zdravstveniKarton;
            Pacijent = izabraniPacijent;
            InitializeComponent();
            pacijentiProzor = pacijenti;
            pocetna = pocetnaStranica;
            AlergenRepo.Instance.Deserijalizacija();
        }

        private void IzmeniZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton zdravstveniKartonZaIzmenu = UzmiZdravstveniKartonPacijenta();
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto = PokupiPodatkeOZanimanju();
            ZdravstveniKartonDto zdravstveniKartonDto = PokupiPodatkeZdravstvenogKartona();
            SekretarKontroler.Instance.IzmenaZdravstvenogKartona(zdravstveniKartonDto, zdravstveniKartonZaIzmenu,
                podaciOZaposlenjuIZanimanjuDto);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private ZdravstveniKarton UzmiZdravstveniKartonPacijenta()
        {
            if (pacijentiProzor.ListaPacijenata.SelectedValue is not null)
            {
                Pacijent pacijent = ((Pacijent)pacijentiProzor.ListaPacijenata.SelectedValue); 
                return pacijent.zdravstveniKarton;
            }
            return null;
        }

        private ZdravstveniKartonDto PokupiPodatkeZdravstvenogKartona()
        {
            return new(ZdravstveniKarton.BrojKartona, ZdravstveniKarton.BrojKnjizice, ZdravstveniKarton.Jmbg,
                       ZdravstveniKarton.ImeJednogRoditelja, ZdravstveniKarton.LiceZaZdravstvenuZastitu, 
                       ZdravstveniKarton.PolPacijenta, ZdravstveniKarton.BracnoStanje, ZdravstveniKarton.KategorijaZdravstveneZastite,
                       (Alergen)AlergeniPacijenta.SelectedItem);
        }

        private PodaciOZaposlenjuIZanimanjuDto PokupiPodatkeOZanimanju()
        {
            return new(radnoMjestoUnos.Text.ToString(),
            registarskiBrojUnos.Text.ToString(), sifraDjelatnostiUnos.Text.ToString(), posaoUnos.Text.ToString(),
            OSIZ.Text.ToString(), radUPosebnimUslovimaUnos.Text.ToString(), promjene.Text.ToString());
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e) 
            => this.pocetna.contentControl.Content = this.pacijentiProzor.Content;

        private void DodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            DodajAlergenPacijentu dodajAlergenPacijentu = new DodajAlergenPacijentu(this);
            dodajAlergenPacijentu.Show();
        }

        private void ObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (AlergeniPacijenta.SelectedItem != null)
            {
                SekretarKontroler.Instance.UklanjanjeAlergenaIzZdravstvenogKartona
                    ((Alergen)AlergeniPacijenta.SelectedItem, ZdravstveniKarton.Jmbg);
                AzurirajPrikazAlergenaPacijenta();
            }
        }

        private void AzurirajPrikazAlergenaPacijenta()
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(Pacijent.Jmbg);
            ZdravstveniKarton zdravstveniKarton = pacijent.zdravstveniKarton;
            AlergeniPacijenta.ItemsSource = zdravstveniKarton.Alergeni;
        }
    }
}
