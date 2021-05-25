using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaVesti : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;

        public IzmenaVesti(VestiProzor vestiProzor, PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            vesti = vestiProzor;
            pocetna = pocetnaStranicaSekretara;
        }

        private void IzmeniVest_Click(object sender, RoutedEventArgs e)
        {
            VestDto vestDto = new VestDto(naslovVesti.Text, sadrzajVesti.Text);
            SekretarKontroler.Instance.IzmenaVesti(vestDto, (Vest)vesti.ListaVesti.SelectedItem);
            vesti.ListaVesti.ItemsSource = VestRepo.Instance.Vesti;
            pocetna.contentControl.Content = vesti.Content;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e) => pocetna.contentControl.Content = vesti.Content;

    }
}
