using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;
using Kontroler;
using Model;
using InformacioniSistemBolnice.ViewModels.SekretarViewModel;

namespace InformacioniSistemBolnice
{
    public partial class VestiProzor : UserControl
    {
        public UCMenuSekretara menu;
        public VestiProzor(UCMenuSekretara menuSekretara)
        {
            InitializeComponent();
            menu = menuSekretara;
            VestRepo.Instance.Deserijalizacija();
            ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
        }

        private void KreirajVest_Clik(object sender, RoutedEventArgs e) =>
            menu.pocetna.contentControl.Content = new KreirajVestProzor()
            { DataContext = new KreiranjeVestiViewModel(menu.pocetna, menu, this) };

        private void VidiVest_Click(object sender, RoutedEventArgs e)
        {
            Vest izabranaVest = (Vest)ListaVesti.SelectedItem;
            PregledVesti pregledVesti = new PregledVesti()
            { DataContext = new PregledVestiViewModel(ListaVesti, izabranaVest) };
            pregledVesti.Show();
        }

        private void ObrisiVest_Clik(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeVesti((Vest)ListaVesti.SelectedValue);
            ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
        }

        private void IzmeniVest_Click(object sender, RoutedEventArgs e)
        {
            Vest izabranaVest = (Vest)ListaVesti.SelectedItem;
            if (izabranaVest is not null)
                menu.pocetna.contentControl.Content = new IzmenaVesti()
                { DataContext = new IzmenaVestiViewModel(this, menu.pocetna, izabranaVest) };
        }
    }
}
