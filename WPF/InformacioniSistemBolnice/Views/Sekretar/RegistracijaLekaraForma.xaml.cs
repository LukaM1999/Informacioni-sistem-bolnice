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
using Repozitorijum;
using Model;
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
        {
            pocetna.contentControl.Content = new UCLekari(pocetna);
        }

        private void PotvrdiBtn_Click(object sender, RoutedEventArgs e)
        {
            LekarDto lekarDto = new LekarDto(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, DateTime.Parse(datumUnos.Text),
                                             drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text,
                                            telUnos.Text, mailUnos.Text, korisnikUnos.Text, lozinkaUnos.Password,
                                            specijalizacijeLekara.SelectedItem.ToString());
            SekretarKontroler.Instance.KreiranjeNalogaLekara(lekarDto);
            this.pocetna.contentControl.Content = new UCLekari(pocetna);

        }

       
    }
}
