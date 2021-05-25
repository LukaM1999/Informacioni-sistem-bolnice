﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Repozitorijum;
using Model;
using Servis;
using Kontroler;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class PacijentiProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public ObservableCollection<Pacijent> pacijenti = new ObservableCollection<Pacijent>();

        public PacijentiProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            PrikaziPacijente();
        }

        private void PrikaziPacijente()
        {
            PacijentRepo.Instance.Deserijalizacija();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
                if (pacijent.Korisnik.KorisnickoIme != null) pacijenti.Add(pacijent);
            ListaPacijenata.ItemsSource = pacijenti.ToList();
        }

        private void RegistrujPacijenta_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new RegistracijaPacijentaForma(this);
        }

        private void ObrisiPacijenta_Click(object sender, RoutedEventArgs e)
        {
            if (ListaPacijenata.SelectedValue != null)
                SekretarKontroler.Instance.UklanjanjeNaloga((Pacijent)ListaPacijenata.SelectedItem);
            pocetna.contentControl.Content = new PacijentiProzor(pocetna);
        }

        private void IzmeniPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null)
                pocetna.contentControl.Content = new IzmenaNalogaPacijentaForma(this, izabraniPacijent);
        }

        private void VidiPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null && izabraniPacijent.zdravstveniKarton is not null)
                pocetna.contentControl.Content = new PregledNalogaPacijenta(this, izabraniPacijent);
        }

        private void ZdravstveniKartonPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null && izabraniPacijent.zdravstveniKarton is not null)
                pocetna.contentControl.Content = new PregledZdravstvenogKartona(this, izabraniPacijent);
        }

        private void IzmeniZdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            Pacijent izabraniPacijent = (Pacijent)ListaPacijenata.SelectedValue;
            if (izabraniPacijent is not null && izabraniPacijent.zdravstveniKarton is not null)
                pocetna.contentControl.Content = new IzmenaZdravstvenogKartonaForma(this, pocetna, izabraniPacijent);
        }

    }
}
