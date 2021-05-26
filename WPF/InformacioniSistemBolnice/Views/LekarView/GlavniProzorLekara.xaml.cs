using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InformacioniSistemBolnice.Servis.UpravljanjeBolnickimLecenjima;
using InformacioniSistemBolnice.Views;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for GlavniProzorLekara.xaml
    /// </summary>
    public partial class GlavniProzorLekara : Window
    {
        public Lekar ulogovanLekar;

        public GlavniProzorLekara(string korisnickoIme, string lozinka)
        {
            InitializeComponent();
            LekarRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            BolnickoLecenjeRepo.Instance.Deserijalizacija();
            PronadjiUlogovanogLekara(korisnickoIme, lozinka);
            contentControl.Content = new UCRaspored(this);
            new Thread(() => new ZavrsenTerminServis<Lekar>().PokreniProveruZavrsenostiTermina(ulogovanLekar)).Start();
            new Thread(() => new ZavrsenoBolnickoLecenjeServis().ProveriZavrsenostLecenja()).Start();
        }

        private void PronadjiUlogovanogLekara(string korisnickoIme, string lozinka)
        {
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                if (!lekar.Korisnik.KorisnickoIme.Equals(korisnickoIme) ||
                    !lekar.Korisnik.Lozinka.Equals(lozinka)) continue;
                ulogovanLekar = lekar;
                break;
            }
        }

        private void RasporedBtn_Click(object sender, RoutedEventArgs e)
        {
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor(ulogovanLekar, this);
            contentControl.Content = new UCRaspored(this);
        }
        private void PacijentiBtn_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new UCPacijenti(this);
        }
        private void OdjavaBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new UCLekovi(this);
        }

        private void BolnickaLecenja_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new UCBolnickaLecenja(this);
        }
    }
}
