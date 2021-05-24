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
using StatickaOprema = Model.StatickaOprema;
using DinamickaOprema = Model.DinamickaOprema;

namespace InformacioniSistemBolnice
{
    public partial class ProstorijeProzor : Window
    {
        public int kolicina;
        private ObservableCollection<Prostorija> listaProstorija { get; set; }
        public ProstorijeProzor()
        {
            InitializeComponent();
            Prostorije.Instance.Deserijalizacija();
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            StatickaOpremaRepo.Instance.Deserijalizacija();
            listaProstorija = Prostorije.Instance.ListaProstorija;

            ListaProstorija.ItemsSource = listaProstorija;
            cbStaticka.ItemsSource = Enum.GetValues(typeof(TipStatickeOpreme));
            cbDinamicka.ItemsSource = Enum.GetValues(typeof(TipDinamickeOpreme));
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Dodaj(object sender, RoutedEventArgs e)
        {
            ProstorijaForma prozor = new ProstorijaForma(ListaProstorija);
            prozor.Show();
        }
        private void Obrisi(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                Prostorija izabranaProstorija = (Prostorija)ListaProstorija.SelectedValue;
                UpravnikKontroler.Instance.UklanjanjeProstorije(new ProstorijaDto() { Id = izabranaProstorija.Id });
                listaProstorija = Prostorije.Instance.ListaProstorija;
                ListaProstorija.ItemsSource = listaProstorija;
            }
        }
        private void izmeniProstorijuDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                ProstorijaFormaIzmeni prozor = new ProstorijaFormaIzmeni(ListaProstorija);
                prozor.Show();
            }
        }
        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                ProstorijaInfoForma prozor = new(ListaProstorija);
                prozor.Show();
            }

        }
        private void magacinDugme_Click(object sender, RoutedEventArgs e)
        {
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            MagacinProzor prozor = new MagacinProzor(ListaProstorija);
            prozor.Show();
        }
        private void lekoviDugme_Click(object sender, RoutedEventArgs e)
        {
            LekProzor prozor = new LekProzor();
            prozor.Show();
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Prostorija> novaListaProstorija = new ObservableCollection<Prostorija>();
            if (!string.IsNullOrWhiteSpace(tbKolicina.Text) && tbKolicina.Text.All(char.IsDigit))
            {
                kolicina = int.Parse(tbKolicina.Text);
            }
            if ((bool)rbStaticka.IsChecked)
            {
                TipStatickeOpreme tipStatickeOpreme = (TipStatickeOpreme)cbStaticka.SelectedItem;
                foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija.ToList())
                {
                    foreach(StatickaOprema statickaOprema in prostorija.Inventar.StatickaOprema.ToList())
                    {
                        if (statickaOprema.Tip.Equals(tipStatickeOpreme) && statickaOprema.Kolicina >= kolicina)
                        {
                            novaListaProstorija.Add(prostorija);
                        }
                    }
                }
            }
            else
            {
                TipDinamickeOpreme tipDinamickeOpreme = (TipDinamickeOpreme)cbDinamicka.SelectedItem;
                foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija.ToList())
                {
                    foreach (DinamickaOprema dinamickaOprema in prostorija.Inventar.DinamickaOprema.ToList())
                    {
                        if (dinamickaOprema.Tip.Equals(tipDinamickeOpreme) && dinamickaOprema.Kolicina >= kolicina)
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
            listaProstorija = Prostorije.Instance.ListaProstorija;
            ListaProstorija.ItemsSource = listaProstorija;
        }

        private void btnRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaRenoviranjeProzor prozor = new ProstorijaRenoviranjeProzor(ListaProstorija);
            prozor.Show();
        }
    }
}
