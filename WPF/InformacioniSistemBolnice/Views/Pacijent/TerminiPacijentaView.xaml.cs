﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Repozitorijum;
using Model;
using System.Collections.ObjectModel;
using Servis;
using Kontroler;
using System.Threading;
using InformacioniSistemBolnice.Views.Pacijent;
using System.Diagnostics;
using InformacioniSistemBolnice.ViewModels.Pacijent;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaView : Window
    {
        private Pacijent ulogovanPacijent;

        public TerminiPacijentaView(string korisnickoIme, string lozinka)
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
            UpravljanjeObavestenjimaPacijenta.Instance.UkljuciPodsetnike(ulogovanPacijent.jmbg);
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
                new PomeranjeTerminaPacijentaView(terminZaPomeranje).Show();
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            new ZakazivanjeTerminaPacijentaView(ulogovanPacijent.jmbg).Show();
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
            new VestiView().Show();
        }

        private void OtvoriAnketu(object sender, RoutedEventArgs e)
        {
            Termin izabranTermin = (Termin)listaZakazanihTermina.SelectedValue;
            if (JeIzabranZavrsenTermin(izabranTermin)) new AnketaOLekaruFormaView(izabranTermin).Show();
        }

        private void OtvoriPodsetnikFormu(object sender, RoutedEventArgs e)
        {
            new KreiranjePodsetnikaView(ulogovanPacijent).Show();
        }

        private static bool JeIzabranZavrsenTermin(Termin izabranTermin)
        {
            return izabranTermin is { status: StatusTermina.zavrsen, AnketaOLekaru: null };
        }

        private void OtvoriAnamnezu(object sender, RoutedEventArgs e)
        {
            if (ulogovanPacijent.zdravstveniKarton is {anamneza: { }})
                new AnamnezaView {DataContext = new AnamnezaViewModel(ulogovanPacijent)}.Show();
        }
    }
}