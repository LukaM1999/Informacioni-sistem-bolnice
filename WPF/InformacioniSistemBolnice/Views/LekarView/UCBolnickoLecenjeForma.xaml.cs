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
using Repozitorijum;
using Model;
using InformacioniSistemBolnice.DTO;
using Kontroler;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for UCBolnickoLecenjeForma.xaml
    /// </summary>
    public partial class UCBolnickoLecenjeForma : UserControl
    {
        private GlavniProzorLekara glavniProzor;
        public ObservableCollection<Prostorija> sobe = new();
        public Pacijent pacijent;
        public UCBolnickoLecenjeForma(Pacijent izabran, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            BolnickoLecenjeRepo.Instance.Deserijalizacija();
            ProstorijaRepo.Instance.Deserijalizacija();
            glavniProzor = glavni;
            pacijent = izabran;
            PronalaziProstorijeZaHospitalizaciju();
        }

        private void PronalaziProstorijeZaHospitalizaciju()
        {
            foreach (Prostorija prostorija in ProstorijaRepo.Instance.Prostorije)
            {
                DodajProstoriju(prostorija);
            }
            ProstorijeZaHospitalizaciju.ItemsSource = sobe;
        }

        private void DodajProstoriju(Prostorija prostorija)
        {
            if (prostorija.Tip == TipProstorije.prostorijaZaHospitalizaciju) sobe.Add(prostorija);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UCBolnickaLecenja lecenja = new UCBolnickaLecenja(glavniProzor);
            glavniProzor.contentControl.Content = lecenja;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pocetak.SelectedDate != null && Zavrsetak.SelectedDate != null && ProstorijeZaHospitalizaciju.SelectedIndex > -1)
            {
                LekarKontroler.Instance.KreirajBolnickoLecenje(new((DateTime)Pocetak.SelectedDate, 
                    (DateTime)Zavrsetak.SelectedDate, pacijent, (Prostorija)ProstorijeZaHospitalizaciju.SelectedItem));
            }
        }
    }
}
