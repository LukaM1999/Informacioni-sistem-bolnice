using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaNalogaPacijentaForma : UserControl
    {
        public ListView listaPacijenata;
        public PregledZdravstvenogKartona pregledZdravstvenogKartona;
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;

        public IzmenaNalogaPacijentaForma(PacijentiProzor pacijentiProzor)
        {
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            listaPacijenata = pacijentiProzor.ListaPacijenata;
            pocetna = pacijentiProzor.pocetna;
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedValue != null)
            {
                PacijentDto pacijentDto = PokupiPodatkeSaForme();
                SekretarKontroler.Instance.IzmenaNaloga(pacijentDto, (Pacijent)listaPacijenata.SelectedItem);
                SekretarKontroler.Instance.DodelaZdravstvenogKartonaPacijentu();
                pocetna.contentControl.Content = new PacijentiProzor(pocetna);
            }
        }

        private PacijentDto PokupiPodatkeSaForme()
        {
            return new PacijentDto(this.imeUnos.Text, this.prezimeUnos.Text, this.JMBGUnos.Text,
                                    DateTime.Parse(this.datumUnos.Text), this.telUnos.Text,
                                    this.mailUnos.Text, this.korisnikUnos.Text, this.lozinkaUnos.Password,
                                    this.drzavaUnos.Text, this.gradUnos.Text, this.ulicaUnos.Text,
                                    this.brojUnos.Text);
        }

        private void zdravstveniKartonDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedItem is null)
            {
                Pacijent p = (Pacijent)listaPacijenata.SelectedItem;
                if (p.zdravstveniKarton is null) 
                {
                    ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma(pocetna, this);
                    PrepisiPodatkeNaZdravsteniKarton(zdravstveniKartonForma);
                    pocetna.contentControl.Content = zdravstveniKartonForma.Content;
                }
                else MessageBox.Show("Pacijent vec ima kreiran zdravstveni karton");
            }
        }

        private void PrepisiPodatkeNaZdravsteniKarton(ZdravstveniKartonForma zdravstveniKartonForma)
        {
            zdravstveniKartonForma.imeLabela.Content = imeUnos.Text;
            zdravstveniKartonForma.prezimeLabela.Content = prezimeUnos.Text;
            zdravstveniKartonForma.datumRodjenjaLabela.Content = datumUnos.Text.ToString();
            zdravstveniKartonForma.JMBGUnos.Text = JMBGUnos.Text;
            zdravstveniKartonForma.telefon.Content = telUnos.Text;
            zdravstveniKartonForma.adresaLabela.Content = drzavaUnos.Text + ", " + gradUnos.Text;
            zdravstveniKartonForma.ulicaIBrojLabela.Content = ulicaUnos.Text + ", " + brojUnos.Text;
            AlergenRepo.Instance.Deserijalizacija();
            zdravstveniKartonForma.ListaAlergena.ItemsSource = AlergenRepo.Instance.Alergeni;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
        }

    }
}
