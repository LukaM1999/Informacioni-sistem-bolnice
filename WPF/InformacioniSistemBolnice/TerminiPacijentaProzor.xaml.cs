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
using System.Diagnostics;

namespace InformacioniSistemBolnice
{
    public partial class TerminiPacijentaProzor : Window
    {
        public Pacijent ulogovanPacijent;
        public Terapija trenutnaTerapija;
        public Termin pacijentovTermin;

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

            Thread proveraZavrsenostiTermina = new(ProveriZavrsenostTermina);
            proveraZavrsenostiTermina.Start();

            Thread proveraMalicioznosti = new(() =>
            {
                UpravljanjeAntiTrollMehanizmom.Instance.ProveriMalicioznostPacijenta(ulogovanPacijent);
            });
            proveraMalicioznosti.Start();

            OtvoriAnketuOBolnici();

            Thread lekObavestenja = new(UkljuciObavestenja);
            lekObavestenja.Start();

        }

        private void UkljuciObavestenja()
        {
            ZakaziObavestenja();
            while (true) PrikaziObavestenja();
        }

        private void PrikaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.Recepti)
            {
                foreach (Terapija t in recept.Terapije)
                {
                    trenutnaTerapija = t;
                    if (JeVremeZaPrikaz()) OmoguciPrikazObavestenja();
                    else OnemoguciPrikazObavestenja();
                }
            }
        }

        private void OmoguciPrikazObavestenja()
        {
            JobManager.GetSchedule("prvo uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Enable();
            JobManager.GetSchedule("uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Enable();
        }

        private void OnemoguciPrikazObavestenja()
        {
            JobManager.GetSchedule("prvo uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Disable();
            JobManager.GetSchedule("uzimanje: " + trenutnaTerapija.Lek.Naziv)?.Disable();
        }

        private bool JeVremeZaPrikaz()
        {
            return DateTime.Now > trenutnaTerapija.pocetakTerapije && DateTime.Now < trenutnaTerapija.krajTerapije;
        }

        private void ZakaziObavestenja()
        {
            foreach (Recept recept in ulogovanPacijent.zdravstveniKarton.Recepti)
            {
                foreach (Terapija terapija in recept.Terapije)
                {
                    JobManager.Initialize();
                    ZakaziPrvoObavestenje(terapija);
                    ZakaziDaljaObavestenja(terapija);
                }
            }
        }

        private static void ZakaziDaljaObavestenja(Terapija terapija)
        {
            int redovnost = DobaviRedovnostTerapije(terapija);
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.mera + " mg."),
                (s) => s.WithName("uzimanje: " + terapija.Lek.Naziv).ToRunEvery(redovnost).Seconds()
                    .DelayFor(redovnost - DateTime.Now.Second % redovnost).Seconds());
        }

        private static int DobaviRedovnostTerapije(Terapija terapija)
        {
            return (int)terapija.redovnost;
        }

        private static void ZakaziPrvoObavestenje(Terapija terapija)
        {
            int redovnost = DobaviRedovnostTerapije(terapija);
            JobManager.AddJob(
                () => Debug.WriteLine("Vreme je da uzmete: " + terapija.Lek.Naziv + ", " + terapija.mera + " mg."),
                (s) => s.WithName("prvo uzimanje: " + terapija.Lek.Naziv)
                    .ToRunOnceIn(redovnost - DateTime.Now.Second % redovnost).Seconds());
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
                    pacijentovTermin = termin;
                    if (!JeTerminZavrsen()) continue;
                    ZavrsiPacijentovTermin();
                    termin.status = StatusTermina.zavrsen;
                    Termini.Instance.Serijalizacija();
                    Termini.Instance.Deserijalizacija();
                    System.Diagnostics.Debug.WriteLine(DateTime.Now);
                }
                Thread.Sleep(1000);
            }
        }

        private void ZavrsiPacijentovTermin()
        {
            foreach (Termin termin in ulogovanPacijent.zakazaniTermini)
            {
                if (!JePacijentovNezavrsenTermin(termin)) continue;
                pacijentovTermin.status = StatusTermina.zavrsen;
                break;
            }
        }

        private bool JePacijentovNezavrsenTermin(Termin termin)
        {
            return termin.vreme == pacijentovTermin.vreme && pacijentovTermin.status != StatusTermina.zavrsen;
        }

        private bool JeTerminZavrsen()
        {
            return pacijentovTermin.vreme.AddMinutes(pacijentovTermin.trajanje) < DateTime.Now && 
                pacijentovTermin.status != StatusTermina.zavrsen;
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
