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
        public Lek lek;

        public UCLekInfo(GlavniProzorLekara glavni, Lek lek)
        {
            InitializeComponent();
            Zahtevi.Instance.Deserijalizacija();
            glavniProzorLekara = glavni;
            this.lek = lek;
            naziv.Content = lek.Naziv;
            proizvodjac.Content = lek.Proizvodjac;
            sastojci.Text = lek.Sastojci;
            zamena.Text = lek.Zamena;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            lek.Sastojci = sastojci.Text;
            lek.Zamena = zamena.Text;
            Zahtev zahtev = new Zahtev(komentar.Text,
                (glavniProzorLekara.ulogovanLekar.ime + " " + glavniProzorLekara.ulogovanLekar.prezime).ToString());
            if (zahtev!=null)
            {
                MessageBox.Show("Uspesno ste poslali zahtev");
            }
            Zahtevi.Instance.listaZahteva.Add(zahtev);
            Zahtevi.Instance.Serijalizacija();
            Zahtevi.Instance.Deserijalizacija();
            Lekovi.Instance.Serijalizacija();
            Lekovi.Instance.Deserijalizacija();
            UCLekovi lekovi = new UCLekovi(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lekovi;
        }
    }
}
