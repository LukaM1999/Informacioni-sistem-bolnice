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

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for GlavniProzorSekretara.xaml
    /// </summary>
    public partial class GlavniProzorSekretara : Window
    {
        public GlavniProzorSekretara()
        {
            InitializeComponent();
        }

        private void naloziPacijenata(object sender, RoutedEventArgs e)
        {
            PacijentiProzor pacijentiProzor = new PacijentiProzor();
            pacijentiProzor.Show();
        }

        private void alergeni_Click(object sender, RoutedEventArgs e)
        {
            AlergeniProzor alergeniProzor = new AlergeniProzor();
            alergeniProzor.Show();
        }

       
        private void zakazivanjeTerminaPacijentima_Click(object sender, RoutedEventArgs e)
        {
            TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara = new TerminiPacijentaProzorSekretara();
            terminiPacijentaProzorSekretara.Show();


        }

        private void vesti_Click(object sender, RoutedEventArgs e)
        {
            VestiProzor vestiProzor = new VestiProzor();
            vestiProzor.Show();
        }

        private void upravljanjeUrgentnimSistemom_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeUrgentnimSistemomProzor upravljanjeUrgentnimSistemomProzor = new UpravljanjeUrgentnimSistemomProzor();
            upravljanjeUrgentnimSistemomProzor.Show();
        }
    }
}
