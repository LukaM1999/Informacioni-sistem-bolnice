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
            listViewStatOpreme.ItemsSource = StatickaOprema.Instance.listaOpreme;
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
    }
}
