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
using InformacioniSistemBolnice;
using Kontroler;
using System.Threading;
using FluentScheduler;
using InformacioniSistemBolnice.Servis;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaProzor : Window
    {
        public Pacijent ulogovanPacijent;

        public TerminiPacijentaProzor(string korisnickoIme, string lozinka)
        {
            InitializeComponent();
            Termini.Instance.Deserijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            ulogovanPacijent = PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            PostaviTermineUlogovanogPacijenta();
            listaZakazanihTermina.ItemsSource = ulogovanPacijent.zakazaniTermini;

            Thread proveraZavrsenostiTermina = new Thread(ProveriZavrsenostTermina);
            proveraZavrsenostiTermina.Start();

            Thread proveraMalicioznosti = new Thread(() =>
                { UpravljanjeAntiTrollMehanizmom.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent); });
            proveraMalicioznosti.Start();
        }

        private void PostaviTermineUlogovanogPacijenta()
        {
            ObservableCollection<Termin> zakazaniTermini = new();
            foreach (Termin termin in Termini.Instance.listaTermina)
            {
                if (termin.pacijentJMBG != ulogovanPacijent.jmbg) continue;
                zakazaniTermini.Add(termin);
            }
            ulogovanPacijent.zakazaniTermini = zakazaniTermini;
            Pacijenti.Instance.Serijalizacija();
            Pacijenti.Instance.Deserijalizacija();
        }

        private Pacijent PronadjiUlogovanogPacijenta(string korisnickoIme, string lozinka)
        {
            foreach (Pacijent pacijent in Pacijenti.Instance.listaPacijenata)
            {
                if (!JeUlogovaniPacijent(korisnickoIme, lozinka, pacijent)) continue;
                return pacijent;
            }
            return null;
        }

        private static bool JeUlogovaniPacijent(string korisnickoIme, string lozinka, Pacijent pacijent)
        {
            return pacijent.korisnik.korisnickoIme == korisnickoIme && pacijent.korisnik.lozinka == lozinka;
        }

        private void ProveriZavrsenostTermina()
        {
            while (true)
            {
                foreach (Termin termin in Termini.Instance.listaTermina.ToList())
                {
                    if (!JeTerminZavrsen(termin)) continue;
                    termin.status = StatusTermina.zavrsen;
                    ZavrsiPacijentovTermin(termin);
                    Termini.Instance.Serijalizacija();
                    Termini.Instance.Deserijalizacija();
                    System.Diagnostics.Debug.WriteLine(DateTime.Now);
                }
                Thread.Sleep(1000);
            }
        }

        private void ZavrsiPacijentovTermin(Termin termin)
        {
            foreach (Termin pacijentovTermin in ulogovanPacijent.zakazaniTermini)
            {
                if (termin.vreme != pacijentovTermin.vreme || pacijentovTermin.status == StatusTermina.zavrsen) continue;
                pacijentovTermin.status = StatusTermina.zavrsen;
                break;
            }
        }

        private static bool JeTerminZavrsen(Termin termin)
        {
            return termin.vreme.AddMinutes(termin.trajanje) < DateTime.Now && termin.status != StatusTermina.zavrsen;
        }

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0 && ((Termin)listaZakazanihTermina.SelectedItem).vreme > DateTime.Now.AddHours(24))
            {
                PomeranjeTerminaPacijentaProzor pomeranje = new PomeranjeTerminaPacijentaProzor(this);
                pomeranje.Show();
            }
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTerminaPacijentaProzor zakazivanjeProzor = new ZakazivanjeTerminaPacijentaProzor(ulogovanPacijent.jmbg, this);
            zakazivanjeProzor.Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Otkazivanje(listaZakazanihTermina);
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0)
            {
                PacijentKontroler.Instance.Uvid(listaZakazanihTermina);
            }
        }

        private void prikaziVesti_Click(object sender, RoutedEventArgs e)
        {
            ProzorSaVestima prozorSaVestima = new ProzorSaVestima();
            prozorSaVestima.Show();
        }

        private void OtvoriAnketu(object sender, RoutedEventArgs e)
        {
            Termin izabranTermin = (Termin)listaZakazanihTermina.SelectedValue;
            if (!JeIzabranZavrsenTermin(izabranTermin)) return;
            AnketaOLekaruForma anketaOLekaru = new(izabranTermin);
            anketaOLekaru.Show();
        }

        private static bool JeIzabranZavrsenTermin(Termin izabranTermin)
        {
            return izabranTermin is {status: StatusTermina.zavrsen, AnketaOLekaru: null};
        }
    }
}
