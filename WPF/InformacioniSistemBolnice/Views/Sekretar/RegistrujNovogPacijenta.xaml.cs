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
using Model;
using Kontroler;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for RegistrujNovogPacijenta.xaml
    /// </summary>
    public partial class RegistrujNovogPacijenta : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCMenuSekretara menu;
        public RegistrujNovogPacijenta(UCMenuSekretara menuSekretara)
        {
            InitializeComponent();
            menu = menuSekretara;
            pocetna = menuSekretara.pocetna;
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            RegistracijaPacijentaDto registracijaPacijentaDto = new RegistracijaPacijentaDto(this.imeUnos.Text, this.prezimeUnos.Text, this.JMBGUnos.Text,
               DateTime.Parse(this.datumUnos.Text), this.telUnos.Text, this.mailUnos.Text, this.korisnikUnos.Text, this.lozinkaUnos.Password,
               this.drzavaUnos.Text, this.gradUnos.Text, this.ulicaUnos.Text, this.brojUnos.Text);
            SekretarKontroler.Instance.KreiranjeNaloga(registracijaPacijentaDto);
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = new UCMenuSekretara(menu.pocetna);
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.pocetna.contentControl.Content = this.menu.Content;
        }
    }
}
