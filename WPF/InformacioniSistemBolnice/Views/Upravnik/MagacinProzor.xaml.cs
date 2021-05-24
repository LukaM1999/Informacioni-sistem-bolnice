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
using DinamickaOprema = Model.DinamickaOprema;
using StatickaOprema = Model.StatickaOprema;


namespace InformacioniSistemBolnice
{
    public partial class MagacinProzor : Window
    {
        private DataGrid ListaProstorija;
        public MagacinProzor() { }
        public MagacinProzor(DataGrid listaProstorija)
        {
            InitializeComponent();
            ListaProstorija = listaProstorija;
            StatickaOpremaRepo.Instance.Deserijalizacija();
            DinamickaOpremaRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            listViewStatOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
            listViewDinamOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
            this.listaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            listaProstorijaS.ItemsSource = ProstorijaRepo.Instance.Prostorije;
        }
        private void dugmeKreirajOpemu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajProzor prozor = new MagacinDodajProzor(listViewStatOpreme);
            prozor.Show();
        }
        private void dugmeObrisiStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                StatickaOprema oprema = (StatickaOprema)listViewStatOpreme.SelectedItem;
                UpravnikKontroler.Instance.BrisanjeStatickeOpreme(new(oprema.Kolicina, oprema.Tip));
                listViewStatOpreme.ItemsSource = StatickaOpremaRepo.Instance.StatickaOprema;
            }
        }
        private void dugmeIzmeniStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                MagacinIzmeniProzor prozor = new MagacinIzmeniProzor(listViewStatOpreme);
                prozor.Show();
            }
        }
        private void dugmeKreirajDinamickuOpremu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajDinamickuOpremu prozor = new MagacinDodajDinamickuOpremu(listViewDinamOpreme);
            prozor.Show();
        }
        private void dugmeObrisiDinamOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null)
            {
                DinamickaOprema oprema = (DinamickaOprema)listViewDinamOpreme.SelectedItem;
                UpravnikKontroler.Instance.BrisanjeDinamickeOpreme(new(oprema.Kolicina, oprema.Tip));
                listViewDinamOpreme.ItemsSource = DinamickaOpremaRepo.Instance.DinamickaOprema;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null)
            {
                MagacinIzmeniDinamickuOpremu prozor = new MagacinIzmeniDinamickuOpremu(listViewDinamOpreme);
                prozor.Show();
            }
        }
        private void dugmeRaspodeliDinamicku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewDinamOpreme.SelectedValue != null && listaProstorija.SelectedValue != null)
            {
                Prostorija uProstoriju = (Prostorija)listaProstorija.SelectedItem;
                RaspodelaDinamickeOpremeDto dto = new(null, uProstoriju.Id, (DinamickaOprema)listViewDinamOpreme.SelectedValue, 
                    Int32.Parse(tbKolDin.Text));
                UpravnikKontroler.Instance.RasporedjivanjeDinamickeOpreme(dto);
                listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOpremaRepo.Instance.DinamickaOprema;
                ListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
                ListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            }
        }
        private void dugmeRaspodeliStaticku_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null && listaProstorijaS.SelectedValue != null && datumStat.SelectedDate != null)
            {
                Prostorija prostorija = (Prostorija)listaProstorijaS.SelectedItem;
                UpravnikKontroler.Instance.RasporedjivanjeStatickeOpreme(new(null, prostorija.Id, 
                    (StatickaOprema)listViewStatOpreme.SelectedItem, Int32.Parse(tbKolStat.Text), (DateTime)datumStat.SelectedDate));
                listViewStatOpreme.ItemsSource = Repozitorijum.StatickaOpremaRepo.Instance.StatickaOprema;
                ListaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            }
        }
    }
}
