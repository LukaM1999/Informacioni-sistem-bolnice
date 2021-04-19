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

        public List<string> listaDatuma = new List<string>();
        public string pacijentJMBG;
        public TerminiPacijentaProzor terminiPacijentaProzor;

        public ZakazivanjeTerminaPacijentaProzor(string jmbgPacijenta, TerminiPacijentaProzor termini)
        {
            InitializeComponent();
            terminiPacijentaProzor = termini;
            /*
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }

            listaSati.ItemsSource = listaDatuma;
            */
            Lekari.Instance.Deserijalizacija("../../../json/lekari.json");
            lekari.ItemsSource = Lekari.Instance.listaLekara;
            pacijentJMBG = jmbgPacijenta;
        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            IzborTermina izborTermina = new IzborTermina(this, pacijentJMBG);
            izborTermina.Visibility = Visibility.Visible;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
