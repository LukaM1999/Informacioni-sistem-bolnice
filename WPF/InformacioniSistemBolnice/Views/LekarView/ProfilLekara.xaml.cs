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
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.LekarView
{
    public partial class ProfilLekara : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public Lekar Lekar { get; set; }
        public ProfilLekara(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            Lekar = glavni.ulogovanLekar;
            glavniProzor = glavni;
            ime.Content = Lekar.Ime;
            prezime.Content = Lekar.Prezime;
            jmbg.Content = Lekar.Jmbg;
            telefon.Content = Lekar.Telefon;
            specijalizacija.Content = Lekar.Specijalizacija.Naziv;
            email.Content = Lekar.Email;
            drzava.Content = Lekar.AdresaStanovanja.Drzava;
            grad.Content = Lekar.AdresaStanovanja.Grad;
            ulica.Content = Lekar.AdresaStanovanja.Ulica;
            broj.Content = Lekar.AdresaStanovanja.Broj;
            datum.Content = Lekar.DatumRodjenja.Date.ToString("d");
        }
    }
}
