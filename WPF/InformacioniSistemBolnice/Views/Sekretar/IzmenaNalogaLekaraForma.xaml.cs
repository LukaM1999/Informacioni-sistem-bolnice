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
using Kontroler;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    public partial class IzmenaNalogaLekaraForma : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public UCLekari UCLekari;
        public IzmenaNalogaLekaraForma(PocetnaStranicaSekretara pocetnaStranicaSekretara, UCLekari uCLekari)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            UCLekari = uCLekari;
            Specijalizacije.Instance.Deserijalizacija();
            specijalizacijeLekara.ItemsSource = Specijalizacije.Instance.listaSpecijalizacija;
        }

        private void PotvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)(UCLekari.ListaLekara.SelectedValue);
            LekarDto lekarDto = new LekarDto(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, DateTime.Parse(datumUnos.Text),
                                            drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text,
                                           telUnos.Text, mailUnos.Text, korisnikUnos.Text, lozinkaUnos.Password);
            IzmenaSpecijalizacijeLekara(lekar);
            SekretarKontroler.Instance.IzmenaNalogaLekara(lekarDto, lekar);
            this.pocetna.contentControl.Content = new UCLekari(pocetna);
        }

        private void IzmenaSpecijalizacijeLekara(Lekar lekar)
        {
            if (specijalizacijeLekara.SelectedItem != null)
            {
                LekarDto lekarDto1 = new LekarDto(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, DateTime.Parse(datumUnos.Text),
                                            drzavaUnos.Text, gradUnos.Text, ulicaUnos.Text, brojUnos.Text,
                                           telUnos.Text, mailUnos.Text, korisnikUnos.Text, lozinkaUnos.Password, specijalizacijeLekara.SelectedItem.ToString());
                SekretarKontroler.Instance.IzmenaNalogaLekara(lekarDto1, lekar);
            }
        }

        private void NazadBtn_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new UCLekari(pocetna);
        }
    }
}
