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
using Kontroler;
using System.Collections.ObjectModel;
using DinamickaOprema = Model.DinamickaOprema;
using StatickaOprema = Model.StatickaOprema;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijaInfoForma : Window
    {
        private DataGrid ListaProstorija;
        public ProstorijaInfoForma()
        {
            InitializeComponent();
        }

        public ProstorijaInfoForma(DataGrid listaProstorija)
        {
            InitializeComponent();
            ListaProstorija = listaProstorija;
            Prostorija izabranaProstorija = (Prostorija)ListaProstorija.SelectedValue;
            PostaviTekstLabelama(izabranaProstorija);
            
            listaDinamicke.ItemsSource = izabranaProstorija.Inventar.dinamickaOprema;
            listaStaticke.ItemsSource = izabranaProstorija.Inventar.statickaOprema;

            cbDinamicka.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
            cbStaticka.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
        }
        private void PostaviTekstLabelama(Prostorija izabranaProstorija)
        {
            labId2.Content = izabranaProstorija.Id;
            labSprat2.Content = izabranaProstorija.Sprat;
            labTip2.Content = izabranaProstorija.Tip;
            if (izabranaProstorija.JeZauzeta)
            {
                labZauzetost.Content = "Prostorija je zauzeta";
            }
            else
            {
                labZauzetost.Content = "Prostorija nije zauzeta";
            }
        }
        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked)
            {
                DinamickaOprema oprema = (DinamickaOprema)listaDinamicke.SelectedItem;
                int kolicina = Int32.Parse(tbKolicinaDinamicka.Text);
                Prostorija prostorija = (Prostorija)ListaProstorija.SelectedItem;

                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(prostorija, null, oprema, kolicina);
                ListaProstorija.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
                listaDinamicke.ItemsSource = Repozitorijum.Prostorije.Instance.UzmiIzabranuProstoriju(prostorija).Inventar.dinamickaOprema;
            }
            else
            {
                if(cbDinamicka.SelectedItem != (Prostorija)ListaProstorija.SelectedItem){
                    DinamickaOprema oprema = (DinamickaOprema)listaDinamicke.SelectedItem;
                    int kolicina = Int32.Parse(tbKolicinaDinamicka.Text);
                    Prostorija izProstorije = (Prostorija)ListaProstorija.SelectedItem;
                    Prostorija uProstorije = (Prostorija)cbDinamicka.SelectedItem;

                    UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(izProstorije, uProstorije, oprema, kolicina);

                    ListaProstorija.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
                    listaDinamicke.ItemsSource = Repozitorijum.Prostorije.Instance.UzmiIzabranuProstoriju(izProstorije).Inventar.dinamickaOprema;
                }

            }
        }

        private void dugmeRaspodeliStaticku_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked && listaStaticke.SelectedValue != null && datum.SelectedDate != null)
            {
                StatickaOprema oprema = (StatickaOprema)listaStaticke.SelectedItem;
                int kolicina = Int32.Parse(tbKolicinaStaticka.Text);
                Prostorija izProstorije = (Prostorija)ListaProstorija.SelectedItem;

                UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(izProstorije, null, oprema, kolicina, (DateTime)datum.SelectedDate);
            }
            else
            {
                if (cbStaticka.SelectedItem != (Prostorija)ListaProstorija.SelectedItem && listaStaticke.SelectedValue != null 
                    && cbStaticka.SelectedItem != null && datum.SelectedDate != null)
                {
                    StatickaOprema oprema = (StatickaOprema)listaStaticke.SelectedItem;
                    int kolicina = Int32.Parse(tbKolicinaStaticka.Text);
                    Prostorija izProstorije = (Prostorija)ListaProstorija.SelectedItem;
                    Prostorija uProstoriju = (Prostorija)cbStaticka.SelectedItem;

                    UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(izProstorije, uProstoriju, oprema, kolicina, (DateTime)datum.SelectedDate);
                }

            }
        }
    }
}
