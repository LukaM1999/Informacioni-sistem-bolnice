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
            PregledVesti pregledVesti = new PregledVesti(ListaVesti);
            VestDto vestDto = SekretarKontroler.Instance.PregledVesti((Vest)ListaVesti.SelectedItem);
            pregledVesti.sadrzaj.Text = vestDto.Sadrzaj;
            pregledVesti.naslov.Content = vestDto.Id;
            pregledVesti.vremeObjave.Content = vestDto.VremeObjave;
            pregledVesti.Show();
        }

        private void ObrisiVest_Clik(object sender, RoutedEventArgs e)
        {
            if (ListaVesti.SelectedValue is not null)
                SekretarKontroler.Instance.UklanjanjeVesti((Vest)ListaVesti.SelectedValue);
            ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
        }

        private void IzmeniVest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListaVesti.SelectedValue is not null)
            {
                IzmenaVesti izmenaAVesti = new IzmenaVesti(this, menu.pocetna);
                izmenaAVesti.naslovVesti.Text = ((Vest)ListaVesti.SelectedItem).Id;
                izmenaAVesti.sadrzajVesti.Text = ((Vest)ListaVesti.SelectedItem).Sadrzaj;
                ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
                menu.pocetna.contentControl.Content = izmenaAVesti.Content;
            }
        }

    }
}
