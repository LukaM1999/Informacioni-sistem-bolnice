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
using RadSaDatotekama;
using Model;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentiProzor.xaml
    /// </summary>
    public partial class PacijentiProzor : Window
    {
        public PacijentiProzor()
        {
            InitializeComponent();
            
            Pacijenti.Instance.Deserijalizacija("pacijenti.json");
            ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void registrujPacijentaDugme_Click(object sender, RoutedEventArgs e)
        {
            RegistracijaPacijentaForma rP = new RegistracijaPacijentaForma();
            rP.Show();
        }
    }
}
