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
            bool nasaoKorisnika = false;
            foreach (Korisnik k in Korisnici.Instance.listaKorisnika)
            {
                if (username.Text == k.korisnickoIme && password.Password.ToString() == k.lozinka)
                {
                    nasaoKorisnika = true;
                    switch (k.uloga)
                    {
                        case Model.UlogaKorisnika.upravnik:
                            ProstorijeProzor prostorijeP = new ProstorijeProzor();
                            prostorijeP.Show();
                            this.Hide();
                            break;

                        case Model.UlogaKorisnika.lekar:
                            GlavniProzorLekara glavniProzorLekara = new GlavniProzorLekara(k.korisnickoIme, k.lozinka);
                            glavniProzorLekara.Show();
                            this.Close();
                            break;

                        case Model.UlogaKorisnika.pacijent:
                            TerminiPacijentaProzor tpp = new TerminiPacijentaProzor(k.korisnickoIme, k.lozinka);
                            tpp.Show();
                            this.Close();
                            break;

                        case Model.UlogaKorisnika.sekretar:
                            //GlavniProzorSekretara glavniProzorSekretara = new GlavniProzorSekretara();
                            //glavniProzorSekretara.Show();
                            PocetnaStranicaSekretara pocetnaStranicaSekretara = new();
                            pocetnaStranicaSekretara.Show();
                            this.Close();
                            break;

                        default: break;
                    }

                    if (nasaoKorisnika)
                    {
                        break;
                    }
                }

            }

            if (!nasaoKorisnika)
            {
                MessageBox.Show("Invalid input");
            }

        }

    }
}
