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
        private ProstorijeProzor prostorProzor;
        public ProstorijaInfoForma()
        {
            InitializeComponent();
        }

        public ProstorijaInfoForma(ProstorijeProzor p)
        {
            Prostorija pr = (Prostorija)p.ListaProstorija.SelectedValue;
            prostorProzor = p;
            InitializeComponent();
            labId2.Content = pr.Id;
            labSprat2.Content = pr.Sprat;
            labTip2.Content = pr.Tip;
            if (pr.JeZauzeta)
            {
                labZauzetost.Content = "Prostorija je zauzeta";
            }
            else
            {
                labZauzetost.Content = "Prostorija nije zauzeta";
            }
            
            listaDinamicke.ItemsSource = pr.Inventar.dinamickaOprema;
            listaStaticke.ItemsSource = pr.Inventar.statickaOprema;

            cbDinamicka.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
            cbStaticka.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
        }

        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked)
            {
                DinamickaOprema oprema = (DinamickaOprema)listaDinamicke.SelectedItem;
                int kolicina = Int32.Parse(tbKolicinaDinamicka.Text);
                Prostorija prostorija = (Prostorija)prostorProzor.ListaProstorija.SelectedItem;

                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(prostorija, null, oprema, kolicina);
                //Repozitorijum.DinamickaOprema.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOprema.Instance.listaOpreme.IndexOf(dinamickaOprema)).kolicina -= kolicina;
                prostorProzor.ListaProstorija.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
                listaDinamicke.ItemsSource = Repozitorijum.Prostorije.Instance.uzmiIzabranuProstoriju(prostorija).Inventar.dinamickaOprema;
            }
            else
            {
                if(cbDinamicka.SelectedItem != (Prostorija)prostorProzor.ListaProstorija.SelectedItem){
                    DinamickaOprema oprema = (DinamickaOprema)listaDinamicke.SelectedItem;
                    int kolicina = Int32.Parse(tbKolicinaDinamicka.Text);
                    Prostorija izProstorije = (Prostorija)prostorProzor.ListaProstorija.SelectedItem;
                    Prostorija uProstorije = (Prostorija)cbDinamicka.SelectedItem;

                    UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(izProstorije, uProstorije, oprema, kolicina);

                    prostorProzor.ListaProstorija.ItemsSource = Repozitorijum.Prostorije.Instance.ListaProstorija;
                    listaDinamicke.ItemsSource = Repozitorijum.Prostorije.Instance.uzmiIzabranuProstoriju(izProstorije).Inventar.dinamickaOprema;
                }

            }
        }

        private void dugmeRaspodeliStaticku_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rbMagacin.IsChecked && listaStaticke.SelectedValue != null && datum.SelectedDate != null)
            {
                StatickaOprema oprema = (StatickaOprema)listaStaticke.SelectedItem;
                int kolicina = Int32.Parse(tbKolicinaStaticka.Text);
                Prostorija izProstorije = (Prostorija)prostorProzor.ListaProstorija.SelectedItem;

                UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(izProstorije, null, oprema, kolicina, (DateTime)datum.SelectedDate);
            }
            else
            {
                if (cbStaticka.SelectedItem != (Prostorija)prostorProzor.ListaProstorija.SelectedItem && listaStaticke.SelectedValue != null 
                    && cbStaticka.SelectedItem != null && datum.SelectedDate != null)
                {
                    StatickaOprema oprema = (StatickaOprema)listaStaticke.SelectedItem;
                    int kolicina = Int32.Parse(tbKolicinaStaticka.Text);
                    Prostorija izProstorije = (Prostorija)prostorProzor.ListaProstorija.SelectedItem;
                    Prostorija uProstoriju = (Prostorija)cbStaticka.SelectedItem;

                    UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(izProstorije, uProstoriju, oprema, kolicina, (DateTime)datum.SelectedDate);
                }

            }
        }
    }
}
