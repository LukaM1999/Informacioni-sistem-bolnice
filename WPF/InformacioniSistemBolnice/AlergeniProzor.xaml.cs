using System.Windows;
using Repozitorijum;
using Kontroler;
using Model;
using System.Windows.Controls;

namespace InformacioniSistemBolnice
{
    public partial class AlergeniProzor : UserControl
    {
        public PocetnaStranicaSekretara pocetna;
        public AlergeniProzor(PocetnaStranicaSekretara pocetnaStranicaSekretara)
        {
            InitializeComponent();
            pocetna = pocetnaStranicaSekretara;
            Alergeni.Instance.Deserijalizacija();
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
        }

        private void definisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new DefinisanjeAlergenaForma(this);
        }

        private void obrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.UklanjanjeAlergena(ListaAlergena);

        }

        private void izmjeniAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (ListaAlergena.SelectedValue != null)
            {
                Alergen alergen = (Alergen)ListaAlergena.SelectedItem;
                IzmenaAlergenaForma izmenaAlergenaForma = new IzmenaAlergenaForma(ListaAlergena);
                izmenaAlergenaForma.nazivAlergenaUnos.Text = alergen.nazivAlergena;
                izmenaAlergenaForma.Show();
            }
        }

        private void pregledAlergena_Click(object sender, RoutedEventArgs e)
        {
            SekretarKontroler.Instance.PregledAlergena(this);

        }

    }
}
