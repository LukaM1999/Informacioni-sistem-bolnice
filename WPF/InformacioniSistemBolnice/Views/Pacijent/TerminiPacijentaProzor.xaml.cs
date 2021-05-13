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
        private Pacijent ulogovanPacijent;

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
            ulogovanPacijent = Pacijenti.Instance.PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            Pacijenti.Instance.PostaviTermineUlogovanogPacijenta(ulogovanPacijent);
            listaZakazanihTermina.ItemsSource = ulogovanPacijent.zakazaniTermini;
        }

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            Termin terminZaPomeranje = (Termin)listaZakazanihTermina.SelectedItem;
            if (listaZakazanihTermina.SelectedIndex >= 0 && terminZaPomeranje.vreme > DateTime.Now.AddHours(24)
            && terminZaPomeranje.status != StatusTermina.pomeren)
                new PomeranjeTerminaPacijentaProzor(terminZaPomeranje).Show();
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            new ZakazivanjeTerminaPacijentaProzor(ulogovanPacijent.jmbg, this).Show();
        }

        private void otkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Otkazivanje((Termin) listaZakazanihTermina.SelectedValue);
        }

        private void infoDugme_Click(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0) PacijentKontroler.Instance.Uvid(listaZakazanihTermina);
        }

        private void prikaziVesti_Click(object sender, RoutedEventArgs e)
        {
            new ProzorSaVestima().Show();
        }

        private void OtvoriAnketu(object sender, RoutedEventArgs e)
        {
            Termin izabranTermin = (Termin)listaZakazanihTermina.SelectedValue;
            if (JeIzabranZavrsenTermin(izabranTermin)) new AnketaOLekaruForma(izabranTermin).Show();
        }

        private static bool JeIzabranZavrsenTermin(Termin izabranTermin)
        {
            return izabranTermin is { status: StatusTermina.zavrsen, AnketaOLekaru: null };
        }
    }
}
