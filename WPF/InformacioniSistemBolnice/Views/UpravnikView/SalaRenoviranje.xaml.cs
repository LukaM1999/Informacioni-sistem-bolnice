using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaRenoviranje.xaml
    /// </summary>
    public partial class SalaRenoviranje : Page
    {
        private Prostorija IzabranaProstorija { get; set; }

        public SalaRenoviranje(Prostorija izabranaProstorija)
        {
            InitializeComponent();
            dpDatumPocetak.DisplayDateStart = DateTime.Today;
            dpDatumKraj.DisplayDateStart = DateTime.Today;
            IzabranaProstorija = izabranaProstorija;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (ProveraDatumaRenoviranja())
            {
                ProveraZauzetostiProstorije();
            }
            else
            {
                labelaPoruka.Visibility = Visibility.Visible;
            }
        }

        public bool ProveraDatumaRenoviranja()
        {
            return dpDatumPocetak.SelectedDate < dpDatumKraj.SelectedDate;
        }

        public void ProveraZauzetostiProstorije()
        {
            foreach (Termin t in IzabranaProstorija.TerminiProstorije.ToList())
            {
                if (DaLiPostojiTerminUIzabranomOpseguVremena(t))
                {
                    labelaPorukaOZauzetosti.Visibility = Visibility.Visible;
                    return;
                }
            }
            ProstorijaRenoviranjeDto dto = new ProstorijaRenoviranjeDto((DateTime)dpDatumPocetak.SelectedDate,
                    (DateTime)dpDatumKraj.SelectedDate, IzabranaProstorija);
            ProstorijaKontroler.Instance.ZakazivanjeRenoviranja(dto);
            this.NavigationService.Navigate(new Sale());
        }

        public bool DaLiPostojiTerminUIzabranomOpseguVremena(Termin zakazaniTermin)
        {
            return zakazaniTermin.Vreme >= dpDatumPocetak.SelectedDate && zakazaniTermin.Vreme < dpDatumKraj.SelectedDate;
        }
    }
}
