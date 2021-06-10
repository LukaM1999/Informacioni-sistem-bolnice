using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Kontroler;
using Model;
using System.Collections.ObjectModel;
using System;

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
            ZakazivanjeTerminaSekretara zakazivanjeTerminaSekretara = new ZakazivanjeTerminaSekretara(this, pocetna);
            pocetna.contentControl.Content = zakazivanjeTerminaSekretara.Content;
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

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Termin> Filter = new ObservableCollection<Termin>();
            foreach (var termin in TerminRepo.Instance.Termini)
            {
                if (datum.SelectedDate is null)
                {
                    listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
                }
                else
                {
                    if (Nadji((DateTime)datum.SelectedDate, termin))
                        Filter.Add(termin);
                }
            }

            listaZakazanihTermina.ItemsSource = Filter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
        }


        private bool Nadji(DateTime text, Termin p)
        {
            return text.Day == p.Vreme.Day;
        }

        private void datum_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ObservableCollection<Termin> Filter = new ObservableCollection<Termin>();
            foreach (var lek in TerminRepo.Instance.Termini)
            {
                if (datum.SelectedDate is null)
                {

                    listaZakazanihTermina.ItemsSource = TerminRepo.Instance.Termini;
                }
                else
                {
                    if (Nadji((DateTime)datum.SelectedDate, lek))
                        Filter.Add(lek);
                }
            }

            listaZakazanihTermina.ItemsSource = Filter;
        }
    }
}




