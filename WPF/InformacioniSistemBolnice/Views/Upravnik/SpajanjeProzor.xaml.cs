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
using Kontroler;

namespace InformacioniSistemBolnice.Views.Upravnik
{
    public partial class SpajanjeProzor : Window
    {
        public SpajanjeProzor()
        {
            InitializeComponent();
            TransformacijaProstorijaRepo.Instance.Deserijalizacija();
            cbStaraProstorija1.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbStaraProstorija2.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbTipNovaProstorija.ItemsSource = Enum.GetValues(typeof(TipProstorije));

            cbStaraProstorija.ItemsSource = ProstorijaRepo.Instance.Prostorije;
            cbTipNovaProstorija1.ItemsSource = Enum.GetValues(typeof(TipProstorije));
            cbTipNovaProstorija2.ItemsSource = Enum.GetValues(typeof(TipProstorije));

            dpPocetak.DisplayDateStart = DateTime.Today;
            dpKraj.DisplayDateStart = DateTime.Today;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (dpPocetak.SelectedDate != null && dpKraj.SelectedDate != null && dpPocetak.SelectedDate < dpKraj.SelectedDate)
            {
                if (rbSpajanje.IsChecked == true)
                {
                    Prostorija prvaStaraProstorija = (Prostorija)cbStaraProstorija1.SelectedItem;
                    Prostorija drugaStaraProstorija = (Prostorija)cbStaraProstorija2.SelectedItem;
                    Prostorija novaProstorija = new(Int32.Parse(tbSpratNovaProstorija.Text)
                        , (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipNovaProstorija.Text)
                        , tbIdNovaProstorija.Text, false, new());
                    if (SeNekaProstorijaVecRenovira(prvaStaraProstorija, drugaStaraProstorija, novaProstorija))
                        return;
                    UpravnikKontroler.Instance.TransformisiProstorije(new(true, prvaStaraProstorija.Id, 
                        drugaStaraProstorija.Id, novaProstorija, null, (DateTime)dpPocetak.SelectedDate
                        , (DateTime)dpKraj.SelectedDate));
                    Close();
                }
                if (rbSpajanje.IsChecked == false)
                {
                    Prostorija staraProstorija = (Prostorija)cbStaraProstorija.SelectedItem;
                    Prostorija prvaNovaProstorija = new(Int32.Parse(tbSpratNovaProstorija1.Text)
                        , (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipNovaProstorija1.Text)
                        , tbIdNovaProstorija1.Text, false, new());
                    Prostorija drugaNovaProstorija = new(Int32.Parse(tbSpratNovaProstorija2.Text)
                        , (TipProstorije)Enum.Parse(typeof(TipProstorije), cbTipNovaProstorija2.Text)
                        , tbIdNovaProstorija2.Text, false, new());
                    if (SeNekaProstorijaVecRenovira(staraProstorija, prvaNovaProstorija, drugaNovaProstorija))
                        return;
                    UpravnikKontroler.Instance.TransformisiProstorije(new(false, staraProstorija.Id, null
                        , prvaNovaProstorija, drugaNovaProstorija, (DateTime)dpPocetak.SelectedDate
                        , (DateTime)dpKraj.SelectedDate));
                    Close();
                }
            }
            
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
