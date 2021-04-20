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
using Model;
using Repozitorijum;
using Servis;
using Kontroler;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeTerminaPacijentaProzor : Window
    {
        private ObservableCollection<Termin> slobodniTermini;
        public TerminiPacijentaProzor terminiPacijenta;

        public PomeranjeTerminaPacijentaProzor(TerminiPacijentaProzor termini)
        {
            InitializeComponent();

            terminiPacijenta = termini;
            slobodniTermini = new ObservableCollection<Termin>();
            foreach (Lekar izabraniLekar in Lekari.Instance.listaLekara)
            {
                if (izabraniLekar.jmbg == ((Termin)termini.listaZakazanihTermina.SelectedItem).lekarJMBG)
                {
                    DateTime slobodanTermin = (((Termin)termini.listaZakazanihTermina.SelectedItem).vreme).
                        Subtract(new TimeSpan(48 + ((Termin)termini.listaZakazanihTermina.SelectedItem).vreme.Hour, 0, 0));
                    slobodanTermin = slobodanTermin.AddHours(7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                ((Termin)termini.listaZakazanihTermina.SelectedItem).pacijentJMBG, izabraniLekar.jmbg, null));

                            slobodanTermin = slobodanTermin.AddMinutes(30);

                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    slobodanTermin = (((Termin)termini.listaZakazanihTermina.SelectedItem).vreme).
                        Subtract(new TimeSpan(((Termin)termini.listaZakazanihTermina.SelectedItem).vreme.Hour, 0, 0)).AddHours(24 + 7);

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                ((Termin)termini.listaZakazanihTermina.SelectedItem).pacijentJMBG, izabraniLekar.jmbg, null));

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
            }
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.Instance.Pomeranje(this);
        }
    }
}
