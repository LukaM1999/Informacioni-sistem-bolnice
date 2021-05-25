﻿using System;
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
using InformacioniSistemBolnice.Views.Lekar;
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
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                System.Diagnostics.Debug.WriteLine(lekar.Korisnik.KorisnickoIme);
                if (lekar.Korisnik.KorisnickoIme.Equals(korisnickoIme) && lekar.Korisnik.Lozinka.Equals(lozinka))
                {
                    ulogovanLekar = lekar;
                    break;
                }
            }
            contentControl.Content = new UCRaspored(this);
            new Thread(() => new ZavrsenTerminServis<Lekar>().PokreniProveruZavrsenostiTermina(ulogovanLekar)).Start();
        }

        private void RasporedBtn_Click(object sender, RoutedEventArgs e)
        {
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor(ulogovanLekar, this);
            //terminiLekara.Show();
            contentControl.Content = new UCRaspored(this);
        }
        private void PacijentiBtn_Click(object sender, RoutedEventArgs e)
        {
            //userControl
            this.contentControl.Content = new UCPacijenti(this);
            //this.Show();
        }
        private void OdjavaBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new UCLekovi(this);
        }

        private void BolnickaLecenja_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new UCBolnickaLecenja(this);
        }
    }
}
