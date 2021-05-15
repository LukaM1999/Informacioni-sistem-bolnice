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
using System.Threading;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Korisnici.Instance.Deserijalizacija();
            StatickaOpremaTermini.Instance.Deserijalizacija();
            Repozitorijum.StatickaOprema.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            RenoviranjeTermini.Instance.Deserijalizacija();
            new Thread(RasporedjivanjeStatickeOpreme.Instance.ProveraPremestajaOpreme).Start();
            new Thread(Renoviranje.Instance.ProveraRenoviranja).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Korisnik korisnik in Korisnici.Instance.listaKorisnika)
            {
                if (username.Text != korisnik.korisnickoIme || password.Password.ToString() != korisnik.lozinka) continue;
                PretraziUlogu(korisnik);
                return;
            }
            MessageBox.Show("Invalid input");
        }

        private void PretraziUlogu(Korisnik korisnik)
        {
            switch (korisnik.uloga)
            {
                case UlogaKorisnika.upravnik:
                    ProstorijeProzor prostorijeP = new ProstorijeProzor();
                    prostorijeP.Show();
                    this.Hide();
                    return;

                case UlogaKorisnika.lekar:
                    GlavniProzorLekara glavniProzorLekara = new GlavniProzorLekara(korisnik.korisnickoIme, korisnik.lozinka);
                    glavniProzorLekara.Show();
                    this.Close();
                    return;

                case UlogaKorisnika.pacijent:
                    TerminiPacijentaView tpp = new TerminiPacijentaView(korisnik.korisnickoIme, korisnik.lozinka);
                    tpp.Show();
                    this.Close();
                    return;

                case UlogaKorisnika.sekretar:
                    PocetnaStranicaSekretara pocetnaStranica = new();
                    pocetnaStranica.Show();
                    this.Close();
                    return;
            }
        }
    }
}
