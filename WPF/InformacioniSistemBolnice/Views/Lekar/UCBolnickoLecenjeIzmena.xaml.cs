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
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Views.Lekar
{
    /// <summary>
    /// Interaction logic for UCBolnickoLecenjeIzmena.xaml
    /// </summary>
    public partial class UCBolnickoLecenjeIzmena : UserControl
    {
        private GlavniProzorLekara glavniProzorLekara;
        private BolnickoLecenje lecenje;
        private Model.Pacijent pacijent;
        private Prostorija prostorija;
        public UCBolnickoLecenjeIzmena(BolnickoLecenje izabranoLecenje, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            lecenje = izabranoLecenje;
            glavniProzorLekara = glavni;
            Pocetak.SelectedDate = lecenje.PocetakLecenja;
            Zavrsetak.SelectedDate = lecenje.ZavrsetakLecenja;
            if (Pocetak.SelectedDate <= DateTime.Today)
            {
                Zavrsetak.IsEnabled = true;
            }
            pacijent = PacijentRepo.Instance.NadjiPoJmbg(lecenje.JmbgPacijenta);
            ImePacijenta.Content = pacijent.Ime + " " + pacijent.Prezime;
            prostorija = ProstorijaRepo.Instance.NadjiPoId(lecenje.NazivProstorije);
            NazivProstorije.Content = lecenje.NazivProstorije;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCBolnickaLecenja lecenja = new UCBolnickaLecenja(glavniProzorLekara);
            glavniProzorLekara.contentControl.Content = lecenja;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Zavrsetak.SelectedDate != null)
            {
                LekarKontroler.Instance.IzmeniBolnickoLecenje(new((DateTime)Pocetak.SelectedDate,
                    (DateTime)Zavrsetak.SelectedDate, pacijent, prostorija));
            }
        }
    }
}
