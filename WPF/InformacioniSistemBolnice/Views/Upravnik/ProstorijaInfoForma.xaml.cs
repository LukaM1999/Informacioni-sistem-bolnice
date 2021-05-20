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
        private Prostorija izProstorije { get; set; }
        public ProstorijaInfoForma()
        {
            InitializeComponent();
        }

        public ProstorijaInfoForma(DataGrid listaProstorija)
        {
            InitializeComponent();
            ListaProstorija = listaProstorija;
            izProstorije = (Prostorija)ListaProstorija.SelectedItem;
            Prostorija izabranaProstorija = (Prostorija)ListaProstorija.SelectedItem;
            PostaviTekstLabelama(izabranaProstorija);
            
            listaDinamicke.ItemsSource = izabranaProstorija.Inventar.DinamickaOprema;
            listaStaticke.ItemsSource = izabranaProstorija.Inventar.StatickaOprema;

            cbDinamicka.ItemsSource = Prostorije.Instance.ListaProstorija;
            cbStaticka.ItemsSource = Prostorije.Instance.ListaProstorija;
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
            Prostorija uProstoriju = (Prostorija)cbDinamicka.SelectedItem;
            if ((bool)rbMagacin.IsChecked)
            {
                RaspodelaDinamickeOpremeDto dtoRapsodelaUMagacin = new(izProstorije.Id, null,
                    (DinamickaOprema)listaDinamicke.SelectedItem, Int32.Parse(tbKolicinaDinamicka.Text));
                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(dtoRapsodelaUMagacin);
            }
            else
            {
                if(cbDinamicka.SelectedItem != (Prostorija)ListaProstorija.SelectedItem){
                    RaspodelaDinamickeOpremeDto dtoRaspodelaUDruguProstoriju = new(izProstorije.Id, uProstoriju.Id,
                        (DinamickaOprema)listaDinamicke.SelectedItem, Int32.Parse(tbKolicinaDinamicka.Text));
                    UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(dtoRaspodelaUDruguProstoriju);
                }
            }
            ListaProstorija.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
            listaDinamicke.ItemsSource = Repozitorijum.Prostorije.Instance.NadjiPoId(izProstorije.Id).Inventar.DinamickaOprema;
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
