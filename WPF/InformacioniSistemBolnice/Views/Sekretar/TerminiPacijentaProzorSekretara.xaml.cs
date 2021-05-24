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
using Kontroler;

namespace InformacioniSistemBolnice
{
  
    public partial class TerminiPacijentaProzorSekretara : UserControl
    {

        public PocetnaStranicaSekretara pocetna;
        public TerminiPacijentaProzorSekretara(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            TerminRepo.Instance.Deserijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();

            /*foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (pacijent.Korisnik.KorisnickoIme.Equals(korisnickoIme))
                {
                    ulogovanPacijent = pacijent;
                    break;
                }
            }
            */
            listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaSekretara zakazivanjeTerminaSekretara = new ZakazivanjeTerminaSekretara(this);
            zakazivanjeTerminaSekretara.Show();
        }

        private void otkaziTermin_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                System.Diagnostics.Debug.WriteLine((Termin)listaZakazanihTermina.SelectedValue);
                SekretarKontroler.Instance.Otkazivanje((Termin)listaZakazanihTermina.SelectedValue);
                listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
            }
        }

        private void pomjeriTermin_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
               PomjeranjeTerminaProzorSekretara pomjeranje = new PomjeranjeTerminaProzorSekretara(this);
               pomjeranje.Show();
            }

        }

        private void uvidUTerminPacijenta_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                PacijentKontroler.Instance.Uvid(listaZakazanihTermina);
            }
        }
    }
}



