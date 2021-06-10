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
using InformacioniSistemBolnice.Views.UpravnikView;

namespace InformacioniSistemBolnice
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            KorisnikRepo.Instance.Deserijalizacija();
            PremestanjeStatickeOpremeRepo.Instance.Deserijalizacija();
            StatickaOpremaRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            RenoviranjeRepo.Instance.Deserijalizacija();
            TransformacijaProstorijaRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            FeedbackRepo.Instance.Deserijalizacija();
            new Thread(RaspodelaStatickeOpremeServis.Instance.ProveraPremestajaOpreme).Start();
            new Thread(RenoviranjeServis.Instance.ProveraRenoviranja).Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Korisnik korisnik in KorisnikRepo.Instance.Korisnici)
            {
                if (username.Text != korisnik.KorisnickoIme || password.Password.ToString() != korisnik.Lozinka) continue;
                PretraziUlogu(korisnik);
                return;
            }
            MessageBox.Show("Invalid input");
        }

        private void PretraziUlogu(Korisnik korisnik)
        {
            switch (korisnik.Uloga)
            {
                case UlogaKorisnika.upravnik:
                    //ProstorijeProzor prostorijeP = new ProstorijeProzor();
                    //prostorijeP.Show();
                    GlavniProzor prozor = new GlavniProzor();
                    prozor.Show();
                    this.Hide();
                    return;

                case UlogaKorisnika.lekar:
                    GlavniProzorLekara glavniProzorLekara = new GlavniProzorLekara(korisnik.KorisnickoIme, korisnik.Lozinka);
                    glavniProzorLekara.Show();
                    this.Close();
                    return;

                case UlogaKorisnika.pacijent:
                    GlavniProzorPacijentaView tpp = new GlavniProzorPacijentaView(korisnik.KorisnickoIme, korisnik.Lozinka);
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
