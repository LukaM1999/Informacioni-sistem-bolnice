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
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProzorSaVestima.xaml
    /// </summary>
    public partial class ProzorSaVestima : Window
    {
        public Korisnik korisnik;
        public ProzorSaVestima()
        {
            InitializeComponent();
            Korisnici.Instance.Deserijalizacija("../../../json/korisnici.json");

            foreach (Korisnik pacijent in Korisnici.Instance.listaKorisnika)
            {
                if (pacijent.uloga.Equals("2"))
                {
                    korisnik = pacijent;
                    break;
                }
            }


            Vesti.Instance.Deserijalizacija("../../../json/vesti.json");
            ListaVesti.ItemsSource = Vesti.Instance.listaVesti;



        }

        private void vidiVest_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PregledVesti(ListaVesti);
        }

        private void ListaVesti_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
