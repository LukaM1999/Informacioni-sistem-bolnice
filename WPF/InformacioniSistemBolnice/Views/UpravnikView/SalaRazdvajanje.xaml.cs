using Kontroler;
using Model;
using Repozitorijum;
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
    /// Interaction logic for SalaRazdvajanje.xaml
    /// </summary>
    public partial class SalaRazdvajanje : Page
    {
        private Prostorija IzabranaProstorija { get; set; }
        public SalaRazdvajanje(Prostorija izabranaProstorija)
        {
            InitializeComponent();
            IzabranaProstorija = izabranaProstorija;
            cbTipPrvaProstorija.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbTipDrugaProstorija.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            dpDatumPocetak.DisplayDateStart = DateTime.Today;
            dpDatumKraj.DisplayDateStart = DateTime.Today;
        }

        private void VratiSe(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Sale());
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (JeSvakoPoljePopunjeno())
            {
                Prostorija prvaProstorija = new(Int32.Parse(tbSpratPrvaProstorija.Text)
                , (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipPrvaProstorija.Text)
                , tbIdPrvaProstorija.Text, false, new());
                Prostorija drugaNovaProstorija = new(Int32.Parse(tbSpratDrugaProstorija.Text),
                    (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipDrugaProstorija.Text)
                    , tbIdDrugaProstorija.Text, false, new());
                if (SeNekaProstorijaVecRenovira(IzabranaProstorija, prvaProstorija, drugaNovaProstorija))
                    return;
                ProstorijaKontroler.Instance.TransformisiProstorije(new(false, IzabranaProstorija.Id, null
                    , prvaProstorija, drugaNovaProstorija, (DateTime)dpDatumPocetak.SelectedDate
                    , (DateTime)dpDatumKraj.SelectedDate));
                this.NavigationService.Navigate(new Sale());
            }
        }

        private bool JeSvakoPoljePopunjeno()
        {
            return dpDatumPocetak.SelectedDate != null && dpDatumKraj.SelectedDate != null
                && dpDatumPocetak.SelectedDate < dpDatumKraj.SelectedDate && tbSpratPrvaProstorija.Text.All(char.IsDigit)
                && !string.IsNullOrWhiteSpace(tbSpratPrvaProstorija.Text) && !string.IsNullOrWhiteSpace(tbIdPrvaProstorija.Text)
                && tbSpratDrugaProstorija.Text.All(char.IsDigit) && !string.IsNullOrWhiteSpace(tbSpratDrugaProstorija.Text)
                && !string.IsNullOrWhiteSpace(tbIdDrugaProstorija.Text);
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
