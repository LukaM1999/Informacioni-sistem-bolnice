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

        private void DefinisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            this.pocetna.contentControl.Content = new DefinisanjeAlergenaForma(this);
        }

        private void ObrisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (ListaAlergena.SelectedValue != null)
                SekretarKontroler.Instance.UklanjanjeAlergena((Alergen)ListaAlergena.SelectedValue);
            this.pocetna.contentControl.Content = new AlergeniProzor(pocetna);
        }

        private void IzmjeniAlergen_Click(object sender, RoutedEventArgs e)
        {
            if (ListaAlergena.SelectedValue != null)
            {
                IzmenaAlergenaForma izmenaAlergenaForma = new IzmenaAlergenaForma(this);
                izmenaAlergenaForma.nazivAlergenaUnos.Text = ((Alergen)ListaAlergena.SelectedItem).Naziv;
                pocetna.contentControl.Content = izmenaAlergenaForma.Content;
            }
        }

        private void PregledAlergena_Click(object sender, RoutedEventArgs e)
        {
            if (ListaAlergena.SelectedIndex >= 0)
            {
                PregledAlergena pregledAlergena = new PregledAlergena(this);
                AlergenDto alergenDto = SekretarKontroler.Instance.PregledAlergena((Alergen)ListaAlergena.SelectedItem);
                pregledAlergena.naziv.Content = alergenDto.Naziv;
                pocetna.contentControl.Content = pregledAlergena.Content;
            }
        }


    }
}
