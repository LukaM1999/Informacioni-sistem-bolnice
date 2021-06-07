using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Repozitorijum;
using Model;

namespace InformacioniSistemBolnice
{
    public partial class ZdravstveniKartonForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public IzmenaNalogaPacijentaForma izmenaNalogaPacijentaForma;
        public Pacijent Pacijent { get; set; }
        public ZdravstveniKarton ZdravstveniKarton { get; set; }

        public ZdravstveniKartonForma(PocetnaStranicaSekretara pocetnaStranicaSekretara,
                                      IzmenaNalogaPacijentaForma izmenaNalogaForma, Pacijent izabraniPacijent)
        {
            Pacijent = izabraniPacijent;
            ZdravstveniKarton = izabraniPacijent.zdravstveniKarton;
            InitializeComponent();
            AlergenRepo.Instance.Deserijalizacija();
            ListaAlergena.ItemsSource = AlergenRepo.Instance.Alergeni;
            pocetna = pocetnaStranicaSekretara;
            izmenaNalogaPacijentaForma = izmenaNalogaForma;
        }

        private void KreirajZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto = PokupiPodatkeOZaposlenju();
            ZdravstveniKartonDto zdravstveniKartonDto = PokupiPodatkeZdravstvenogKartona();
            SekretarKontroler.Instance.KreiranjeZdravstvenogKartona(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto);
            pocetna.contentControl.Content = izmenaNalogaPacijentaForma;
        }

        private ZdravstveniKartonDto PokupiPodatkeZdravstvenogKartona()
        {
            return new(brojKartonaUnos.Text,brojKnjiziceUnos.Text, JMBGUnos.Text, imeRoditeljaUnos.Text, liceZdrZastitaUnos.Text,
                       (Pol)Enum.Parse(typeof(Pol), polUnos.SelectedItem.ToString()),
                       (BracnoStanje)Enum.Parse(typeof(BracnoStanje), bracnoStanjeUnos.SelectedItem.ToString()),
                       (KategorijaZdravstveneZastite)Enum.Parse(typeof(KategorijaZdravstveneZastite), 
                       kategorijaZdrZastiteUnos.SelectedItem.ToString()), (Alergen)ListaAlergena.SelectedItem);
        }





        private PodaciOZaposlenjuIZanimanjuDto PokupiPodatkeOZaposlenju()
        {
            return new(radnoMjestoUnos.Text, registarskiBrojUnos.Text, sifraDjelatnostiUnos.Text,
                        posaoUnos.Text, OSIZ.Text, radUPosebnimUslovimaUnos.Text, promjene.Text);
        }
    }
}
