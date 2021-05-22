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
            Prostorija izabranaProstorija = zakazivanje.IzabrnaProstorija;
            TimeSpan intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            DateTime slobodanTermin = zakazivanje.MinDatum.AddHours(7);
            for (int i = 0; i < intervalDana.Days; i++)
            {
                for (int j = 0; j < 27; j++)
                {

                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                   izabraniPacijent.jmbg, izabranLekar.jmbg, izabranaProstorija.Id));
                   
                    slobodanTermin = slobodanTermin.AddMinutes(30);

                }

                slobodanTermin = slobodanTermin.AddHours(10.5);
            }

            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in izabranLekar.zauzetiTermini)
                {
                    if (predlozenTermin.vreme != postojeciTermin.vreme) continue;
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
                                                               izabraniPacijent.jmbg, izabranLekar.jmbg, izabranaProstorija.Id));

                           

                            if (slobodniTermini.Last().idProstorije == null)
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
                                                           izabraniPacijent.jmbg, izabranLekar.jmbg, izabranaProstorija.Id));

                            

                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    foreach (Termin predlozenTermin in slobodniTermini.ToList())
                    {
                        foreach (Termin postojeciTermin in izabranLekar.zauzetiTermini)
                        {
                            if (predlozenTermin.vreme == postojeciTermin.vreme)
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
                    foreach (Lekar drugiLekar in Lekari.Instance.listaLekara)
                    {
                        if (drugiLekar.jmbg == zakazivanje.IzabranLekar.jmbg) continue;
                        if (drugiLekar.specijalizacija == zakazivanje.IzabranLekar.specijalizacija)
                        {
                            for (int i = 0; i < intervalDana.Days; i++)
                            {
                                for (int j = 0; j < 27; j++)
                                {
                                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, izabraniTip, StatusTermina.slobodan,
                                                                  izabraniPacijent.jmbg, drugiLekar.jmbg, izabranaProstorija.Id));

                                
                                    if (slobodniTermini.Last().idProstorije == null)
                                        slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                                    slobodanTermin = slobodanTermin.AddMinutes(30);

                                }
                                slobodanTermin = slobodanTermin.AddHours(10.5);
                            }
                            foreach (Termin predlozenTermin in slobodniTermini.ToList())
                            {
                                foreach (Termin postojeciTermin in drugiLekar.zauzetiTermini)
                                {
                                    if (predlozenTermin.vreme == postojeciTermin.vreme)
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
