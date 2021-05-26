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
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;
using Servis;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class IzborTerminaZaPomeranjeZakazanog : Window
    {
        private ObservableCollection<Termin> slobodniTermini = new ObservableCollection<Termin>();
        public IzborTerminaZaPomeranje terminiZaPomeranje;

        public IzborTerminaZaPomeranjeZakazanog(IzborTerminaZaPomeranje izborTerminaZaPomeranje)
        {
            InitializeComponent();
            terminiZaPomeranje =izborTerminaZaPomeranje;
            foreach (Lekar izabraniLekar in LekarRepo.Instance.Lekari)
            {
                proveriLekaraZakazanogTermina(izabraniLekar);
            }
            ponudjeniTermini.ItemsSource = slobodniTermini;
        }

        private void proveriLekaraZakazanogTermina(Lekar izabraniLekar)
        {
            if (izabraniLekar.Jmbg == ((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).LekarJmbg)
            {
                DateTime slobodanTermin = (((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).Vreme).
                    Subtract(new TimeSpan(48 + ((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).Vreme.Hour, 0, 0));
                slobodanTermin = slobodanTermin.AddHours(7);
                slobodanTermin = izgenerisiListuSlobodnihTermina(izabraniLekar, slobodanTermin);
                slobodanTermin = (((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).Vreme).
                    Subtract(new TimeSpan(((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).Vreme.Hour, 0, 0)).AddHours(24 + 7);
                slobodanTermin = izgenerisiListuTerminaZaPomeranje(izabraniLekar, slobodanTermin);
                obrisiZauzeteTermine(izabraniLekar);
            }
        }

        private DateTime izgenerisiListuTerminaZaPomeranje(Lekar izabraniLekar, DateTime slobodanTermin)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                        ((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).PacijentJmbg, izabraniLekar.Jmbg, 
                                        ((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).ProstorijaId));
                    slobodanTermin = slobodanTermin.AddMinutes(30);
                }
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }
            return slobodanTermin;
        }

        private DateTime izgenerisiListuSlobodnihTermina(Lekar izabraniLekar, DateTime slobodanTermin)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    slobodniTermini.Add(new Termin(slobodanTermin, 30.0, TipTermina.pregled, StatusTermina.slobodan,
                                        ((Termin)terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem).PacijentJmbg, izabraniLekar.Jmbg, null));
                    slobodanTermin = slobodanTermin.AddMinutes(30);
                }
                slobodanTermin = slobodanTermin.AddHours(10.5);
            }

            return slobodanTermin;
        }

        private void obrisiZauzeteTermine(Lekar izabraniLekar)
        {
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

        private void pomeriDugme_Click(object sender, RoutedEventArgs e)
        {
            if (this.ponudjeniTermini.SelectedIndex >= 0)
            {
                TerminiLekaraZaPomeranjeDto terminiLekaraZaPomeranjeDto = new(
                    (Termin)this.terminiZaPomeranje.ponudjeniTerminiZaPomeranje.SelectedItem,
                    (Termin)this.ponudjeniTermini.SelectedItem,
                    ((Pacijent)this.terminiZaPomeranje.zakazivanjeHitnogTermina.pacijenti.SelectedItem).Jmbg);
                SekretarKontroler.Instance.PomeranjeTermina(terminiLekaraZaPomeranjeDto);
                AzurirajPrikaz();
            }


        }

        private void AzurirajPrikaz()
        {
            TerminRepo.Instance.Deserijalizacija();
            terminiZaPomeranje.zakazivanjeHitnogTermina.upravljanjeUrgentnimSistemomProzor.
            ListaTermina.ItemsSource = TerminRepo.Instance.Termini;
            this.terminiZaPomeranje.Close();
            this.Close();
        }
    }

    
}
