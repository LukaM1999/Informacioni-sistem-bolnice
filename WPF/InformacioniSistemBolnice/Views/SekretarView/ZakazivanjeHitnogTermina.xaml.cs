using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeHitnogTermina : UserControl
    {
        public UpravljanjeUrgentnimSistemomProzor upravljanjeUrgentnimSistemomProzor;
        public PocetnaStranicaSekretara pocetna;

        public ZakazivanjeHitnogTermina(UpravljanjeUrgentnimSistemomProzor upravljanje)
        {
            InitializeComponent();
            upravljanjeUrgentnimSistemomProzor = upravljanje;
            pocetna = upravljanje.pocetna;
            GenerisiListe();
        }

        private void GenerisiListe()
        {
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            specijalizacijeLekara.ItemsSource = SpecijalizacijaRepo.Instance.Specijalizacije;
            PacijentRepo.Instance.Deserijalizacija();
            pacijenti.ItemsSource = PacijentRepo.Instance.Pacijenti;
            ProstorijaRepo.Instance.Deserijalizacija();
            prostorije.ItemsSource = ProstorijaRepo.Instance.Prostorije;
        }

        private void ZakaziHitanTermin_Click(object sender, RoutedEventArgs e)
        {
            if (Validiraj())
            {
                Pacijent pacijent = (Pacijent)pacijenti.SelectedItem;
                Prostorija prostorija = (Prostorija)prostorije.SelectedItem;
                HitnoZakazivanjeDto hitnoZakazivanjeDto = new(specijalizacijeLekara.SelectedItem.ToString(),
                                                              pacijent.Jmbg, prostorija.Id);
                UrgentniTerminKontroler.Instance.ZakazivanjeHitnogTermina(hitnoZakazivanjeDto);
                MessageBox.Show("Vanredni termin uspjesno kreiran!");
                AzurirajPrikaz();
            } 
        }

        private void AzurirajPrikaz()
        {
            TerminRepo.Instance.Deserijalizacija();
            upravljanjeUrgentnimSistemomProzor.ListaTermina.ItemsSource = TerminRepo.Instance.Termini;
            pocetna.contentControl.Content = upravljanjeUrgentnimSistemomProzor.Content;
        }

        private void PomeriPostojeciTermin_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaPomeranje izborTerminaZaPomeranje = new IzborTerminaZaPomeranje(this);
            izborTerminaZaPomeranje.Show();
        }

        public bool Validiraj() 
        {
            if (specijalizacijeLekara.SelectedItem == null || pacijenti.SelectedItem == null || prostorije.SelectedItem == null)
            {
                MessageBox.Show("Forma nije popunjena!\nUnesite potrebne podatke!");
                return false;
            }
            else return true;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = this.upravljanjeUrgentnimSistemomProzor.Content;

    }
}
