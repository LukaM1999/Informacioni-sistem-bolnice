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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for IzborTerminaPacijenta.xaml
    /// </summary>
    public partial class IzborTerminaPacijenta : Window
    {
       
        private ObservableCollection<Termin> slobodniTermini;

        public ZakazivanjeTerminaSekretara zakazivanjeTerminaPacijenta;
        

        public IzborTerminaPacijenta(ZakazivanjeTerminaSekretara zakazivanje, string jmbgPacijenta)
        {
            InitializeComponent();
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

                        slobodniTermini.Add(new Termin(slobodanTermin, 30.0, (Model.TipTermina)Enum.Parse(typeof(Model.TipTermina), zakazivanje.tipTermina.SelectedItem.ToString()), StatusTermina.slobodan,
                                                       jmbgPacijenta, izabraniLekar.jmbg, zakazivanje.prostorije.SelectedItem.ToString()));


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
                                foreach (Termin zauzetTermin in izabraniLekar.zauzetiTermini)
                                {
                                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                   jmbgPacijenta, izabraniLekar.jmbg, null));

                                    slobodanTermin = slobodanTermin.AddMinutes(30);
                                }
                            }
                            slobodanTermin = slobodanTermin.AddHours(10.5);
                        }
                        slobodanTermin = ((DateTime)zakazivanje.maxDatumTermina.SelectedDate).AddHours(7);
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 27; j++)
                            {
                                foreach (Termin zauzetTermin in izabraniLekar.zauzetiTermini)
                                {
                                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                                   jmbgPacijenta, izabraniLekar.jmbg, null));

                                    slobodanTermin = slobodanTermin.AddMinutes(30);
                                }
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
            //UpravljanjeTerminimaPacijenata.Instance.Zakazivanje(this, this.slobodniTermini[0].pacijentJMBG);
            UpravljanjeNalozimaPacijenata.Instance.ZakazivanjeTermina(this, slobodniTermini[0].pacijentJMBG);
            this.Close();
        }
    }
}
