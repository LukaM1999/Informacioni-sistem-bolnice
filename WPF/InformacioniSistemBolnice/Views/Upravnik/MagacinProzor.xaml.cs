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
using Repozitorijum;
using Kontroler;
using Model;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MagacinProzor.xaml
    /// </summary>
    public partial class MagacinProzor : Window
    {
        private ProstorijeProzor prostorijaProzor;

        public MagacinProzor() { }
        public MagacinProzor(ProstorijeProzor p)
        {
            InitializeComponent();
            prostorijaProzor = p;
            Repozitorijum.StatickaOprema.Instance.Deserijalizacija();
            Repozitorijum.DinamickaOprema.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            listViewStatOpreme.ItemsSource = Repozitorijum.StatickaOprema.Instance.listaOpreme;
            listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOprema.Instance.listaOpreme;
            listaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
            listaProstorijaS.ItemsSource = Prostorije.Instance.listaProstorija;
        }

        private void dugmeKreirajOpemu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajProzor p = new MagacinDodajProzor(listViewStatOpreme);
            p.Show();
        }

        private void dugmeObrisiStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.BrisanjeStatickeOpreme(this);
        }

        private void dugmeIzmeniStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                MagacinIzmeniProzor p = new MagacinIzmeniProzor(listViewStatOpreme);
                p.postavljanjeVrednost();
                p.Show();
            }
        }

        private void dugmeKreirajDinamickuOpremu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajDinamickuOpremu p = new MagacinDodajDinamickuOpremu(listViewDinamOpreme);
            p.Show();
        }

        private void dugmeObrisiDinamOpremu_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.BrisanjeDinamickeOpreme(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null)
            {
                MagacinIzmeniDinamickuOpremu p = new MagacinIzmeniDinamickuOpremu(listViewDinamOpreme);
                p.postavljanjeVrednost();
                p.Show();
            }
        }

        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null && listaProstorija.SelectedValue != null)
            {
                Model.DinamickaOprema oprema = (Model.DinamickaOprema)listViewDinamOpreme.SelectedValue;
                int kolicina = Int32.Parse(tbKolDin.Text);
                Prostorija prostorija = (Prostorija)listaProstorija.SelectedItem;
                
                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(null, prostorija, oprema, kolicina);
                listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOprema.Instance.listaOpreme;
                prostorijaProzor.ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
                prostorijaProzor.ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
                
            }


        }

        private void dugmeRaspodeliStaticku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null && listaProstorijaS.SelectedValue != null && datumStat.SelectedDate != null)
            {
                Model.StatickaOprema oprema = (Model.StatickaOprema)listViewStatOpreme.SelectedItem;
                int kolicina = Int32.Parse(tbKolStat.Text);
                Prostorija prostorija = (Prostorija)listaProstorijaS.SelectedItem;
                
                UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(null, prostorija, oprema, kolicina, (DateTime)datumStat.SelectedDate);
                listViewStatOpreme.ItemsSource = Repozitorijum.StatickaOprema.Instance.listaOpreme;
                prostorijaProzor.ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
            }
        }
    }
}
