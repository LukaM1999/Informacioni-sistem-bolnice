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
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UputForma.xaml
    /// </summary>
    public partial class UputForma : Window
    {
        public ObservableCollection<Lekar> imajuSpecijalizaciju = new();
        public List<string> listaSpecijalizacija = new();
        public List<Prostorija> filtriraneProstorije = new();
        public Pacijent pacijent;

        public UputForma(Pacijent pacijent)
        {
            InitializeComponent();
            LekarRepo.Instance.Deserijalizacija();
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            TerminRepo.Instance.Deserijalizacija();
            PopunjavanjeListeSpecijalizacija();
            specijalizacije.ItemsSource = listaSpecijalizacija;
            this.pacijent = pacijent;
            filtriraneProstorije.Clear();
            
            if (tip.SelectedItem != null)
            {
                foreach (Prostorija ponudjenaProstorija in ProstorijaRepo.Instance.Prostorije)
                {
                    if (tip.SelectionBoxItemStringFormat.Equals("pregled"))
                    {
                        if (ponudjenaProstorija.Tip == TipProstorije.prostorijaZaPreglede)
                            filtriraneProstorije.Add(ponudjenaProstorija);
                    }
                    else
                    {
                        if (ponudjenaProstorija.Tip == TipProstorije.operacionaSala)
                            filtriraneProstorije.Add(ponudjenaProstorija);
                    }
                }
                this.prostorija.ItemsSource = filtriraneProstorije;
            }
        }

        private void PopunjavanjeListeSpecijalizacija()
        {
            foreach (Specijalizacija specijalizacija in SpecijalizacijaRepo.Instance.Specijalizacije)
            {
                listaSpecijalizacija.Add(specijalizacija.Naziv);
            }
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
                    (Lekar)lekari.SelectedItem, pacijent, /*(Prostorija)prostorija.SelectionBoxItem,*/ 
                    (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked);
                IzborTerminaLekara izborTerminaLekara = new(uputDto);
                izborTerminaLekara.Show();
            }
        }
    }
}

