using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for IzborTerminaZaNovoZakazivanje.xaml
    /// </summary>
    public partial class IzborTerminaZaNovoZakazivanje : Window
    {
       private ObservableCollection<Termin> slobodniTermini;
        public IzborTerminaZaPomeranje izborTerminaZaPomeranje;
        public PomeranjeZakazanogTermina pomeranjeZakazanogTermina;
        public IzborTerminaZaNovoZakazivanje(PomeranjeZakazanogTermina pomeranje)
        {
            InitializeComponent();
            pomeranjeZakazanogTermina = pomeranje;
            if (pomeranje.minDatumTermina.SelectedDate != null && pomeranje.maxDatumTermina != null)
            {
                slobodniTermini = new ObservableCollection<Termin>();
                string jmbgLekara = pomeranje.lekar.Content.ToString();
                string jmbgPacijenta = pomeranje.pacijent.Content.ToString();

                TimeSpan intervalDana = (DateTime)pomeranje.maxDatumTermina.SelectedDate - (DateTime)pomeranje.minDatumTermina.SelectedDate;
                DateTime slobodanTermin = ((DateTime)pomeranje.minDatumTermina.SelectedDate).AddHours(7);
                for (int i = 0; i < intervalDana.Days; i++)
                {
                    for (int j = 0; j < 27; j++)
                    {

                        slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                      jmbgPacijenta, jmbgLekara, null));


                        slobodanTermin = slobodanTermin.AddMinutes(30);
                    }
                    slobodanTermin = slobodanTermin.AddHours(10.5);
                }
                foreach (Termin predlozenTermin in slobodniTermini.ToList())
                {
                    foreach (Lekar izabraniLekar in LekarRepo.Instance.Lekari)
                    {
                        if (izabraniLekar.Jmbg == jmbgLekara)
                        {
                            foreach (Termin postojeciTermin in izabraniLekar.ZauzetiTermini)
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
                //slobodniTermini.Clear();
                if (slobodniTermini.Count == 0)
                {
                    if ((bool)pomeranje.lekarRadio.IsChecked)
                    {
                        slobodanTermin = ((DateTime)pomeranje.minDatumTermina.SelectedDate).Subtract(new TimeSpan(48, 0, 0));
                        slobodanTermin = slobodanTermin.AddHours(7);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {

                                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                               jmbgPacijenta, jmbgLekara, null));

                                slobodanTermin = slobodanTermin.AddMinutes(30);

                            }
                            slobodanTermin = slobodanTermin.AddHours(10.5);
                        }
                        slobodanTermin = ((DateTime)pomeranje.maxDatumTermina.SelectedDate).AddHours(7);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {

                                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                               jmbgPacijenta, jmbgLekara, null));

                                slobodanTermin = slobodanTermin.AddMinutes(30);

                            }
                            slobodanTermin = slobodanTermin.AddHours(10.5);
                        }
                        foreach (Termin predlozenTermin in slobodniTermini.ToList())
                        {
                            foreach (Lekar izabraniLekar in LekarRepo.Instance.Lekari)
                            {
                                if (izabraniLekar.Jmbg == jmbgLekara)
                                {
                                    foreach (Termin postojeciTermin in izabraniLekar.ZauzetiTermini)
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
                    else
                    {
                        slobodanTermin = ((DateTime)pomeranje.minDatumTermina.SelectedDate).AddHours(7);
                        foreach (Lekar drugiLekar in LekarRepo.Instance.Lekari)
                        {
                            foreach (Lekar izabraniLekar in LekarRepo.Instance.Lekari)
                            {
                                if (izabraniLekar.Jmbg == jmbgLekara)
                                {
                                    if (drugiLekar == izabraniLekar) continue;
                                    if (drugiLekar.Specijalizacija == izabraniLekar.Specijalizacija)
                                    {
                                        for (int i = 0; i < intervalDana.Days; i++)
                                        {
                                            for (int j = 0; j < 27; j++)
                                            {
                                                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                              jmbgPacijenta, jmbgLekara, null));

                                                slobodanTermin = slobodanTermin.AddMinutes(30);

                                            }
                                            slobodanTermin = slobodanTermin.AddHours(10.5);
                                        }
                                        foreach (Termin predlozenTermin in slobodniTermini.ToList())
                                        {
                                            foreach (Termin postojeciTermin in drugiLekar.ZauzetiTermini)
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
                    }
                }
            }
            ponudjeniTerminiZaNovoZakazivanje.ItemsSource = slobodniTermini;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PomeranjeVanrednogTerminaPacijenta(this, izborTerminaZaPomeranje);
        }
    }
 }

