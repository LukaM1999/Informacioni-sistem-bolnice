using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;
using Kontroler;
using Model;

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
            menu.pocetna.contentControl.Content = new KreirajVestProzor(menu.pocetna, menu, this);

        private void VidiVest_Click(object sender, RoutedEventArgs e)
        {
            Vest izabranaVest = (Vest)ListaVesti.SelectedItem;
            PregledVesti pregledVesti = new PregledVesti(ListaVesti,izabranaVest);
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
                menu.pocetna.contentControl.Content = new IzmenaVesti(this, menu.pocetna, izabranaVest);
        }
    }
}
