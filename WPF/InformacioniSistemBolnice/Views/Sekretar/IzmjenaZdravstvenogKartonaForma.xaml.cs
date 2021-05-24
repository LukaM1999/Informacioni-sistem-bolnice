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
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    
    public partial class IzmjenaZdravstvenogKartonaForma : UserControl 
    {
        public PacijentiProzor pacijentiProzor;
        public PocetnaStranicaSekretara pocetna;
       
        public IzmjenaZdravstvenogKartonaForma(PacijentiProzor pacijentiProzor, PocetnaStranicaSekretara pocetna)
        {
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            this.pocetna = pocetna;
            AlergenRepo.Instance.Deserijalizacija();
            this.ListaAlergena.ItemsSource = ((Pacijent)pacijentiProzor.ListaPacijenata.SelectedItem).zdravstveniKarton.Alergeni;
        }

       
        private void izmjeniNalogPacijenta_Click(object sender, RoutedEventArgs e)
        {
            if (pacijentiProzor.ListaPacijenata.SelectedValue != null)
            {
                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto = new(radnoMjestoUnos.Text.ToString(),
                registarskiBrojUnos.Text.ToString(), sifraDjelatnostiUnos.Text.ToString(), posaoUnos.Text.ToString(),
                OSIZ.Text.ToString(), radUPosebnimUslovimaUnos.Text.ToString(), promjene.Text.ToString());
                ZdravstveniKartonDto zdravstveniKartonDto = new(brojKartona.Text, brojKnjizice.Text, JMBGLabela.Content.ToString(),
                    imeRoditelja.Text, liceZdrZastita.Text, (Model.Pol)Enum.Parse(typeof(Model.Pol), polUnos.Text),
                    (Model.BracnoStanje)Enum.Parse(typeof(Model.BracnoStanje), bracnoStanjeUnos.Text),
                    (Model.KategorijaZdravstveneZastite)Enum.Parse(typeof(Model.KategorijaZdravstveneZastite), kategorijaZdrZastiteUnos.Text),
                    (Alergen)ListaAlergena.SelectedItem);
                SekretarKontroler.Instance.IzmenaZdravstvenogKartona(zdravstveniKartonDto,
                    ((Pacijent)pacijentiProzor.ListaPacijenata.SelectedValue).zdravstveniKarton, podaciOZaposlenjuIZanimanjuDto);
                pocetna.contentControl.Content = new PacijentiProzor(pocetna);
            }
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
        }

        private void dodajAlergen_Click(object sender, RoutedEventArgs e)
        {
            DodajAlergenPacijentu dodajAlergenPacijentu = new DodajAlergenPacijentu(this);
            dodajAlergenPacijentu.Show();
        }

        private void obrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (ListaAlergena.SelectedItem != null)
            {
                SekretarKontroler.Instance.UklanjanjeAlergenaIzZdravstvenogKartona
                    ((Alergen)ListaAlergena.SelectedItem, JMBGLabela.Content.ToString());

                ListaAlergena.ItemsSource = PacijentRepo.Instance
                    .NadjiPoJmbg(JMBGLabela.Content.ToString()).zdravstveniKarton.Alergeni;
            }
        }
    }
}
