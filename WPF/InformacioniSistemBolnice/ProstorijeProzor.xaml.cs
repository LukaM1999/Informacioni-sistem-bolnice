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
using Servis;
using Repozitorijum;
using Kontroler;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijeProzor : Window
    {
        private ObservableCollection<Prostorija> listaProstorija
        {
            get;
            set;
        }
        public ProstorijeProzor()
        {
            InitializeComponent();
            Prostorije.Instance.Deserijalizacija();
            listaProstorija = Prostorije.Instance.listaProstorija;

            ListaProstorija.ItemsSource = listaProstorija;
            cbStaticka.ItemsSource = Enum.GetValues(typeof(TipStatickeOpreme));
            cbDinamicka.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            ProstorijaForma pf = new ProstorijaForma(ListaProstorija);
            pf.Show();
        }

        private void Obrisi(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.UklanjanjeProstorije(ListaProstorija);
        }

        private void izmeniProstorijuDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                ProstorijaFormaIzmeni pf = new ProstorijaFormaIzmeni(ListaProstorija);
                Prostorija pr = (Prostorija)ListaProstorija.SelectedValue;
                pf.SetTextBoxValue(pr);
                pf.Show();
                ListaProstorija.ItemsSource = listaProstorija;
            }
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                UpravnikKontroler.Instance.PregledProstorije(this);
            }

        }

        private void magacinDugme_Click(object sender, RoutedEventArgs e)
        {
            Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
            MagacinProzor mp = new MagacinProzor(this);
            mp.Show();
        }

        private void lekoviDugme_Click(object sender, RoutedEventArgs e)
        {
            LekProzor prozor = new LekProzor();
            prozor.Show();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Prostorija> novaListaProstorija = new ObservableCollection<Prostorija>();
            int kolicina = int.Parse(tbKolicina.Text);
            TipDinamickeOpreme tipDinamickeOpreme = (TipDinamickeOpreme)cbDinamicka.SelectedItem;
            TipStatickeOpreme tipStatickeOpreme = (TipStatickeOpreme)cbDinamicka.SelectedItem;

            if ((bool)rbStaticka.IsChecked)
            {
                foreach(Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
                {
                    foreach(Model.StatickaOprema statickaOprema in prostorija.inventar.statickaOprema.ToList())
                    {
                        if (statickaOprema.tip.Equals(tipStatickeOpreme) && statickaOprema.kolicina >= kolicina)
                        {
                            novaListaProstorija.Add(prostorija);
                        }
                    }
                }
            }
            else
            {
                foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija.ToList())
                {
                    foreach (Model.DinamickaOprema dinamickaOprema in prostorija.inventar.dinamickaOprema.ToList())
                    {
                        if (dinamickaOprema.tip.Equals(tipDinamickeOpreme) && dinamickaOprema.kolicina >= kolicina)
                        {
                            novaListaProstorija.Add(prostorija);
                        }
                    }
                }
            }
            listaProstorija = novaListaProstorija;
            ListaProstorija.ItemsSource = listaProstorija;
        }

        private void btnOsvezi_Click(object sender, RoutedEventArgs e)
        {
            listaProstorija = Prostorije.Instance.listaProstorija;
            ListaProstorija.ItemsSource = listaProstorija;
        }
    }
}
