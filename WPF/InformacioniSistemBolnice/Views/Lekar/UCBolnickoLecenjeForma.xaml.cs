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


namespace InformacioniSistemBolnice.Views.Lekar
{
    /// <summary>
    /// Interaction logic for UCBolnickoLecenjeForma.xaml
    /// </summary>
    public partial class UCBolnickoLecenjeForma : UserControl
    {
        private GlavniProzorLekara glavniProzor;
        public ObservableCollection<Prostorija> sobe = new();
        public Model.Pacijent pacijent;
        public UCBolnickoLecenjeForma(Model.Pacijent izabran, GlavniProzorLekara glavni)
        {
            InitializeComponent();
            BolnickoLecenjeRepo.Instance.Deserijalizacija();
            Prostorije.Instance.Deserijalizacija();
            glavniProzor = glavni;
            pacijent = izabran;
            PronalaziProstorijeZaHospitalizaciju();
        }

        private void PronalaziProstorijeZaHospitalizaciju()
        {
            foreach (Prostorija prostorija in Prostorije.Instance.ListaProstorija)
            {
                DodajProstoriju(prostorija);
            }
            ProstorijeZaHospitalizaciju.ItemsSource = sobe;
        }

        private void DodajProstoriju(Prostorija prostorija)
        {
            if (prostorija.Tip == TipProstorije.prostorijaZaHospitalizaciju)
            {
                sobe.Add(prostorija);
            }
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
                int brojac = 0;
                Prostorija prostorija = (Prostorija)ProstorijeZaHospitalizaciju.SelectedItem;
                DateTime pocetak = (DateTime)Pocetak.SelectedDate;
                DateTime zavrsetak = (DateTime)Zavrsetak.SelectedDate;

                if (prostorija.Renoviranje != null)
                {
                    if (pocetak >= prostorija.Renoviranje.PocetakRenoviranja &&
                        pocetak <= prostorija.Renoviranje.KrajRenoviranja ||
                        zavrsetak >= prostorija.Renoviranje.PocetakRenoviranja &&
                        zavrsetak <= prostorija.Renoviranje.KrajRenoviranja)
                    {
                        MessageBox.Show("Prostorija ima zakazano renoviranje izmedju "
                                        + prostorija.Renoviranje.PocetakRenoviranja.ToString("g") +
                                        " i " + prostorija.Renoviranje.KrajRenoviranja.Date.ToString("g"));
                        return;
                    }
                }

                foreach (BolnickoLecenje lecenje in BolnickoLecenjeRepo.Instance.BolnickaLecenja)
                {
                    if (lecenje.NazivProstorije == prostorija.Id)
                    {
                        brojac++;
                    }
                }

                foreach (Model.StatickaOprema oprema in prostorija.Inventar.StatickaOprema)
                {
                    if (oprema.tip == TipStatickeOpreme.krevet)
                    {
                        if (brojac < oprema.kolicina)
                        {
                            BolnickoLecenje novoLecenje = new(pocetak, zavrsetak, prostorija.Id, pacijent.jmbg);
                            BolnickoLecenjeRepo.Instance.DodajLecenje(novoLecenje);
                            BolnickoLecenjeRepo.Instance.Serijalizacija();
                            return;
                        }
                        MessageBox.Show("Zauzeti svi kreveti u prostoriji");
                        return;
                    }
                }
            }
        }
    }
}
