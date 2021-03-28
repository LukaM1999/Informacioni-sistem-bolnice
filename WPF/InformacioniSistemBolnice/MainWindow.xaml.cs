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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using RadSaDatotekama;


namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OtvoriProzor(object sender, RoutedEventArgs e)
        {
            ProstorijeProzor prostorijeP = new ProstorijeProzor();
            // this.Visibility = Visibility.Hidden;
            prostorijeP.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacijentiProzor pacijentiP = new PacijentiProzor();
            pacijentiP.Show();
        }

        private void pacijentButton_Click(object sender, RoutedEventArgs e)
        {
            TerminiPacijentaProzor tpp = new TerminiPacijentaProzor();
            /*
            DateTime datum = DateTime.Parse("29/03/2021 7:00");
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    Termini.Instance.listaTermina.Add(new Termin(datum, 30, TipTermina.pregled, StatusTermina.slobodan));
                    datum = datum.AddMinutes(30);
                }
                datum = datum.AddHours(10.5);
            }
            
            Termini.Instance.Serijalizacija("../../../json/slobodniTerminiPacijenta.json");
            */
            tpp.Show();
            

        }
    }
}
