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
using Repozitorijum;
using Model;
using Kontroler;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for SalaSpajanje.xaml
    /// </summary>
    public partial class SalaSpajanje : Page
    {
        public SalaSpajanje()
        {
            InitializeComponent();
            dpDatumPocetak.DisplayDateStart = DateTime.Today;
            dpDatumKraj.DisplayDateStart = DateTime.Today;
            cbPrvaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbDrugaProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbTipProstorije.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbPrvaProstorija.SelectedIndex = 0;
            cbDrugaProstorija.SelectedIndex = 0;
            cbTipProstorije.SelectedIndex = 0;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (JeSvakoPoljePopunjeno())
            {
                Prostorija prvaStaraProstorija = (Prostorija)cbPrvaProstorija.SelectedItem;
                Prostorija drugaStaraProstorija = (Prostorija)cbDrugaProstorija.SelectedItem;
                Prostorija novaProstorija = new(Int32.Parse(tbSprat.Text)
                    , (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipProstorije.Text), tbId.Text, false, new());
                if (SeNekaProstorijaVecRenovira(prvaStaraProstorija, drugaStaraProstorija, novaProstorija))
                    return;
                ProstorijaKontroler.Instance.TransformisiProstorije(new(true, prvaStaraProstorija.Id, 
                    drugaStaraProstorija.Id, novaProstorija, null, (DateTime)dpDatumPocetak.SelectedDate, 
                    (DateTime)dpDatumKraj.SelectedDate));
                this.NavigationService.Navigate(new Sale());
            }
        }

        private bool JeSvakoPoljePopunjeno()
        {
            return dpDatumPocetak.SelectedDate != null && dpDatumKraj.SelectedDate != null
                && dpDatumPocetak.SelectedDate < dpDatumKraj.SelectedDate && tbSprat.Text.All(char.IsDigit)
                && !string.IsNullOrWhiteSpace(tbSprat.Text) && !string.IsNullOrWhiteSpace(tbId.Text);
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private bool SeNekaProstorijaVecRenovira(Prostorija prvaProstorija, Prostorija drugaProstorija
            , Prostorija trecaProstorija)
        {
            if (TransformacijaProstorijaRepo.Instance.SeProstorijaVecRenovira(prvaProstorija.Id) == true
                || TransformacijaProstorijaRepo.Instance.SeProstorijaVecRenovira(drugaProstorija.Id) == true
                || TransformacijaProstorijaRepo.Instance.SeProstorijaVecRenovira(trecaProstorija.Id) == true)
                return true;
            return false;
        }
    }
}
