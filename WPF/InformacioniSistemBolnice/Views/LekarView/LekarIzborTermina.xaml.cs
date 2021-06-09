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
using InformacioniSistemBolnice.DTO;
using Kontroler;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.LekarView
{
    public partial class LekarIzborTermina : MetroContentControl
    {
        private GlavniProzorLekara glavni;
        public ObservableCollection<Termin> slobodniTermini = new();
        public UputDto uput;
        public object prethodni;

        public LekarIzborTermina(GlavniProzorLekara glavniProzor, UputDto noviUput, object prethodniProzor)
        {
            InitializeComponent();
            glavni = glavniProzor;
            uput = noviUput;
            prethodni = prethodniProzor;
            /*TimeSpan intervalDana = uput.Kraj - uput.Pocetak;
            DateTime slobodanTermin = uput.Pocetak.AddHours(7);*/
            GeneriseSlobodneTermine(uput.Kraj - uput.Pocetak, uput.Pocetak.AddHours(7));
            IzbacujeZauzeteTermineLekara();
            IzbacujeZauzeteTerminePacijenta();
            //slobodniTermini.Clear();
            NemaSlobodnihTermina();
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            glavni.contentControl.Content = prethodni;
        }

        private void NemaSlobodnihTermina()
        {
            if (slobodniTermini.Count == 0)
            {
                DateTime slobodanTermin = (uput.Pocetak.Subtract(new TimeSpan(48, 0, 0)));
                slobodanTermin = slobodanTermin.AddHours(7);
                GeneriseNovuListuSlobodnihTermina(slobodanTermin);
                slobodanTermin = uput.Kraj.AddHours(7);
                GeneriseNovuListuSlobodnihTermina(slobodanTermin);
                IzbacujeZauzeteTermineLekara();
                IzbacujeZauzeteTerminePacijenta();
            }
        }

        private void GeneriseNovuListuSlobodnihTermina(DateTime slobodanTermin)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    slobodanTermin = PopunjavaNovuListuSlobodnihTermina(slobodanTermin);
                }
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }
        }

        private DateTime PopunjavaNovuListuSlobodnihTermina(DateTime slobodanTermin)
        {
            foreach (Termin zauzetTermin in uput.Lekar.ZakazaniTermini)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                    uput.Tip, StatusTermina.slobodan,
                    uput.Pacijent.Jmbg, uput.Lekar.Jmbg, uput.Prostorija.Id, uput.Hitan));
                if (ImaZakazanoRenoviranje(slobodanTermin, uput.Prostorija.Id)) continue;
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            return slobodanTermin;
        }

        private bool ImaZakazanoRenoviranje(DateTime slobodanTermin, string id)
        {
            Prostorija prostorija = ProstorijaRepo.Instance.NadjiPoId(id);
            if (prostorija.Renoviranje != null)
            {
                if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                    slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) return true;
            }
            return false;
        }

        private void IzbacujeZauzeteTermineLekara()
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in uput.Lekar.ZakazaniTermini)
                {
                    if (predlozenTermin.Vreme == postojeciTermin.Vreme)
                    {
                        slobodniTermini.Remove(predlozenTermin);
                        break;
                    }
                }
            }
        }
        private void IzbacujeZauzeteTerminePacijenta()
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in uput.Pacijent.ZakazaniTermini)
                {
                    if (predlozenTermin.Vreme == postojeciTermin.Vreme)
                    {
                        slobodniTermini.Remove(predlozenTermin);
                        break;
                    }
                }
            }
        }

        private void GeneriseSlobodneTermine(TimeSpan intervalDana, DateTime slobodanTermin)
        {
            for (int i = 0; i < intervalDana.Days; i++)
            {
                slobodanTermin = PopunjavaListuSlobodnihTermina(slobodanTermin);
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }
        }

        private DateTime PopunjavaListuSlobodnihTermina(DateTime slobodanTermin)
        {
            for (int j = 0; j < 27; j++)
            {
                slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                    uput.Tip, StatusTermina.slobodan,
                    uput.Pacijent.Jmbg, uput.Lekar.Jmbg, uput.Prostorija.Id, uput.Hitan));
                if (ImaZakazanoRenoviranje(slobodanTermin, uput.Prostorija.Id)) continue;
                slobodanTermin = slobodanTermin.AddMinutes(30);
            }
            return slobodanTermin;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            if (ponudjeniTermini.SelectedIndex == -1) return;
            TerminKontroler.Instance.ZakaziTermin((Termin)ponudjeniTermini.SelectedItem);
            glavni.contentControl.Content = new UCRaspored(glavni);
        }
    }
}
