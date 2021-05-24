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

            PacijentRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            ulogovanLekar.ZauzetiTermini.Clear();
            foreach (Termin termin in TerminRepo.Instance.Termini.ToList())
            {
                if (ulogovanLekar.Jmbg.Equals(termin.LekarJmbg))
                {
                    ulogovanLekar.ZauzetiTermini.Add(termin);
                }
            }
            listaZakazanihTerminaLekara.ItemsSource = ulogovanLekar.ZauzetiTermini;
            lekar = ulogovanLekar;
            jmbgLekara = ulogovanLekar.Jmbg;
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
                listaZakazanihTerminaLekara.ItemsSource = lekar.ZauzetiTermini;
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
