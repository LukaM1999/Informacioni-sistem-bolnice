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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Kontroler;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class UCLekInfo : MetroContentControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        private Lek lek;
        private object prethodniProzor;

        public UCLekInfo(GlavniProzorLekara glavni, Lek izabranLek, object prethodni)
        {
            InitializeComponent();
            ZahtevRepo.Instance.Deserijalizacija();
            prethodniProzor = prethodni;
            glavniProzorLekara = glavni;
            lek = izabranLek;
            naziv.Content = lek.Naziv;
            proizvodjac.Content = lek.Proizvodjac;
            sastojci.Text = lek.Sastojci;
            zamena.Text = lek.Zamena;
            Alergeni.ItemsSource = lek.Alergen;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzorLekara.contentControl.Content = prethodniProzor;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        { 
            LekKontroler.Instance.IzmenaLeka(new(lek.Naziv, lek.Proizvodjac, sastojci.Text, zamena.Text, lek.Alergen));
            if (!string.IsNullOrWhiteSpace(komentar.Text))
            {
                ZahtevKontroler.Instance.KreirajZahtev(new(komentar.Text,
                    (glavniProzorLekara.ulogovanLekar.Ime + " " + glavniProzorLekara.ulogovanLekar.Prezime)));
                MessageBox.Show("Uspesno ste poslali zahtev");
            }
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }
    }
}
