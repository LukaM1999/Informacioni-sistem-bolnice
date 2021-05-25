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
            new Thread(() => new ZavrsenTerminServis<Pacijent>().PokreniProveruZavrsenostiTermina(ulogovanPacijent)).Start();
            new Thread(() => AntiTrollServis.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent.Jmbg)).Start();
            PrikazBolnickeAnketeServis.Instance.OtvoriAnketuOBolnici(ulogovanPacijent);
            ObavestenjePacijentaServis.Instance.UkljuciPodsetnike(ulogovanPacijent.Jmbg);
            new Thread(() => ObavestenjeTerapijeServis.Instance.UkljuciObavestenja(ulogovanPacijent)).Start();
        }

        private void InicijalizujProzor(string korisnickoIme, string lozinka)
        {
            TerminRepo.Instance.Deserijalizacija();
            PacijentRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            AnketaOBolniciRepo.Instance.Deserijalizacija();
            ulogovanPacijent = PacijentRepo.Instance.PronadjiUlogovanogPacijenta(korisnickoIme, lozinka);
            PacijentRepo.Instance.PostaviTermineUlogovanogPacijenta(ulogovanPacijent);
            listaZakazanihTermina.ItemsSource = ulogovanPacijent.ZakazaniTermini;
        }

        private void OtvoriPomeranjeTermina(object sender, RoutedEventArgs e)
        {
            Termin terminZaPomeranje = (Termin)listaZakazanihTermina.SelectedItem;
            if (listaZakazanihTermina.SelectedIndex >= 0 && terminZaPomeranje.Vreme > DateTime.Now.AddHours(24)
            && terminZaPomeranje.Status != StatusTermina.pomeren)
                new PomeranjeTerminaPacijentaView(terminZaPomeranje).Show();
        }

        private void OtvoriZakazivanjeTermina(object sender, RoutedEventArgs e)
        {
            new ZakazivanjeTerminaPacijentaView(ulogovanPacijent.Jmbg).Show();
        }

        private void OtkaziTermin(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Otkazivanje((Termin)listaZakazanihTermina.SelectedValue);
        }

        private void OtvoriTerminInfo(object sender, RoutedEventArgs e)
        {
            if (listaZakazanihTermina.SelectedIndex >= 0) PacijentKontroler.Instance.Uvid(listaZakazanihTermina);
        }

        private void OtvoriVesti(object sender, RoutedEventArgs e)
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
            new KreiranjePodsetnikaView { DataContext = new KreiranjePodsetnikaViewModel(ulogovanPacijent) }.Show();
        }

        private bool JeIzabranZavrsenTermin(Termin izabranTermin)
        {
            return izabranTermin is { Status: StatusTermina.zavrsen, AnketaOLekaru: null };
        }

        private void OtvoriAnamnezu(object sender, RoutedEventArgs e)
        {
            if (ulogovanPacijent.zdravstveniKarton is { Anamneza: { } })
                new AnamnezaView { DataContext = new AnamnezaViewModel(ulogovanPacijent) }.Show();
        }
    }
}
