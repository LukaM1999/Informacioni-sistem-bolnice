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
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class PomjeranjeTerminaProzorSekretara : Window
    {
        private ObservableCollection<Termin> slobodniTermini;
        public TerminiPacijentaProzorSekretara terminiPacijenta;
        public PomjeranjeTerminaProzorSekretara(TerminiPacijentaProzorSekretara termini)
        {
            InitializeComponent();
            terminiPacijenta = termini;
            slobodniTermini = new ObservableCollection<Termin>();
            foreach (Lekar izabraniLekar in LekarRepo.Instance.Lekari)
            {
                if (izabraniLekar.Jmbg == ((Termin)termini.listaZakazanihTermina.SelectedItem).LekarJmbg)
                {
                    DateTime slobodanTermin = (((Termin)termini.listaZakazanihTermina.SelectedItem).Vreme).
                        Subtract(new TimeSpan(48 + ((Termin)termini.listaZakazanihTermina.SelectedItem).Vreme.Hour, 0, 0));
                    slobodanTermin = slobodanTermin.AddHours(7);
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                ((Termin)termini.listaZakazanihTermina.SelectedItem).PacijentJmbg, izabraniLekar.Jmbg, ((Termin)termini.listaZakazanihTermina.SelectedItem).ProstorijaId));

                            slobodanTermin = slobodanTermin.AddMinutes(30);

                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    slobodanTermin = (((Termin)termini.listaZakazanihTermina.SelectedItem).Vreme).
                        Subtract(new TimeSpan(((Termin)termini.listaZakazanihTermina.SelectedItem).Vreme.Hour, 0, 0)).AddHours(24 + 7);

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 27; j++)
                        {
                            slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                                ((Termin)termini.listaZakazanihTermina.SelectedItem).PacijentJmbg, izabraniLekar.Jmbg, ((Termin)termini.listaZakazanihTermina.SelectedItem).ProstorijaId));

                            slobodanTermin = slobodanTermin.AddMinutes(30);

                        }
                        slobodanTermin = slobodanTermin.AddHours(10.5);
                    }
                    foreach (Termin predlozenTermin in slobodniTermini.ToList())
                    {
                        foreach (Termin postojeciTermin in izabraniLekar.ZakazaniTermini)
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
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void potvrdaPomeranjaDugme_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.Instance.PomjeranjeTerminaPacijenata((Termin)terminiPacijenta.listaZakazanihTermina.SelectedItem,
                                                                   (Termin)ponudjeniTermini.SelectedItem);
            this.Close();
        }
    }
    
}
