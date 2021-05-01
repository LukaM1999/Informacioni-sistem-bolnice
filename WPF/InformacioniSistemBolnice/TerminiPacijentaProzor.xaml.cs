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

        private const int TerminaDoAnkete = 3;
        private const int MesecaDoAnkete = 4;

        public TerminiPacijentaProzor(string korisnickoIme, string lozinka)
        {
            InitializeComponent();
            Termini.Instance.Deserijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            AnketeOBolnici.Instance.Deserijalizacija();
            ulogovanPacijent = PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            PostaviTermineUlogovanogPacijenta();
            listaZakazanihTermina.ItemsSource = ulogovanPacijent.zakazaniTermini;

            OtvoriAnketuOBolnici();

            Thread proveraZavrsenostiTermina = new(ProveriZavrsenostTermina);
            proveraZavrsenostiTermina.Start();

            Thread proveraMalicioznosti = new(() =>
                { UpravljanjeAntiTrollMehanizmom.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent); });
            proveraMalicioznosti.Start();
        }

        private async void OtvoriAnketuOBolnici()
        {
            if (!PacijentPosetioBolnicu(DobaviSortiraneTermine())) return;
            if (PrebrojTermineDoAnkete() < TerminaDoAnkete && !JeVremeZaAnketu()) return;
            AnketaOBolniciForma anketaOBolnici = new(ulogovanPacijent.jmbg);
            await Task.Delay(7000);
            anketaOBolnici.Show();
        }

        private int PrebrojTermineDoAnkete()
        {
            int zavrsenihTermina = 0;
            foreach (Termin termin in DobaviSortiraneTermine())
            {
                if (JeNovozavrsen(termin)) zavrsenihTermina++;
                if (JePrvaAnketa(termin)) zavrsenihTermina++;
            }
            return zavrsenihTermina;
        }

        private bool JePrvaAnketa(Termin termin)
        {
            return DobaviPacijentoveAnkete().Count == 0 && termin.status == StatusTermina.zavrsen;
        }

        private bool JeNovozavrsen(Termin termin)
        {
            List<AnketaOBolnici> sortiraneAnkete = DobaviSortiraneAnkete(DobaviPacijentoveAnkete());
            if (sortiraneAnkete.Count <= 1)
                return termin.status == StatusTermina.zavrsen && sortiraneAnkete.Count != 0 &&
                       termin.vreme < sortiraneAnkete[0].VremePopunjavanja;
            return termin.status == StatusTermina.zavrsen && termin.vreme > sortiraneAnkete[1].VremePopunjavanja &&
                   termin.vreme < sortiraneAnkete[0].VremePopunjavanja;
        }

        private bool JeVremeZaAnketu()
        {
            List<AnketaOBolnici> sortiraneAnkete = DobaviSortiraneAnkete(DobaviPacijentoveAnkete());
            return sortiraneAnkete[0].VremePopunjavanja.AddMonths(MesecaDoAnkete) < DateTime.Now;
        }

        private List<AnketaOBolnici> DobaviPacijentoveAnkete()
        {
            List<AnketaOBolnici> pacijentoveAnkete = new();
            foreach (AnketaOBolnici anketa in AnketeOBolnici.Instance.AnketeZaBolnicu.ToList())
            {
                if (anketa.PacijentovJmbg == ulogovanPacijent.jmbg) pacijentoveAnkete.Add(anketa);
            }
            return pacijentoveAnkete;
        }

        private static List<AnketaOBolnici> DobaviSortiraneAnkete(List<AnketaOBolnici> pacijentoveAnkete)
        {
            return pacijentoveAnkete.OrderByDescending(anketa => anketa.VremePopunjavanja).ToList();
        }

        private List<Termin> DobaviSortiraneTermine()
        {
            List<Termin> sortiraniTermini = ulogovanPacijent.zakazaniTermini.OrderBy(termin => termin.vreme).ToList();
            return sortiraniTermini;
        }

        private static bool PacijentPosetioBolnicu(List<Termin> sortiraniTermini)
        {
            return sortiraniTermini.Count != 0 && sortiraniTermini[0].status == StatusTermina.zavrsen;
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

        private static Pacijent PronadjiUlogovanogPacijenta(string korisnickoIme, string lozinka)
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
                if (!JePacijentovNezavrsenTermin(termin, pacijentovTermin)) continue;
                pacijentovTermin.status = StatusTermina.zavrsen;
                break;
            }
        }

        private static bool JePacijentovNezavrsenTermin(Termin termin, Termin pacijentovTermin)
        {
            return termin.vreme == pacijentovTermin.vreme && pacijentovTermin.status != StatusTermina.zavrsen;
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
            return izabranTermin is { status: StatusTermina.zavrsen, AnketaOLekaru: null };
        }
    }
}
