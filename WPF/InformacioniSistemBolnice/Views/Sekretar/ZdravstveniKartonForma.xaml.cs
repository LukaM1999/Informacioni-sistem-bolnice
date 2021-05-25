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
       
        public ZdravstveniKartonForma(PocetnaStranicaSekretara pocetnaStranicaSekretara, 
                                        IzmenaNalogaPacijentaForma izmenaNalogaForma)
        {
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
            return new(brojKartonaUnos.Text, brojKartonaUnos.Text, JMBGUnos.Text,
                       imeRoditeljaUnos.Text, liceZdrZastitaUnos.Text, (Model.Pol)Enum.Parse(typeof(Model.Pol), polUnos.Text),
                      (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), bracnoStanjeUnos.Text),
                      (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite),
                      kategorijaZdrZastiteUnos.Text),
                      (Alergen)ListaAlergena.SelectedItem);
        }

        private PodaciOZaposlenjuIZanimanjuDto PokupiPodatkeOZaposlenju()
        {
            return new(radnoMjestoUnos.Text.ToString(),
                            registarskiBrojUnos.Text.ToString(), sifraDjelatnostiUnos.Text.ToString(), posaoUnos.Text.ToString(),
                            OSIZ.Text.ToString(), radUPosebnimUslovimaUnos.Text.ToString(), promjene.Text.ToString());
        }
    }
}
