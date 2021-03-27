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
using RadSaDatotekama;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for ProstorijaForma.xaml
    /// </summary>
    public partial class ProstorijaForma : Window
    {
        public ProstorijaForma()
        {
            InitializeComponent();
        }

        private void potvrdiDugme_Click(object sender, RoutedEventArgs e)
        {

            Prostorija novaProstorija = new Prostorija(int.Parse(spratUnos.Text), (Model.TipProstorije) Enum.Parse(typeof(TipProstorije), tipUnos.Text), idUnos.Text, false);
            Prostorije.Instance.listaProstorija.Add(novaProstorija);
            Prostorije.Instance.Serijalizacija("../../../json/prostorije.json");
            this.Visibility = Visibility.Hidden;

        }
    }
}
