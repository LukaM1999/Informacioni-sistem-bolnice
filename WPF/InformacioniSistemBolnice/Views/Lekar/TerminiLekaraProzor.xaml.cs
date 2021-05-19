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
using System.Collections.ObjectModel;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class TerminiLekaraProzor : Window
    {
        public string jmbgLekara;
        public GlavniProzorLekara glavniProzorLekara;
        public Lekar lekar;
        public TerminiLekaraProzor(Lekar ulogovanLekar, GlavniProzorLekara glavni)
        {
            InitializeComponent();

            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            Termini.Instance.Deserijalizacija();
            ulogovanLekar.zauzetiTermini.Clear();
            foreach (Termin termin in Termini.Instance.listaTermina.ToList())
            {
                if (ulogovanLekar.jmbg.Equals(termin.lekarJMBG))
                {
                    ulogovanLekar.zauzetiTermini.Add(termin);
                }
            }
            listaZakazanihTerminaLekara.ItemsSource = ulogovanLekar.zauzetiTermini;
            lekar = ulogovanLekar;
            jmbgLekara = ulogovanLekar.jmbg;
            System.Diagnostics.Debug.WriteLine(jmbgLekara);
            glavniProzorLekara = glavni;
        }
        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                PomeranjeTerminaLekaraProzor pomeranje = new PomeranjeTerminaLekaraProzor((Termin)listaZakazanihTerminaLekara.SelectedItem);
                pomeranje.Show();
            }
        }


        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaLekaraProzor zakazivanjeProzor = new ZakazivanjeTerminaLekaraProzor(jmbgLekara, this.listaZakazanihTerminaLekara);
            zakazivanjeProzor.Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex > -1)
            {
                LekarKontroler.Instance.Otkazivanje((Termin)listaZakazanihTerminaLekara.SelectedItem);
                listaZakazanihTerminaLekara.ItemsSource = lekar.zauzetiTermini;
            }
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            LekarKontroler.Instance.Uvid(listaZakazanihTerminaLekara);
        }

        private void pregledDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                IzmenaZdravstvenogKartonaLekar izmena = new IzmenaZdravstvenogKartonaLekar(
                    (Termin)listaZakazanihTerminaLekara.SelectedItem, glavniProzorLekara);
                izmena.Show();
            }
        }
    }
}
