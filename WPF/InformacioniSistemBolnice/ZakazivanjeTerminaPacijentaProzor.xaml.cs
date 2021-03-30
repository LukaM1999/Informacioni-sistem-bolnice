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
using Logika;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ZakazivanjeTerminaPacijentaProzor.xaml
    /// </summary>
    public partial class ZakazivanjeTerminaPacijentaProzor : Window
    {

        public List<string> listaDatuma = new List<string>();

        public ZakazivanjeTerminaPacijentaProzor()
        {
            InitializeComponent();
            DateTime datum = DateTime.Parse("7:00");

            for (int j = 0; j < 27; j++)
            {
                listaDatuma.Add(datum.ToString("HH:mm"));
                datum = datum.AddMinutes(30);
            }

            listaSati.ItemsSource = listaDatuma;


        }

        private void potvrdaZakazivanjaDugme_Click(object sender, RoutedEventArgs e)
        {
            UpravljanjeTerminimaPacijenata.Instance.Zakazivanje(this);

        }
    }
}
