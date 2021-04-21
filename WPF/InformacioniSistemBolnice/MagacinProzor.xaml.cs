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
        public MagacinProzor(ProstorijeProzor p)
        {
            InitializeComponent();
            prostorijaProzor = p;
            Repozitorijum.StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json");
            Repozitorijum.DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
            Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
            listViewStatOpreme.ItemsSource = Repozitorijum.StatickaOprema.Instance.listaOpreme;
            listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOprema.Instance.listaOpreme;
            listaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
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
            if (listViewDinamOpreme.SelectedValue != null)
            {
                Model.DinamickaOprema oprema = (Model.DinamickaOprema)listViewDinamOpreme.SelectedValue;
                int kolicina = Int32.Parse(tbKolDin.Text);

                Repozitorijum.DinamickaOprema.Instance.listaOpreme.ElementAt(Repozitorijum.DinamickaOprema.Instance.listaOpreme.IndexOf(oprema)).kolicina -= kolicina;
                Repozitorijum.DinamickaOprema.Instance.Serijalizacija("../../../json/dinamickaOprema.json");
                Repozitorijum.DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
                listViewDinamOpreme.ItemsSource = Repozitorijum.DinamickaOprema.Instance.listaOpreme;
                Prostorija prostorija = (Prostorija)listaProstorija.SelectedItem;
                //Prostorija prostorija2 = Prostorije.Instance.listaProstorija.ElementAt(Prostorije.Instance.listaProstorija.IndexOf(prostorija));
                foreach (Model.DinamickaOprema p in Prostorije.Instance.getSelected(prostorija).inventar.dinamickaOprema)
                {
                    if (p.tip.Equals(oprema.tip))
                    {
                        Prostorije.Instance.getSelected(prostorija).inventar.getSelected(p).kolicina += kolicina;
                        Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
                        Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
                        prostorijaProzor.ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
                        return;
                    }
                }
                Prostorije.Instance.getSelected(prostorija).inventar.dinamickaOprema.Add(new Model.DinamickaOprema(kolicina, oprema.tip));
                Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
                Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
                prostorijaProzor.ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;

            }


        }

        private void listaProstorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewDinamOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewStatOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewDinamOpreme_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
