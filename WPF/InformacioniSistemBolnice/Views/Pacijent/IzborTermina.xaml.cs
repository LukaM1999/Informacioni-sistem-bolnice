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
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class IzborTermina : Window
    {
        private ObservableCollection<Termin> slobodniTermini;

        public ZakazivanjeTerminaPacijentaProzor zakazivanjeTerminaPacijenta;

        public IzborTermina(ZakazivanjeTerminaPacijentaProzor zakazivanje, string jmbgPacijenta)
        {
            InitializeComponent();
            Prostorije.Instance.Deserijalizacija();
            zakazivanjeTerminaPacijenta = zakazivanje;
            if (zakazivanje.minDatumTermina.SelectedDate != null && zakazivanje.maxDatumTermina != null && zakazivanje.lekari.SelectedIndex > -1)
            {
                slobodniTermini = new ObservableCollection<Termin>();
                Lekar izabraniLekar = (Lekar)zakazivanje.lekari.SelectedItem;
                TimeSpan intervalDana = (DateTime)zakazivanje.maxDatumTermina.SelectedDate - (DateTime)zakazivanje.minDatumTermina.SelectedDate;
                DateTime slobodanTermin = ((DateTime)zakazivanje.minDatumTermina.SelectedDate).AddHours(7);
                for (int i = 0; i < intervalDana.Days; i++)
                {
                    for (int j = 0; j < 27; j++)
                    {

                        slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                       jmbgPacijenta, izabraniLekar.jmbg, null));
                        foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                        {
                            bool vecZakazan = false;
                            if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                            foreach (Termin termin in prostorija.termin)
                            {
                                if (slobodanTermin != termin.vreme) continue;
                                vecZakazan = true;
                                break;
                            }
                            if (vecZakazan) continue;
                            if (prostorija.Renoviranje != null)
                            {
                                if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                    slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                            }

                            slobodniTermini.Last().idProstorije = prostorija.id;
                            break;

                        }

                        if (slobodniTermini.Last().idProstorije == null)
                            slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                        slobodanTermin = slobodanTermin.AddMinutes(30);

                    }

                    slobodanTermin = slobodanTermin.AddHours(10.5);
                }
                foreach (Termin predlozenTermin in slobodniTermini.ToList())
                {
                    foreach (Termin postojeciTermin in izabraniLekar.zauzetiTermini)
                    {
                        if (predlozenTermin.vreme == postojeciTermin.vreme)
                        {
                            slobodniTermini.Remove(predlozenTermin);
                            break;
                        }
                    }
                }
                //slobodniTermini.Clear();
                if (slobodniTermini.Count == 0)
                {
                    if ((bool)zakazivanje.lekarRadio.IsChecked)
                    {
                        slobodanTermin = ((DateTime)zakazivanje.minDatumTermina.SelectedDate).Subtract(new TimeSpan(48, 0, 0));
                        slobodanTermin = slobodanTermin.AddHours(7);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {
                                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                   jmbgPacijenta, izabraniLekar.jmbg, null));

                                foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                                {
                                    bool vecZakazan = false;
                                    if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                    foreach (Termin termin in prostorija.termin)
                                    {
                                        if (slobodanTermin != termin.vreme) continue;
                                        vecZakazan = true;
                                        break;
                                    }
                                    if (vecZakazan) continue;
                                    if (prostorija.Renoviranje != null)
                                    {
                                        if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja && 
                                            slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                                    }

                                    slobodniTermini.Last().idProstorije = prostorija.id;
                                    break;

                                }

                                if (slobodniTermini.Last().idProstorije == null)
                                    slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                                slobodanTermin = slobodanTermin.AddMinutes(30);

                            }
                            slobodanTermin = slobodanTermin.AddHours(10.5);
                        }
                        slobodanTermin = ((DateTime)zakazivanje.maxDatumTermina.SelectedDate).AddHours(7);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {

                                slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                               jmbgPacijenta, izabraniLekar.jmbg, null));

                                foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                                {
                                    bool vecZakazan = false;
                                    if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                    foreach (Termin termin in prostorija.termin)
                                    {
                                        if (slobodanTermin != termin.vreme) continue;
                                        vecZakazan = true;
                                        break;
                                    }
                                    if (vecZakazan) continue;
                                    if (prostorija.Renoviranje != null)
                                    {
                                        if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                            slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                                    }

                                    slobodniTermini.Last().idProstorije = prostorija.id;
                                    break;

                                }

                                if (slobodniTermini.Last().idProstorije == null)
                                    slobodniTermini.RemoveAt(slobodniTermini.Count - 1);
                                slobodanTermin = slobodanTermin.AddMinutes(30);

                            }
                            slobodanTermin = slobodanTermin.AddHours(10.5);
                        }
                        foreach (Termin predlozenTermin in slobodniTermini.ToList())
                        {
                            foreach (Termin postojeciTermin in izabraniLekar.zauzetiTermini)
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
                        slobodanTermin = ((DateTime)zakazivanje.minDatumTermina.SelectedDate).AddHours(7);
                        foreach (Lekar drugiLekar in Lekari.Instance.listaLekara)
                        {
                            if (drugiLekar == izabraniLekar) continue;
                            if (drugiLekar.specijalizacija == izabraniLekar.specijalizacija)
                            {
                                for (int i = 0; i < intervalDana.Days; i++)
                                {
                                    for (int j = 0; j < 27; j++)
                                    {
                                        slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                       jmbgPacijenta, drugiLekar.jmbg, null));

                                        foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                                        {
                                            bool vecZakazan = false;
                                            if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                            foreach (Termin termin in prostorija.termin)
                                            {
                                                if (slobodanTermin != termin.vreme) continue;
                                                vecZakazan = true;
                                                break;
                                            }
                                            if (vecZakazan) continue;
                                            if (prostorija.Renoviranje != null)
                                            {
                                                if (slobodanTermin >= prostorija.Renoviranje.PocetakRenoviranja &&
                                                    slobodanTermin <= prostorija.Renoviranje.KrajRenoviranja) continue;
                                            }

                                            slobodniTermini.Last().idProstorije = prostorija.id;
                                            break;

                                        }

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
            }
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Zakazivanje(this, this.slobodniTermini[0].pacijentJMBG);
            this.Close();
        }
    }
}
