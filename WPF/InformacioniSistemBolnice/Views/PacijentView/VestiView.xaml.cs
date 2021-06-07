using System.Windows;
using Model;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
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
            VestKontroler.Instance.PregledVesti((Vest)ListaVesti.SelectedItem);
        }
    }
}
