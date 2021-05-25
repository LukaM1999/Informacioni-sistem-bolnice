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
using System.Collections.ObjectModel;
using Model;
using Repozitorijum;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
   
    public partial class IzborTerminaPacijenta : Window
    {

        private ObservableCollection<Termin> slobodniTermini = new();
        public IzborTerminaPacijenta(ZakazivanjeTerminaSekretarDto zakazivanje)
        { 
            InitializeComponent();
            Lekar izabranLekar = zakazivanje.IzabranLekar;
            Pacijent izabraniPacijent = zakazivanje.IzabranPacijent;
            TipTermina izabraniTip = zakazivanje.IzabraniTip;
            Prostorija izabranaProstorija = zakazivanje.IzabranaProstorija;
            TimeSpan intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            DateTime slobodanTermin = zakazivanje.MinDatum.AddHours(7);
            for (int i = 0; i < intervalDana.Days; i++)
            {
                for (int j = 0; j < 27; j++)
                {

                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                   izabraniPacijent.Jmbg, izabranLekar.Jmbg, izabranaProstorija.Id));
                   
                    slobodanTermin = slobodanTermin.AddMinutes(30);

                }

                slobodanTermin = slobodanTermin.AddHours(10.5);
            }

            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in izabranLekar.ZakazaniTermini)
                {
                    if (predlozenTermin.Vreme != postojeciTermin.Vreme) continue;
                    slobodniTermini.Remove(predlozenTermin);
                    break;
                }
            }
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
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                               izabraniPacijent.Jmbg, izabranLekar.Jmbg, izabranaProstorija.Id));

                           

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

                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                           izabraniPacijent.Jmbg, izabranLekar.Jmbg, izabranaProstorija.Id));

                            

                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    foreach (Termin predlozenTermin in slobodniTermini.ToList())
                    {
                        foreach (Termin postojeciTermin in izabranLekar.ZakazaniTermini)
                        {
                            if (predlozenTermin.Vreme == postojeciTermin.Vreme)
                            {
                                slobodniTermini.Remove(predlozenTermin);
                                break;
                            }
                        }
                    }

                }
                else
                {
                    slobodanTermin = zakazivanje.MinDatum.AddHours(7);
                    foreach (Lekar drugiLekar in LekarRepo.Instance.Lekari)
                    {
                        if (drugiLekar.Jmbg == zakazivanje.IzabranLekar.Jmbg) continue;
                        if (drugiLekar.Specijalizacija == zakazivanje.IzabranLekar.Specijalizacija)
                        {
                            for (int i = 0; i < intervalDana.Days; i++)
                            {
                                for (int j = 0; j < 27; j++)
                                {
                                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                                  izabraniPacijent.Jmbg, drugiLekar.Jmbg, izabranaProstorija.Id));

                                
                                    if (slobodniTermini.Last().ProstorijaId == null)
                                        slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                                    slobodanTermin = slobodanTermin.AddMinutes(30);

                                }
                                slobodanTermin = slobodanTermin.AddHours(10.5);
                            }
                            foreach (Termin predlozenTermin in slobodniTermini.ToList())
                            {
                                foreach (Termin postojeciTermin in drugiLekar.ZakazaniTermini)
                                {
                                    if (predlozenTermin.Vreme == postojeciTermin.Vreme)
                                    {
                                        slobodniTermini.Remove(predlozenTermin);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.ZakazivanjeTermina((Termin)ponudjeniTermini.SelectedValue);
            this.Close();
        }
    }
}
