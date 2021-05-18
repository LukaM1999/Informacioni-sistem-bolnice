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
    public partial class IzborTerminaPacijentaView : Window
    {
        private ObservableCollection<Termin> slobodniTermini = new();

        public IzborTerminaPacijentaView(ZakazivanjeTerminaPacijentaDTO zakazivanje)
        {
            InitializeComponent();
            Lekar izabranLekar = zakazivanje.IzabranLekar;
            TimeSpan intervalDana = zakazivanje.MaxDatum - zakazivanje.MinDatum;
            DateTime slobodanTermin = zakazivanje.MinDatum.AddHours(7);
            for (int i = 0; i < intervalDana.Days; i++)
            {
                for (int j = 0; j < 27; j++)
                {

                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                   zakazivanje.PacijentovJmbg, izabranLekar.jmbg, null));
                    foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                    {
                        bool vecZakazan = false;
                        if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                        foreach (Termin termin in prostorija.TerminiProstorije)
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
                foreach (Termin postojeciTermin in izabranLekar.zauzetiTermini)
                {
                    if (predlozenTermin.vreme != postojeciTermin.vreme) continue;
                    slobodniTermini.Remove(predlozenTermin);
                    break;
                }
            }
            //slobodniTermini.Clear();
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
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                               zakazivanje.PacijentovJmbg, izabranLekar.jmbg, null));

                            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                            {
                                bool vecZakazan = false;
                                if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                foreach (Termin termin in prostorija.TerminiProstorije)
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
                    slobodanTermin = zakazivanje.MaxDatum.AddHours(7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {

                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                           zakazivanje.PacijentovJmbg, izabranLekar.jmbg, null));

                            foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                            {
                                bool vecZakazan = false;
                                if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                foreach (Termin termin in prostorija.TerminiProstorije)
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
                                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                   zakazivanje.PacijentovJmbg, drugiLekar.jmbg, null));

                                    foreach (Prostorija prostorija in Prostorije.Instance.listaProstorija)
                                    {
                                        bool vecZakazan = false;
                                        if (prostorija.tip != TipProstorije.prostorijaZaPreglede) continue;
                                        foreach (Termin termin in prostorija.TerminiProstorije)
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

            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Zakazivanje((Termin)ponudjeniTermini.SelectedValue);
            Owner?.Close();
            Close();
        }
    }
}
