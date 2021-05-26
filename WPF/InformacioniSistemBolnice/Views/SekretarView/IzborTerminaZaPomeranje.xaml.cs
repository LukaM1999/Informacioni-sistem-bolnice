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
using Repozitorijum;
using Model;
using System.Collections.ObjectModel;

namespace InformacioniSistemBolnice
{
    public partial class IzborTerminaZaPomeranje : Window
    {
        private ObservableCollection<Termin> terminiZaPomeranje = new ObservableCollection<Termin>();
        public ZakazivanjeHitnogTermina zakazivanjeHitnogTermina;
        public IzborTerminaZaPomeranje(ZakazivanjeHitnogTermina zakazivanje)
        {
            InitializeComponent();
            zakazivanjeHitnogTermina = zakazivanje;
            TerminRepo.Instance.Deserijalizacija();
            LekarRepo.Instance.Deserijalizacija();
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            ProveriSpecijalizacijuLekara();
            ponudjeniTerminiZaPomeranje.ItemsSource = terminiZaPomeranje.ToList();
        }

        private void ProveriSpecijalizacijuLekara()
        {
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                if (lekar.Specijalizacija.Naziv == zakazivanjeHitnogTermina.specijalizacijeLekara.SelectedItem.ToString())
                {
                    foreach (Termin zauzetiTermin in lekar.ZakazaniTermini)
                    {
                        if (!zauzetiTermin.Hitan) terminiZaPomeranje.Add(zauzetiTermin);
                    }

                }

            }
        }

        private void pomeriTermin_Click(object sender, RoutedEventArgs e)
        {
            IzborTerminaZaPomeranjeZakazanog izborTerminaZaPomeranjeZakazanog = new IzborTerminaZaPomeranjeZakazanog(this);
            izborTerminaZaPomeranjeZakazanog.Show();
        }
    }
}
