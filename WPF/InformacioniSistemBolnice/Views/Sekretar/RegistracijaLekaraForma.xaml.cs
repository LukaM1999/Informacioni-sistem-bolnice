using System;
using System.Windows;
using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using Repozitorijum;
using Kontroler;

namespace InformacioniSistemBolnice
{
    public partial class RegistracijaLekaraForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;

        public RegistracijaLekaraForma(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            SpecijalizacijaRepo.Instance.Deserijalizacija();
            specijalizacijeLekara.ItemsSource = SpecijalizacijaRepo.Instance.Specijalizacije;
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
            => pocetna.contentControl.Content = new Lekari(pocetna);

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            LekarDto lekarDto = PokupiPodatkeSaForme();
            SekretarKontroler.Instance.KreiranjeNalogaLekara(lekarDto);
            pocetna.contentControl.Content = new Lekari(pocetna);
        }

        private LekarDto PokupiPodatkeSaForme()
        {
            return new LekarDto(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, DateTime.Parse(datumUnos.Text),
                                                         drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text,
                                                        telUnos.Text, mailUnos.Text, korisnikUnos.Text, lozinkaUnos.Password,
                                                        specijalizacijeLekara.SelectedItem.ToString());
        }

    }
}
