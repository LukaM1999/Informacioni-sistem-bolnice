using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class KreirajVestProzor : UserControl 
    {
        public PocetnaStranicaSekretara pocetna;
        public VestiProzor vesti;
        public UCMenuSekretara menu;
        public KreirajVestProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara, UCMenuSekretara menuSekretara, VestiProzor vestiProzor)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            vesti = vestiProzor;
            menu = menuSekretara;
        }

        private void KreirajVest_Click(object sender, RoutedEventArgs e)
        {
            VestDto vestDto = new VestDto(naslovVesti.Text, sadrzajVesti.Text);
            vestDto.VremeObjave = (DateTime.Now);
            SekretarKontroler.Instance.KreiranjeVesti(vestDto);
            pocetna.contentControl.Content = new VestiProzor(menu);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            pocetna.contentControl.Content = vesti.Content;
        }
    }
}
