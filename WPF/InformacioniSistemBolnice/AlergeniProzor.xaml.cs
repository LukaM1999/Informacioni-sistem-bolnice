using System.Windows;
using Repozitorijum;
using Kontroler;
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for AlergeniProzor.xaml
    /// </summary>
    public partial class AlergeniProzor : Window
    {
        public AlergeniProzor()
        {
            InitializeComponent();
            Alergeni.Instance.Deserijalizacija("../../../json/alergeni.json");
            ListaAlergena.ItemsSource = Alergeni.Instance.listaAlergena;
        }

        private void definisiAlergen_Click(object sender, RoutedEventArgs e)
        {
            DefinisanjeAlergenaForma definisanjeAlergenaForma = new DefinisanjeAlergenaForma();
            definisanjeAlergenaForma.Show();
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
            SekretarKontroler.Instance.PregledAlergena(ListaAlergena);

        }

    }
}
