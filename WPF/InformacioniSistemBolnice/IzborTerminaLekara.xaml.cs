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
    public partial class IzborTerminaLekara : Window
    {
        public ObservableCollection<Termin> slobodniTermini = new();
        public IzborTerminaLekara(UputDto uput)
        {
            InitializeComponent();
            TimeSpan intervalDana = uput.Kraj - uput.Pocetak;
            DateTime slobodanTermin = uput.Pocetak.AddHours(7);
            for (int i = 0; i < intervalDana.Days; i++)
            {
                for (int j = 0; j < 27; j++)
                {

                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                        uput.Tip, StatusTermina.slobodan,
                        uput.Pacijent.jmbg, uput.Lekar.jmbg, uput.Prostorija.id, uput.Hitan));


                    slobodanTermin = slobodanTermin.AddMinutes(30);
                }

                slobodanTermin = slobodanTermin.AddHours(10.5);
            }

            foreach (Termin predlozenTermin in slobodniTermini.ToList())
            {
                foreach (Termin postojeciTermin in uput.Lekar.zauzetiTermini)
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
                slobodanTermin =
                    (uput.Pocetak.Subtract(new TimeSpan(48, 0, 0)));
                slobodanTermin = slobodanTermin.AddHours(7);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        foreach (Termin zauzetTermin in uput.Lekar.zauzetiTermini)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                                uput.Tip, StatusTermina.slobodan,
                                uput.Pacijent.jmbg, uput.Lekar.jmbg, uput.Prostorija.id, uput.Hitan));

                            slobodanTermin = slobodanTermin.AddMinutes(30);
                        }
                    }

                    slobodanTermin = slobodanTermin.AddHours(10.5);
                }

                slobodanTermin = uput.Kraj.AddHours(7);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 27; j++)
                    {
                        foreach (Termin zauzetTermin in uput.Lekar.zauzetiTermini)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0,
                                uput.Tip, StatusTermina.slobodan,
                                uput.Pacijent.jmbg, uput.Lekar.jmbg, uput.Prostorija.id, uput.Hitan));

                            slobodanTermin = slobodanTermin.AddMinutes(30);
                        }
                    }

                    slobodanTermin = slobodanTermin.AddHours(10.5);
                }

                foreach (Termin predlozenTermin in slobodniTermini.ToList())
                {
                    foreach (Termin postojeciTermin in uput.Lekar.zauzetiTermini)
                    {
                        if (predlozenTermin.vreme == postojeciTermin.vreme)
                        {
                            slobodniTermini.Remove(predlozenTermin);
                            break;
                        }
                    }
                }
            }
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }
        private void zakaziDugme_Click(object sender, RoutedEventArgs e)
            {
                if (ponudjeniTermini.SelectedIndex > -1)
                {
                    UpravljanjeTerminimaLekara.Instance.IzdavanjeUputa((Termin)ponudjeniTermini.SelectedItem);
                }
                this.Close();
            }
    }

 
    
    }
