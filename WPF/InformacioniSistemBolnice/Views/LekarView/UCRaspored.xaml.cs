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
                PomeranjeTerminaLekaraProzor pomeranje = new PomeranjeTerminaLekaraProzor((Termin)ZakazaniTermini.SelectedItem);
                pomeranje.Show();
            }
            PronadjiTermineLekara();
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
    }
}
