using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using Kontroler;
using InformacioniSistemBolnice.Views;
using InformacioniSistemBolnice.Views.LekarView;
using MahApps.Metro.Controls;

namespace InformacioniSistemBolnice
{
    public partial class UCRaspored : MetroContentControl
    {
        public Lekar lekar;
        public GlavniProzorLekara glavniProzor;
        public ObservableCollection<Termin> zakazaniTermini = new();
        public UCRaspored(GlavniProzorLekara glavniProzorLekara)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            lekar = glavniProzorLekara.ulogovanLekar;
            PronadjiTermineLekara();
            ZakazaniTermini.ItemsSource = zakazaniTermini;
            glavniProzor = glavniProzorLekara;
        }

        private void PronadjiTermineLekara()
        {
            zakazaniTermini.Clear();
            foreach (Termin termin in TerminRepo.Instance.Termini.ToList())
            {
                if (lekar.Jmbg.Equals(termin.LekarJmbg))
                {
                    zakazaniTermini.Add(termin);
                }
            }
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new UCZakazivanje(glavniProzor);
            PronadjiTermineLekara();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex > -1)
            {
                TerminKontroler.Instance.OtkaziTermin((Termin)ZakazaniTermini.SelectedItem);
                zakazaniTermini.Remove((Termin)ZakazaniTermini.SelectedItem);
            }
        }

        private void Pomeri_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex > -1)
            {
                glavniProzor.contentControl.Content = new LekarPomeranjeTermina(glavniProzor, (Termin)ZakazaniTermini.SelectedItem);
                PronadjiTermineLekara();
            }
        }

        private void Pregled_Click(object sender, RoutedEventArgs e)
        {
            if (ZakazaniTermini.SelectedIndex >= 0)
            {
                Karton karton = new Karton()
                { DataContext = new KartonViewModel(glavniProzor, ((Termin)ZakazaniTermini.SelectedItem).PacijentJmbg) };
                glavniProzor.contentControl.Content = karton;
            }
        }

        private bool Nadji(DateTime text, Termin p)
        {
            return text.Day == p.Vreme.Day;
        }

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Termin> Filter = new ObservableCollection<Termin>();
            foreach (var lek in zakazaniTermini)
            {
                if (string.IsNullOrWhiteSpace(datum.SelectedDate.ToString())) ZakazaniTermini.ItemsSource = zakazaniTermini;
                else
                {
                    if (Nadji((DateTime) datum.SelectedDate, lek))
                        Filter.Add(lek);
                }
            }

            ZakazaniTermini.ItemsSource = Filter;
        }

        private void datum_KeyUp(object sender, KeyEventArgs e)
        {
            ObservableCollection<Termin> Filter = new ObservableCollection<Termin>();
            foreach (var lek in zakazaniTermini)
            {
                if (string.IsNullOrWhiteSpace(datum.SelectedDate.ToString())) 
                    ZakazaniTermini.ItemsSource = zakazaniTermini;
                else
                {
                    if (Nadji((DateTime)datum.SelectedDate, lek))
                        Filter.Add(lek);
                }
            }

            ZakazaniTermini.ItemsSource = Filter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PronadjiTermineLekara();
            ZakazaniTermini.ItemsSource = zakazaniTermini;
        }
    }
}
