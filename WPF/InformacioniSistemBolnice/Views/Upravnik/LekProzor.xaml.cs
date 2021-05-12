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
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for LekProzor.xaml
    /// </summary>
    public partial class LekProzor : Window
    {
        public LekProzor()
        {
            InitializeComponent();
            Lekovi.Instance.Deserijalizacija();
            listaLekova.ItemsSource = Lekovi.Instance.listaLekova;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            LekDodajProzor prozor = new LekDodajProzor();
            prozor.Show();
            listaLekova.ItemsSource = Lekovi.Instance.listaLekova;
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            Lek lek = (Lek)listaLekova.SelectedItem;
            UpravnikKontroler.Instance.BrisanjeLeka(lek);
            listaLekova.ItemsSource = Lekovi.Instance.listaLekova;
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            LekIzmeniProzor prozor = new LekIzmeniProzor(listaLekova);
            prozor.PostaviText();
            prozor.Show();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            LekInfoProzor prozor = new LekInfoProzor((Lek)listaLekova.SelectedItem);
            prozor.Show();
        }
    }
}
