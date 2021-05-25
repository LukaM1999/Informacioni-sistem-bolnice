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

        private void kreirajZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto = new (radnoMjestoUnos.Text.ToString(), 
                registarskiBrojUnos.Text.ToString(), sifraDjelatnostiUnos.Text.ToString(), posaoUnos.Text.ToString(),
                OSIZ.Text.ToString(), radUPosebnimUslovimaUnos.Text.ToString(), promjene.Text.ToString());
            ZdravstveniKartonDto zdravstveniKartonDto = new(brojKartonaUnos.Text, brojKartonaUnos.Text, JMBGUnos.Text,
                imeRoditeljaUnos.Text, liceZdrZastitaUnos.Text, (Model.Pol)Enum.Parse(typeof(Model.Pol), polUnos.Text),
                (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), bracnoStanjeUnos.Text),
                (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite), kategorijaZdrZastiteUnos.Text),
                (Alergen)ListaAlergena.SelectedItem);
            SekretarKontroler.Instance.KreiranjeZdravstvenogKartona(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto);
            pocetna.contentControl.Content = izmenaNalogaPacijentaForma;
            
        }
    }
}
