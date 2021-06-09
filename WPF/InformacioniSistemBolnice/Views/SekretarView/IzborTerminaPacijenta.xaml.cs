using System;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
   
    public partial class IzborTerminaPacijenta : UserControl
    {
        private ObservableCollection<Termin> slobodniTermini = new();
        public Lekar IzabraniLekar { get; set; }
        public Pacijent IzabraniPacijent { get; set; }
        public TipTermina IzabraniTip { get; set; }
        public Prostorija IzabranaProstorija { get; set; }

        public PocetnaStranicaSekretara _pocetna;

        private readonly TimeSpan intervalDana;

        public ZakazivanjeTerminaSekretara zakazivanjeTermina;
        public IzborTerminaPacijenta(ZakazivanjeTerminaSekretarDto zakazivanje, PocetnaStranicaSekretara pocetna, 
            ZakazivanjeTerminaSekretara zakazivanjeTerminaSekretara)
        {
            InitializeComponent();
            _pocetna = pocetna;
            zakazivanjeTermina = zakazivanjeTerminaSekretara;
            IzabraniLekar = zakazivanje.IzabranLekar;
            IzabraniPacijent = zakazivanje.IzabranPacijent;
            IzabraniTip = zakazivanje.IzabraniTip;
            IzabranaProstorija = zakazivanje.IzabranaProstorija;
            intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            DateTime slobodanTermin = zakazivanje.MinDatum.AddHours(7);
            slobodanTermin = GenerisiRadnoVreme(intervalDana, slobodanTermin);
            ProveriTermineLekara(IzabraniLekar);
            if (slobodniTermini.Count == 0)
            {
                if (!zakazivanje.VremePrioritet)
                {
                    slobodanTermin = zakazivanje.MinDatum.Subtract(new TimeSpan(48, 0, 0));
                    slobodanTermin = slobodanTermin.AddHours(7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, IzabraniTip, StatusTermina.slobodan,
                                           IzabraniPacijent.Jmbg, IzabraniLekar.Jmbg, IzabranaProstorija.Id));
                            if (slobodniTermini.Last().ProstorijaId == null)
                                slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                            slobodanTermin = slobodanTermin.AddMinutes(30);
                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    slobodanTermin = zakazivanje.MaxDatum.AddHours(7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, IzabraniTip, StatusTermina.slobodan,
                                           IzabraniPacijent.Jmbg, IzabraniLekar.Jmbg, IzabranaProstorija.Id));
                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    ProveriTermineLekara(IzabraniLekar);
                }
                else
                {
                    slobodanTermin = zakazivanje.MinDatum.AddHours(7);
                    foreach (Lekar drugiLekar in LekarRepo.Instance.Lekari)
                    {
                        if (drugiLekar.Jmbg == zakazivanje.IzabranLekar.Jmbg) continue;
                        if (drugiLekar.Specijalizacija == zakazivanje.IzabranLekar.Specijalizacija)
                        {
                            Termin t = new Termin(slobodanTermin, 30.0, IzabraniTip, StatusTermina.slobodan,
                                IzabraniPacijent.Jmbg, drugiLekar.Jmbg, IzabranaProstorija.Id);

                            for (int i = 0; i < intervalDana.Days; i++)
                            {
                                for (int j = 0; j < 27; j++)
                                {
                                    slobodniTermini.Add(t);
                                    if (slobodniTermini.Last().ProstorijaId == null)
                                        slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                                    slobodanTermin = slobodanTermin.AddMinutes(30);
                                }
                                slobodanTermin = slobodanTermin.AddHours(10.5);
                            }
                            ProveriTermineLekara(drugiLekar);
                        }
                    }
                }
            }
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private DateTime GenerisiRadnoVreme(TimeSpan intervalDana, DateTime slobodanTermin)
        {
            for (int i = 0; i < intervalDana.Days; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, IzabraniTip, StatusTermina.slobodan,
                                           IzabraniPacijent.Jmbg, IzabraniLekar.Jmbg, IzabranaProstorija.Id));
                    slobodanTermin = slobodanTermin.AddMinutes(30);
                }
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }

            return slobodanTermin;
        }

        private void ProveriTermineLekara(Lekar izabranLekar)
        {
            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in izabranLekar.ZakazaniTermini)
                {
                    if (predlozenTermin.Vreme != postojeciTermin.Vreme) continue;
                    slobodniTermini.Remove(predlozenTermin);
                    break;
                }
            }
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.Instance.ZakaziTermin((Termin)ponudjeniTermini.SelectedValue);
            _pocetna.contentControl.Content = new TerminiPacijentaProzorSekretara(_pocetna);
            
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            _pocetna.contentControl.Content = zakazivanjeTermina.Content;
        }
    }
}
