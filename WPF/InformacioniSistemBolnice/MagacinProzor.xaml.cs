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
using Repozitorijum;
using Kontroler;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MagacinProzor.xaml
    /// </summary>
    public partial class MagacinProzor : Window
    {
        public MagacinProzor()
        {
            InitializeComponent();
            StatickaOprema.Instance.Deserijalizacija("../../../json/statickaOprema.json");
            DinamickaOprema.Instance.Deserijalizacija("../../../json/dinamickaOprema.json");
            listViewStatOpreme.ItemsSource = StatickaOprema.Instance.listaOpreme;
            listViewDinamOpreme.ItemsSource = DinamickaOprema.Instance.listaOpreme;
        }

        private void dugmeKreirajOpemu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajProzor p = new MagacinDodajProzor();
            p.Show();
        }

        private void dugmeObrisiStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.BrisanjeStatickeOpreme(this);
        }

        private void dugmeIzmeniStatOpremu_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStatOpreme.SelectedValue != null)
            {
                MagacinIzmeniProzor p = new MagacinIzmeniProzor(listViewStatOpreme);
                p.postavljanjeVrednost();
                p.Show();
            }
        }

        private void dugmeKreirajDinamickuOpremu_Click(object sender, RoutedEventArgs e)
        {
            MagacinDodajDinamickuOpremu p = new MagacinDodajDinamickuOpremu();
            p.Show();
        }

        private void dugmeObrisiDinamOpremu_Click(object sender, RoutedEventArgs e)
        {
            UpravnikKontroler.Instance.BrisanjeDinamickeOpreme(this);
        }
    }
}
