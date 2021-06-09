using System;
using System.Collections.Generic;
using System.Windows;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;
using Model;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeTerminaSekretara : UserControl
    {
        public List<string> listaDatuma = new List<string>();
        public TerminiPacijentaProzorSekretara terminiPacijenta;
        public PocetnaStranicaSekretara pocetna;

        public ZakazivanjeTerminaSekretara(TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara,
            PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            terminiPacijenta = terminiPacijentaProzorSekretara;
            pocetna = pocetnaStranicaSekretara;
            GenerisiListe();
        }

        private void GenerisiListe()
        {
            LekarRepo.Instance.Deserijalizacija();
            lekari.ItemsSource = LekarRepo.Instance.Lekari;
            PacijentRepo.Instance.Deserijalizacija();
            pacijenti.ItemsSource = PacijentRepo.Instance.Pacijenti;
            ProstorijaRepo.Instance.Deserijalizacija();
            prostorije.ItemsSource = ProstorijaRepo.Instance.Prostorije;
        }

        private void PotvrdiZakazivanje_Click(object sender, RoutedEventArgs e)
        {
            if (Validiraj())
            {
                ZakazivanjeTerminaSekretarDto zakazivanje = new((DateTime)minDatumTermina.SelectedDate,
                     (DateTime)maxDatumTermina.SelectedDate, (Lekar)lekari.SelectedItem, ((Pacijent)pacijenti.SelectedItem),
                     (TipTermina)Enum.Parse(typeof(TipTermina), this.tipTermina.SelectedItem.ToString()),
                     (Prostorija)prostorije.SelectedItem, (bool)vremeRadio.IsChecked);
                if (zakazivanje.MinDatum >= zakazivanje.MaxDatum || zakazivanje.IzabranLekar == null) return;
                pocetna.contentControl.Content = new IzborTerminaPacijenta(zakazivanje, pocetna,this).Content;
            }
        }

        public bool Validiraj()
        {
            if(minDatumTermina.SelectedDate is null || maxDatumTermina.SelectedDate is null || pacijenti.SelectedItem is null ||
                lekari.SelectedItem is null || prostorije.SelectedItem is null || tipTermina.SelectedItem is null)
            {
                MessageBox.Show("Forma nije popunjena!\nUnesite potrebne podatke!");
                return false;
            }
            return true;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = new TerminiPacijentaProzorSekretara(pocetna).Content;
        }
    }
}
