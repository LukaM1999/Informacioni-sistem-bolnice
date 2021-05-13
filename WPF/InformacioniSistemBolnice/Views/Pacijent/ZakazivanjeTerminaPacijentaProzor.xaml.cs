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
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeTerminaPacijentaProzor : Window
    {
        public string pacijentJMBG;
        public TerminiPacijentaProzor terminiPacijentaProzor;

        public ZakazivanjeTerminaPacijentaProzor(string jmbgPacijenta, TerminiPacijentaProzor termini)
        {
            InitializeComponent();
            terminiPacijentaProzor = termini;
            lekari.ItemsSource = Lekari.Instance.listaLekara;
            pacijentJMBG = jmbgPacijenta;
        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            new IzborTermina(this, pacijentJMBG).Show();
        }
    }
}
