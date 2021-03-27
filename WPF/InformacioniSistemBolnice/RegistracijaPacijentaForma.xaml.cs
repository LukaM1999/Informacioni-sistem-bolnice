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
using System.Windows.Shapes;
using Model;
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for RegistracijaPacijentaForma.xaml
    /// </summary>
    public partial class RegistracijaPacijentaForma : Window
    {
        public RegistracijaPacijentaForma()
        {
            InitializeComponent();
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = new Pacijent(new Osoba(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, DateTime.Parse(datumUnos.Text), telUnos.Text, mailUnos.Text, 
                new Korisnik(korisnikUnos.Text, lozinkaUnos.Text, (Model.UlogaKorisnika)Enum.Parse(typeof(UlogaKorisnika), tipUnosa.Text))));
            Pacijenti.Instance.listaPacijenata.Add(p);
            Pacijenti.Instance.Serijalizacija("pacijenti.json");
            this.Visibility = Visibility.Hidden;



        }

        private void prezimeUnos_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
