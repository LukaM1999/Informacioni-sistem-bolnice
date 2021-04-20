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
            Lekari.Instance.Deserijalizacija("../../../json/lekari.json");

            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                System.Diagnostics.Debug.WriteLine(lekar.korisnik.korisnickoIme);
                if (lekar.korisnik.korisnickoIme.Equals(korisnickoIme) && lekar.korisnik.lozinka.Equals(lozinka))
                {
                    ulogovanLekar = lekar;
                    break;
                }
            }
        }

        private void RasporedBtn_Click(object sender, RoutedEventArgs e)
        {
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor(ulogovanLekar);
            terminiLekara.Show();
            this.Show();

        }
        private void PacijentiBtn_Click(object sender, RoutedEventArgs e)
        {
            //userControl
            TerminiLekaraProzor terminiLekara = new TerminiLekaraProzor(ulogovanLekar);
            terminiLekara.Show();
            this.Show();

        }
        private void OdjavaBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
