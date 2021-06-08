using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Kontroler;
using Model;

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
            listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
        }

        private void ZakaziTermin_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaSekretara zakazivanjeTerminaSekretara = new ZakazivanjeTerminaSekretara(this);
            zakazivanjeTerminaSekretara.Show();
        }

        private void OtkaziTermin_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                System.Diagnostics.Debug.WriteLine((Termin)listaZakazanihTermina.SelectedValue);
                TerminKontroler.Instance.OtkaziTermin((Termin)listaZakazanihTermina.SelectedValue);
                listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
            }
        }

        private void PomeriTermin_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
               PomjeranjeTerminaProzorSekretara pomjeranje = new(this);
               pomjeranje.Show();
            }
        }
    }
}



