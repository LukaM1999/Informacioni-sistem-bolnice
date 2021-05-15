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
        public Pacijent pacijent;

        public UputForma(Pacijent pacijent)
        {
            InitializeComponent();
            Lekari.Instance.Deserijalizacija();
            Specijalizacije.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            Termini.Instance.Deserijalizacija();
            PopunjavanjeListeSpecijalizacija();
            specijalizacije.ItemsSource = listaSpecijalizacija;
            this.pacijent = pacijent;
            this.prostorija.ItemsSource = Prostorije.Instance.listaProstorija;

        }

        private void PopunjavanjeListeSpecijalizacija()
        {
            foreach (Specijalizacija specijalizacija in Specijalizacije.Instance.listaSpecijalizacija)
            {
                listaSpecijalizacija.Add(specijalizacija.Naziv);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imajuSpecijalizaciju.Clear();
            foreach (Lekar lekar in Lekari.Instance.listaLekara)
            {
                PretragaLekara(lekar);
            }

            lekari.ItemsSource = imajuSpecijalizaciju;
        }

        private void PretragaLekara(Lekar lekar)
        {
            if (!specijalizacije.SelectionBoxItem.Equals(null))
            {
                PopunjavanjeListeLekara(lekar);
            }
        }

        private void PopunjavanjeListeLekara(Lekar lekar)
        {
            if (specijalizacije.SelectionBoxItem.ToString().Equals(lekar.specijalizacija))
            {
                imajuSpecijalizaciju.Add(lekar);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (pocetak.SelectedDate != null && lekari.SelectedIndex > -1 && kraj.SelectedDate != null)
            {
                UputDto uputDto = new UputDto((DateTime)pocetak.SelectedDate, (DateTime)kraj.SelectedDate, 
                    (Lekar)lekari.SelectedItem, pacijent, (Prostorija)prostorija.SelectionBoxItem, 
                    (TipTermina)Enum.Parse(typeof(TipTermina), tip.SelectedItem.ToString()), (bool)hitno.IsChecked);
                IzborTerminaLekara izborTerminaLekara = new(uputDto);
                izborTerminaLekara.Show();
            }
        }
    }
}

