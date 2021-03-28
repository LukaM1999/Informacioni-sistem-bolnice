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
    /// Interaction logic for IzmenaNalogaPacijentaForma.xaml
    /// </summary>
    public partial class IzmenaNalogaPacijentaForma : Window
    {
        public ListView listaPacijenata;
        public IzmenaNalogaPacijentaForma()
        {
            InitializeComponent();
        }

        public IzmenaNalogaPacijentaForma(ListView lp)
        {
            InitializeComponent();
            listaPacijenata = lp;
        }
        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {
            Pacijent p = (Pacijent)listaPacijenata.SelectedItem;
            p.ImeOsobe = imeUnos.Text;
            p.PrezimeOsobe = prezimeUnos.Text;
            p.datumRodjenja = DateTime.Parse(datumUnos.Text);
            p.email = mailUnos.Text;
            p.telefon = telUnos.Text;
            p.korisnik.korisnickoIme = korisnikUnos.Text;
            p.korisnik.lozinka = lozinkaUnos.Text;
            this.Close();
            Pacijenti.Instance.Serijalizacija("../../../json/pacijenti.json");
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            listaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;

        }
    }
}
