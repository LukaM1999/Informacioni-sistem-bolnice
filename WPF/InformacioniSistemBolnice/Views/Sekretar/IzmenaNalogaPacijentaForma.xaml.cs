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
using Model;
using Repozitorijum;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaNalogaPacijentaForma : UserControl
    {
        public ListView listaPacijenata;
        public PregledZdravstvenogKartona pregledZdravstvenogKartona;
        public PocetnaStranicaSekretara pocetna;
        public PacijentiProzor pacijentiProzor;
        

        public IzmenaNalogaPacijentaForma(PacijentiProzor pacijentiProzor, PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            this.pacijentiProzor = pacijentiProzor;
            listaPacijenata = pacijentiProzor.ListaPacijenata;
            this.pocetna = pocetnaStranicaSekretara;
        }
        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedValue != null)
            {
               PacijentDto pacijentDto = new PacijentDto(this.imeUnos.Text, this.prezimeUnos.Text, this.JMBGUnos.Text,
                                                        DateTime.Parse(this.datumUnos.Text), this.telUnos.Text, this.mailUnos.Text,
                                                        this.korisnikUnos.Text, this.lozinkaUnos.Password,
                                                         this.drzavaUnos.Text, this.gradUnos.Text, this.ulicaUnos.Text, this.brojUnos.Text);
                SekretarKontroler.Instance.IzmenaNaloga(pacijentDto, (Pacijent)listaPacijenata.SelectedItem);
                SekretarKontroler.Instance.DodjelaZdravstvenogKartonaPacijentu(this);
                pocetna.contentControl.Content = new PacijentiProzor(pocetna);
            }
        }

      

        private void zdravstveniKartonDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaPacijenata.SelectedValue != null)
            {
                Pacijent p = (Pacijent)listaPacijenata.SelectedItem;
                if (p.zdravstveniKarton != null)
                {
                    MessageBox.Show("Pacijent vec ima kreiran zdravstveni karton");
                }
                else {
                    ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma(listaPacijenata, pocetna, this);
                    zdravstveniKartonForma.imeLabela.Content = this.imeUnos.Text;
                    zdravstveniKartonForma.prezimeLabela.Content = this.prezimeUnos.Text;
                    zdravstveniKartonForma.datumRodjenjaLabela.Content = this.datumUnos.Text.ToString();
                    zdravstveniKartonForma.JMBGUnos.Text = this.JMBGUnos.Text;
                    zdravstveniKartonForma.telefon.Content = this.telUnos.Text;
                    zdravstveniKartonForma.adresaLabela.Content = this.drzavaUnos.Text + ", " + this.gradUnos.Text;
                    zdravstveniKartonForma.ulicaIBrojLabela.Content = this.ulicaUnos.Text + ", " + this.brojUnos.Text;
                    
                    Alergeni.Instance.Deserijalizacija();
                    zdravstveniKartonForma.ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
                    pocetna.contentControl.Content = zdravstveniKartonForma.Content;


                }
            }
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.pacijentiProzor.Content;
        }

    }
}
