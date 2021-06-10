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
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.LekarView
{
    public partial class LekarPomeranjeTermina : MetroContentControl
    {
        private GlavniProzorLekara glavniProzor;
        public ObservableCollection<Prostorija> FiltriraneProstorije { get; set; }
        private Termin izabran;
        public LekarPomeranjeTermina(GlavniProzorLekara glavni, Termin termin)
        {
            InitializeComponent();
            PacijentRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            izabran = termin;
            pocetak.SelectedDate = termin.Vreme;
            glavniProzor = glavni;
            FiltriraneProstorije = new();
            Prostorija.ItemsSource = FiltriraneProstorije;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (pocetak.SelectedDate != null && kraj.SelectedDate != null && tip.SelectedIndex > -1 && Prostorija.SelectedIndex > -1)
            {
                LekarIzborTermina izbor = new LekarIzborTermina(glavniProzor,
                    new((DateTime)pocetak.SelectedDate, (DateTime)kraj.SelectedDate,
                        glavniProzor.ulogovanLekar, PacijentRepo.Instance.NadjiPoJmbg(izabran.PacijentJmbg),
                        (Prostorija)Prostorija.SelectionBoxItem,
                        (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked),
                    this);
                glavniProzor.contentControl.Content = izbor;
                TerminRepo.Instance.ObrisiTermin(izabran);
            }
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new UCRaspored(glavniProzor);
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
