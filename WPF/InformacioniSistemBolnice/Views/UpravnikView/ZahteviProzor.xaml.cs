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

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    public partial class ZahteviProzor : Window
    {
        public ZahteviProzor()
        {
            InitializeComponent();
            ZahtevRepo.Instance.Deserijalizacija();
            listaZahteva.ItemsSource = ZahtevRepo.Instance.Zahtevi;
        }

        private void btnLekovi_Click(object sender, RoutedEventArgs e)
        {
            LekProzor prozor = new LekProzor();
            prozor.Show();
        }

        private void btnObrisiZahtev_Click(object sender, RoutedEventArgs e)
        {
            Zahtev izabraniZahtev = (Zahtev)listaZahteva.SelectedValue;
            ZahtevKontroler.Instance.BrisanjeZahtevaLeka(new(izabraniZahtev.Komentar, izabraniZahtev.Potpis));
            listaZahteva.ItemsSource = ZahtevRepo.Instance.Zahtevi;
        }

        private void btnInfoZahteva_Click(object sender, RoutedEventArgs e)
        {
            if (listaZahteva.SelectedValue != null)
            {
                ZahteviInfoProzor prozor = new ZahteviInfoProzor(listaZahteva);
                prozor.Show();
            }
        }
    }
}
