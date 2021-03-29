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
using Logika;

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
            DateTime datum = DateTime.Parse(datumUnos.Text);
            Pacijent p = new Pacijent(new Osoba(imeUnos.Text, prezimeUnos.Text, JMBGUnos.Text, datum, telUnos.Text, mailUnos.Text, 
                new Korisnik(korisnikUnos.Text, lozinkaUnos.Password, (Model.UlogaKorisnika)Enum.Parse(typeof(UlogaKorisnika), "pacijent"))));
            UpravljanjeNalozimaPacijenata.Instance.KreiranjeNaloga(p);
            this.Close();

        }

    }
}
