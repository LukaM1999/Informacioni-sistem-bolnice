using System;
using System.Collections.Generic;
using System.Windows;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;
using Model;
namespace InformacioniSistemBolnice
{
    public partial class ZakazivanjeTerminaSekretara : Window
    {
        public List<string> listaDatuma = new List<string>();
        public TerminiPacijentaProzorSekretara terminiPacijenta;

        public ZakazivanjeTerminaSekretara(TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara)
        {
            InitializeComponent();
            terminiPacijenta = terminiPacijentaProzorSekretara;
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
            ZakazivanjeTerminaSekretarDto zakazivanje = new((DateTime)minDatumTermina.SelectedDate,
                 (DateTime)maxDatumTermina.SelectedDate, (Lekar)lekari.SelectedItem, ((Pacijent)pacijenti.SelectedItem),
                 (TipTermina)Enum.Parse(typeof(TipTermina), this.tipTermina.SelectedItem.ToString()),
                 (Prostorija)prostorije.SelectedItem, (bool)vremeRadio.IsChecked); 
            if (zakazivanje.MinDatum >= zakazivanje.MaxDatum || zakazivanje.IzabranLekar == null) return;
            IzborTerminaPacijenta izborTermina = new IzborTerminaPacijenta(zakazivanje);
            izborTermina.Visibility = Visibility.Visible;
        }
    }
}
