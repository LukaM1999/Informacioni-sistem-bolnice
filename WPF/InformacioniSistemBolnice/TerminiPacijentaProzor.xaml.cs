using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Repozitorijum;
using Model;
using System.Collections.ObjectModel;
using Servis;
using Kontroler;
using System.Threading;
using InformacioniSistemBolnice.Servis;
using System.Diagnostics;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaProzor : Window
    {
        public Pacijent ulogovanPacijent;

        public TerminiPacijentaProzor(string korisnickoIme, string lozinka)
        {
            InitializeComponent();
            InicijalizujProzor(korisnickoIme, lozinka);
            PokreniNiti();
        }

        private void PokreniNiti()
        {
            new Thread(() => ProveraZavrsenostiTermina.Instance.ProveriZavrsenostTermina(ulogovanPacijent)).Start();
            new Thread(() => UpravljanjeAntiTrollMehanizmom.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent)).Start();
            UpravljanjeAnketama.Instance.OtvoriAnketuOBolnici(ulogovanPacijent);
            new Thread(() => UpravljanjeObavestenjimaTerapija.Instance.UkljuciObavestenja(ulogovanPacijent)).Start();
        }

        private void InicijalizujProzor(string korisnickoIme, string lozinka)
        {
            Termini.Instance.Deserijalizacija();
            Pacijenti.Instance.Deserijalizacija();
            Lekari.Instance.Deserijalizacija();
            AnketeOBolnici.Instance.Deserijalizacija();
            ulogovanPacijent = PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            PostaviTermineUlogovanogPacijenta();
            listaZakazanihTermina.ItemsSource = ulogovanPacijent.zakazaniTermini;
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
