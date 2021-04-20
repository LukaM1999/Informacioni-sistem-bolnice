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
    public partial class IzmenaNalogaPacijentaForma : Window
    {
        public ListView listaPacijenata;
        public PregledZdravstvenogKartona pregledZdravstvenogKartona;
        public IzmenaNalogaPacijentaForma()
        {
            InitializeComponent();
            
        }

        public IzmenaNalogaPacijentaForma(ListView lp)
        {
            InitializeComponent();
            listaPacijenata = lp;
        }
        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.IzmenaNaloga(this, listaPacijenata);
            SekretarKontroler.Instance.DodjelaZdravstvenogKartonaPacijentu(this);
            this.Close();

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
                    ZdravstveniKartonForma zdravstveniKartonForma = new ZdravstveniKartonForma(listaPacijenata);
                    zdravstveniKartonForma.imeLabela.Content = this.imeUnos.Text;
                    zdravstveniKartonForma.prezimeLabela.Content = this.prezimeUnos.Text;
                    zdravstveniKartonForma.datumRodjenjaLabela.Content = this.datumUnos.Text.ToString();
                    zdravstveniKartonForma.JMBGUnos.Text = this.JMBGUnos.Text;
                    zdravstveniKartonForma.telefon.Content = this.telUnos.Text;
                    zdravstveniKartonForma.adresaLabela.Content = this.drzavaUnos.Text + ", " + this.gradUnos.Text;
                    zdravstveniKartonForma.ulicaIBrojLabela.Content = this.ulicaUnos.Text + ", " + this.brojUnos.Text;
                    
                    Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
                    zdravstveniKartonForma.ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
                    zdravstveniKartonForma.Show();


                }
            }
        }
    }
}
