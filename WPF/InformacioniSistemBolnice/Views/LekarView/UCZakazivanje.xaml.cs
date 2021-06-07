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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using MahApps.Metro.Controls;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views
{
    /// <summary>
    /// Interaction logic for UCZakazivanje.xaml
    /// </summary>
    public partial class UCZakazivanje : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public ObservableCollection<Prostorija> FiltriraneProstorije { get; set; }
        public UCZakazivanje(GlavniProzorLekara glavni)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            glavniProzor = glavni;
            ListaPacijenata.ItemsSource = PacijentRepo.Instance.Pacijenti;
            FiltriraneProstorije = new();
            Prostorija.ItemsSource = FiltriraneProstorije;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new UCRaspored(glavniProzor);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (pocetak.SelectedDate != null && kraj.SelectedDate != null && ListaPacijenata.SelectedIndex > -1 && tip.SelectedIndex > -1)
            {
                IzborTerminaLekara izborTerminaLekara = new(new((DateTime)pocetak.SelectedDate, (DateTime)kraj.SelectedDate,
                    glavniProzor.ulogovanLekar, (global::Model.Pacijent)ListaPacijenata.SelectedItem, (Prostorija)Prostorija.SelectionBoxItem,
                    (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked));
                izborTerminaLekara.Show();
            }
        }
        private void tip_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltriraneProstorije.Clear();
            if (tip.SelectedItem != null)
            {
                foreach (Prostorija ponudjenaProstorija in ProstorijaRepo.Instance.Prostorije)
                {
                    if (tip.SelectedIndex == 0)
                    {
                        if (ponudjenaProstorija.Tip == TipProstorije.prostorijaZaPreglede)
                            FiltriraneProstorije.Add(ponudjenaProstorija);
                    }
                    else if (ponudjenaProstorija.Tip == TipProstorije.operacionaSala)
                        FiltriraneProstorije.Add(ponudjenaProstorija);
                }
            }
        }
    }
}
