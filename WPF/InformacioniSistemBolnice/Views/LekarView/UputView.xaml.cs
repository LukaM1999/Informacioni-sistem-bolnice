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
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.ViewModels.LekarViewModel;
using MahApps.Metro.Controls;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.LekarView
{
    /// <summary>
    /// Interaction logic for UputView.xaml
    /// </summary>
    public partial class UputView : MetroContentControl
    {
        public ObservableCollection<Lekar> imajuSpecijalizaciju = new();
        public List<string> listaSpecijalizacija = new();
        public ObservableCollection<Prostorija> FiltriraneProstorije { get; set; }
        public Model.Pacijent pacijent;
        private GlavniProzorLekara glavniProzor;

        public UputView(Model.Pacijent pacijent, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            glavniProzor = glavni;
            LekarRepo.Instance.Deserijalizacija();
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            PopunjavanjeListeSpecijalizacija();
            specijalizacije.ItemsSource = listaSpecijalizacija;
            this.pacijent = pacijent;
            FiltriraneProstorije = new();
            prostorija.ItemsSource = FiltriraneProstorije;
        }

        private void PopunjavanjeListeSpecijalizacija()
        {
            foreach (Specijalizacija specijalizacija in SpecijalizacijaRepo.Instance.Specijalizacije)
            {
                listaSpecijalizacija.Add(specijalizacija.Naziv);
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            glavniProzor.contentControl.Content = new Karton()
                {DataContext = new KartonViewModel(glavniProzor, pacijent.Jmbg)};
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imajuSpecijalizaciju.Clear();
            foreach (Lekar lekar in LekarRepo.Instance.Lekari)
            {
                PretragaLekara(lekar);
            }
            lekari.ItemsSource = imajuSpecijalizaciju;
        }

        private void PretragaLekara(Lekar lekar)
        {
            if (specijalizacije.SelectionBoxItem.Equals(null)) return;
            PopunjavanjeListeLekara(lekar);

        }

        private void PopunjavanjeListeLekara(Lekar lekar)
        {
            if (specijalizacije.SelectedItem.ToString() == lekar.Specijalizacija.Naziv)
                imajuSpecijalizaciju.Add(lekar);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pocetak.SelectedDate != null && lekari.SelectedIndex > -1 && kraj.SelectedDate != null && tip.SelectedIndex > -1)
            {
                UputDto uputDto = new UputDto((DateTime)pocetak.SelectedDate, (DateTime)kraj.SelectedDate,
                    (Lekar)lekari.SelectedItem, pacijent, (Prostorija)prostorija.SelectionBoxItem,
                    (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked);
                LekarIzborTermina izbor = new(glavniProzor, uputDto, this);
                glavniProzor.contentControl.Content = izbor;
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
