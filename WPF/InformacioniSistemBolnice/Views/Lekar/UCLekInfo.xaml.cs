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
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCLekInfo.xaml
    /// </summary>
    public partial class UCLekInfo : UserControl
    {
        public GlavniProzorLekara glavniProzorLekara;
        private Lek lek;

        public UCLekInfo(GlavniProzorLekara glavni, Lek izabranLek)
        {
            InitializeComponent();
            ZahtevRepo.Instance.Deserijalizacija();
            glavniProzorLekara = glavni;
            lek = izabranLek;
            naziv.Content = lek.Naziv;
            proizvodjac.Content = lek.Proizvodjac;
            sastojci.Text = lek.Sastojci;
            zamena.Text = lek.Zamena;
            Alergeni.ItemsSource = lek.Alergen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        { 
            LekarKontroler.Instance.IzmenaLeka(new(lek.Naziv, lek.Proizvodjac, sastojci.Text, zamena.Text, lek.Alergen));
            if (!string.IsNullOrWhiteSpace(komentar.Text))
            {
                LekarKontroler.Instance.KreirajZahtev(new(komentar.Text,
                    (glavniProzorLekara.ulogovanLekar.Ime + " " + glavniProzorLekara.ulogovanLekar.Prezime)));
                MessageBox.Show("Uspesno ste poslali zahtev");
            }
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }
    }
}
