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

namespace InformacioniSistemBolnice
{
    public partial class ProstorijeProzor : Window
    {
        public ProstorijeProzor()
        {
            InitializeComponent();
            Prostorije.Instance.Deserijalizacija("../../../json/prostorije.json");
            ListaProstorija.ItemsSource = Prostorije.Instance.listaProstorija;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            ProstorijaForma pf = new ProstorijaForma();
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
            }
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ListaProstorija.SelectedValue != null)
            {
                Prostorija pr = (Prostorija)ListaProstorija.SelectedValue;
                UpravnikKontroler.Instance.PregledProstorije(pr);
            }

        }

        private void magacinDugme_Click(object sender, RoutedEventArgs e)
        {
            MagacinProzor mp = new MagacinProzor();
            mp.Show();
        }
    }
}
