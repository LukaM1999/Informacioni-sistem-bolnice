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
    /// Interaction logic for VestiView.xaml
    /// </summary>
    public partial class VestiView : Window
    {
        public Korisnik korisnik;
        public VestiView()
        {
            InitializeComponent();
            KorisnikRepo.Instance.Deserijalizacija();

            foreach (Korisnik pacijent in KorisnikRepo.Instance.Korisnici)
            {
                if (pacijent.Uloga.Equals("2"))
                {
                    korisnik = pacijent;
                    break;
                }
            }


            VestRepo.Instance.Deserijalizacija();
            ListaVesti.ItemsSource = VestRepo.Instance.Vesti;



        }

        private void vidiVest_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PregledVesti((Vest)ListaVesti.SelectedItem);
        }

        private void ListaVesti_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
