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
using Repozitorijum;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for PacijentiProzor1.xaml
    /// </summary>
    public partial class PacijentiProzor1 : Window
    {
       
        public PacijentiProzor1()
        {
            InitializeComponent();
            Pacijenti.Instance.Deserijalizacija("../../../json/pacijenti.json");
            ListaPacijenata.ItemsSource = Pacijenti.Instance.listaPacijenata;
        }

        private void vidiRasporedPacijenta_Click(object sender, RoutedEventArgs e)
        {
            if(ListaPacijenata.SelectedItem != null)
            {
                Pacijent pacijent = (Pacijent)ListaPacijenata.SelectedItem;
                TerminiPacijentaProzorSekretara terminiPacijentaProzorSekretara = new TerminiPacijentaProzorSekretara(pacijent.korisnik.korisnickoIme);
                terminiPacijentaProzorSekretara.Show();
            }
        }
    }
}
