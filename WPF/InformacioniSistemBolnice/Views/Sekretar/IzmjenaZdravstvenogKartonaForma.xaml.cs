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

        public IzmenaZdravstvenogKartonaForma(PacijentiProzor pacijenti, PocetnaStranicaSekretara pocetnaStranica)
        {
            InitializeComponent();
            pacijentiProzor = pacijenti;
            pocetna = pocetnaStranica;
            AlergenRepo.Instance.Deserijalizacija();
            AlergeniPacijenta.ItemsSource = ((Pacijent)pacijentiProzor.ListaPacijenata.SelectedItem).zdravstveniKarton.Alergeni;
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
            return new(brojKartona.Text, brojKnjizice.Text, JMBGLabela.Content.ToString(),
                                imeRoditelja.Text, liceZdrZastita.Text, (Model.Pol)Enum.Parse(typeof(Model.Pol), polUnos.Text),
                                (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), bracnoStanjeUnos.Text),
                                (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite),
                                kategorijaZdrZastiteUnos.Text), (Alergen)AlergeniPacijenta.SelectedItem);
        }

        private PodaciOZaposlenjuIZanimanjuDto PokupiPodatkeOZanimanju()
        {
            return new(radnoMjestoUnos.Text.ToString(),
            registarskiBrojUnos.Text.ToString(), sifraDjelatnostiUnos.Text.ToString(), posaoUnos.Text.ToString(),
            OSIZ.Text.ToString(), radUPosebnimUslovimaUnos.Text.ToString(), promjene.Text.ToString());
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
        }

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
                    ((Alergen)AlergeniPacijenta.SelectedItem, JMBGLabela.Content.ToString());
                AzurirajPrikazAlergenaPacijenta();
            }
        }

        private void AzurirajPrikazAlergenaPacijenta()
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(JMBGLabela.Content.ToString());
            ZdravstveniKarton zdravstveniKarton = pacijent.zdravstveniKarton;
            AlergeniPacijenta.ItemsSource = zdravstveniKarton.Alergeni;
        }
    }
}
